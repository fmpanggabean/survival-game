using UnityEngine;
using UnityEngine.InputSystem;


public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float bulletSpeed = 10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBullet), 0f, spawnInterval);
    }

    private void SpawnBullet()
    {
        Bullet bullet = bulletPool.Request<Bullet>();

        if (bullet != null)
        {
            bullet.SetPosition(transform.position);

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;

            Vector3 direction = (mouseWorldPos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.SetRotation(Quaternion.Euler(0, 0, angle));

            StartCoroutine(MoveBullet(bullet, direction));
        }
    }

    private System.Collections.IEnumerator MoveBullet(Bullet bullet, Vector3 direction)
    {
        while (bullet.gameObject.activeSelf)
        {
            bullet.transform.position += direction * bulletSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
