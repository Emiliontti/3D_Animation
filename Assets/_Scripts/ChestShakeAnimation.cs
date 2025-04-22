using UnityEngine;
using System.Collections;
using SmoothShakeFree;

public class ChestShakeAnimation : MonoBehaviour
{
    [SerializeField] private SmoothShake _lockShake;
    [SerializeField] private SmoothShake _chestShake;
    [SerializeField] private GameObject _goldPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _throwForce = 5f;
    [SerializeField] private float _spawnDelay = 0.2f;

    public void ShakeChest()
    {
        _lockShake.StartShake();
        _lockShake.StopShake();
        _chestShake.StartShake();
        _chestShake.StopShake();
        Debug.Log("Shaking shaking");
    }

    public void SpawnAndThrowGold()
    {
        StartCoroutine(SpawnGoldRoutine());
    }

    private IEnumerator SpawnGoldRoutine()
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 spawnPos = _spawnPoint ? _spawnPoint.position : transform.position + Vector3.up * 1f;
            GameObject gold = Instantiate(_goldPrefab, spawnPos, Quaternion.identity);

            Rigidbody rb = gold.GetComponentInChildren<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;

                Vector3 forward = transform.forward;
                Vector2 circle = Random.insideUnitCircle.normalized; 
                Vector3 right = transform.right;
                Vector3 up = Vector3.up;

                Vector3 direction = (forward + right * circle.x + up * Mathf.Abs(circle.y)).normalized;

                rb.AddForce(direction * _throwForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(_spawnDelay);
        }
    }

}
