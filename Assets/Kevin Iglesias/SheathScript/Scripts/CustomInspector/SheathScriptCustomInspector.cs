///////////////////////////////////////////////////////////////////////////
//  Sheath Script 1.1 - Custom Inspector for SheathScript                //
//  Kevin Iglesias - https://www.keviniglesias.com/     			     //
//  Contact Support: support@keviniglesias.com                           //
//  Documentation: 														 //
//  https://www.keviniglesias.com/assets/SheathScript/Documentation.pdf  //
///////////////////////////////////////////////////////////////////////////

/*
 This script makes a custom inspector for the MonoBehaviour SheathComponentScript
 for easier adding and removing sheaths.
*/

//This '#if' makes this script to compile only if using the Unity Editor,
//avoids errors when Building the game project
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEditor;
using UnityEditor.SceneManagement;

namespace KevinIglesias {

	//Custom Inspector
	[CustomEditor(typeof(SheathComponentScript))]
	public class SheathScriptCustomInspector : Editor
	{
		string version = "1.1";
		
		public override void OnInspectorGUI()
		{
			var coreScript = target as SheathComponentScript;
		 
			GUILayout.Space(5);

			GUILayout.BeginHorizontal();
			GUILayout.Label("SHEATH SCRIPT "+version);
			GUILayout.EndHorizontal();		
				
			//SEPARATOR	
			DrawUILine(Color.black);
			
			//SHEATHS
			EditorGUI.BeginChangeCheck();
			
			if(coreScript.sheaths != null)
			{
				for(int i = 0; i < coreScript.sheaths.Count; i++)
				{
					GUILayout.BeginHorizontal();
					GUIContent removeSheath = new GUIContent("[X]", "Remove this Sheath");
					if(GUILayout.Button(removeSheath, GUILayout.Width(33)))
					{
						EditorGUI.BeginChangeCheck();
						GUI.changed = true;
						if(EditorGUI.EndChangeCheck()) 
						{
							Undo.RegisterCompleteObjectUndo(target, "Removed Sheath");
						}
						coreScript.sheaths.RemoveAt(i);
						break;
					}
					GUILayout.Label("Sheath ID "+i.ToString("00")+" | "+coreScript.sheaths[i].sheathName);
					GUILayout.EndHorizontal();
					
					EditorGUI.BeginChangeCheck();
					
					GUILayout.BeginHorizontal();
					GUILayout.Space(10);
					GUIContent sheathContent = new GUIContent("Sheath Name: ", "Write a name for identifying the sheath");
					string iSheathName = EditorGUILayout.TextField(sheathContent, coreScript.sheaths[i].sheathName);
					GUILayout.EndHorizontal();

					if(EditorGUI.EndChangeCheck()) {

						Undo.RegisterCompleteObjectUndo(target, "Change Sheath Name "+(i));
						coreScript.sheaths[i].sheathName = iSheathName;
					}
					
					EditorGUI.BeginChangeCheck();
					
					GUILayout.BeginHorizontal();
					GUILayout.Space(10);
					GUIContent weaponContent = new GUIContent("Weapon: ", "Transform of the Weapon to sheath/unsheath");
					Transform iWeapon = EditorGUILayout.ObjectField(weaponContent, coreScript.sheaths[i].weapon, typeof(Transform), true) as Transform;
					GUILayout.EndHorizontal();
					
					if(EditorGUI.EndChangeCheck()) {
						Undo.RegisterCompleteObjectUndo(target, "Change Weapon"+(i));
						coreScript.sheaths[i].weapon = iWeapon;
					}
					
					if(coreScript.sheaths[i].weapon != null)
					{
						if(coreScript.sheaths[i].weapon.localEulerAngles != Vector3.zero || coreScript.sheaths[i].weapon.localPosition != Vector3.zero)
						{
							GUILayout.BeginHorizontal();
							GUILayout.Space(20);
							GUILayout.Label("WARNING: Weapon Transform Position or Rotation is not zero!");
							GUILayout.EndHorizontal();
						}
					}
					
					EditorGUI.BeginChangeCheck();
					
					GUILayout.BeginHorizontal();
					GUILayout.Space(10);
					GUIContent handContent = new GUIContent("Weapon Parent in Hand: ", "Transform of the Weapon parent when unsheathed");
					Transform iHand = EditorGUILayout.ObjectField(handContent, coreScript.sheaths[i].hand, typeof(Transform), true) as Transform;	
					GUILayout.EndHorizontal();
					
					if(EditorGUI.EndChangeCheck()) {
						Undo.RegisterCompleteObjectUndo(target, "Change Hand"+(i));
						coreScript.sheaths[i].hand = iHand;
					}
					
					EditorGUI.BeginChangeCheck();
					
					GUILayout.BeginHorizontal();
					GUILayout.Space(10);
					GUIContent wSheathContent = new GUIContent("Weapon Parent in Sheath: ", "Transform of the Weapon parent when sheathed");
					Transform iSheath = EditorGUILayout.ObjectField(wSheathContent, coreScript.sheaths[i].sheath, typeof(Transform), true) as Transform;
					GUILayout.EndHorizontal();

					if(EditorGUI.EndChangeCheck()) {
						Undo.RegisterCompleteObjectUndo(target, "Change Sheath"+(i));
						coreScript.sheaths[i].sheath = iSheath;
					}
					
					if(EditorApplication.isPlaying)
					{
						GUI.enabled = true;
					}else{
						GUI.enabled = false;
					}
					
					GUILayout.BeginHorizontal();
					if(GUILayout.Button("Manual Sheath"))
					{
						coreScript.Sheath(i);
					}
					
					if(GUILayout.Button("Manual Unsheath"))
					{
						coreScript.Unsheath(i);
					}
					GUILayout.EndHorizontal();
					
					GUI.enabled = true;
					
					DrawUILine(Color.black);
				}
			}

			GUILayout.BeginHorizontal();
			GUIContent addButton = new GUIContent("[+] Add Sheath", "Add a new Sheath");
			if(GUILayout.Button(addButton))
			{
				if(coreScript.sheaths == null)
				{
					coreScript.sheaths = new List<Sheaths>();
				}
				EditorGUI.BeginChangeCheck();
				GUI.changed = true;
				if(EditorGUI.EndChangeCheck()) 
				{
					Undo.RegisterCompleteObjectUndo(target, "Added Sheath");
				}
				coreScript.sheaths.Add(null);
			}
			
			GUIContent removeAll = new GUIContent("[X] Remove All", "Clear ALL Sheaths");
			if(GUILayout.Button(removeAll))
			{
				EditorGUI.BeginChangeCheck();
				GUI.changed = true;
				if(EditorGUI.EndChangeCheck()) 
				{
					Undo.RegisterCompleteObjectUndo(target, "Removed Sheaths");
				}
				coreScript.sheaths = new List<Sheaths>();
			}

			GUILayout.EndHorizontal();
			
			if(GUI.changed)
			{
				if(!EditorApplication.isPlaying)
				{
					EditorUtility.SetDirty(target);
					EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
				}
			}
		}
		
		//FUNCTION FOR DRAWING A SEPARATOR
		public static void DrawUILine(Color color, int thickness = 1, int padding = 5)
		{
			Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
			r.height = thickness;
			r.y+=padding/2;
			EditorGUI.DrawRect(r, color);
		}
	}
}

#endif
