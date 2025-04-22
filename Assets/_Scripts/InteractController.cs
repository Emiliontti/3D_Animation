using UnityEngine;

public class InteractController : MonoBehaviour
{
    public GameObject ItemCheck()
    {
        float detectionRadius = 1f;
        Vector3 sphereCenter = transform.position + new Vector3(0, 1f, 0);

        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, detectionRadius);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Interactable"))
            {
                return collider.gameObject;
            }
        }

        return null;
    }

}