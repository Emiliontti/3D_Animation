using UnityEngine;

public class CeilingFanSpinControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Range(0, 100)][SerializeField] private int _fanSpeed;

    void Update()
    {
        _animator.SetFloat("RotateSpeed", _fanSpeed);
    }
}
