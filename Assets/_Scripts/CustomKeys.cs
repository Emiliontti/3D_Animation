using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CustomKeys : MonoBehaviour
{
    private Animator _animator;
    private StarterAssetsInputs _input;
    private InteractController _interactController;
    private GameObject interactObject;
    private PlayerInput _playerInput;

    private bool isPerformingAction = false;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
        _interactController = GetComponent<InteractController>();
        _playerInput = GetComponent<PlayerInput>();

        if (_input == null)
            Debug.LogError("StarterAssetsInputs not found");
    }

    void Update()
    {
        if (isPerformingAction) return;

        if (_input.leftClick)
        {
            Debug.Log("Left Click");
            _input.leftClick = false;
            _animator.SetTrigger("Hit");
            StartAction(); 
        }

        if (_input.interact)
        {
            Debug.Log("Pressed the interact key");
            _input.interact = false;
            interactObject = _interactController.ItemCheck();
            if (interactObject != null)
            {
                _animator.SetTrigger("Interact");
                StartAction();
            }
        }
    }

    public void OpenChest()
    {
        if (interactObject != null)
        {
            interactObject.GetComponent<Animator>().SetTrigger("Open");
            interactObject = null;
            Debug.Log("Animation event triggered");
        }
    }

    private void StartAction()
    {
        _playerInput.enabled = false;
        isPerformingAction = true;
    }

    public void EndAction()
    {
        _playerInput.enabled = true;
        isPerformingAction = false;
    }
}
