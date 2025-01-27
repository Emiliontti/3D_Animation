using UnityEngine;
using StarterAssets;

public class HitController : MonoBehaviour
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
    }
}
