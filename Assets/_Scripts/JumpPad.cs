using UnityEngine;
using StarterAssets;

public class JumpPad : MonoBehaviour
{
    private StarterAssetsInputs playerController;
    private Animator animator;

    private void Start()
    {
        playerController = FindFirstObjectByType<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Launching...");
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Launch");
        }

    }

    public void LaunchPlayer()
    {
        if (playerController != null)
            playerController.jump = true;
        else
            Debug.LogWarning("Player controller not assigned or found in the scene.");
    }
}
