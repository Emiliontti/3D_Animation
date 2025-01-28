using UnityEngine;
using StarterAssets;
using Unity.VisualScripting;

public class CustomKeys : MonoBehaviour
{
    private Animator _animator;
    private StarterAssetsInputs _input;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();

        if (_input == null)
            Debug.LogError("StarterAssetsInputs not found");
    }

    void Update()
    {
        if (_input.leftClick)
        {
            Debug.Log("Left Click");
            _input.leftClick = false;
            _animator.SetTrigger("Hit");
        }

        if (_input.interact)
        {
            Debug.Log("Pressed the interact key");
            _input.interact = false;
            _animator.SetTrigger("Interact");
        }
    }

    public void OpenChest(){

        Debug.Log("Animation event triggered");
    }
}
