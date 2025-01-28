using UnityEngine;

public class InteractController : MonoBehaviour
{
    public GameObject ItemCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), Vector3.forward, out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "Interactable")
            {
                GameObject itemInRange = hit.collider.gameObject;
                return itemInRange;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + new Vector3(0, 1f, 0), Vector3.forward * 1f);
    }
}