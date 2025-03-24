using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Camera mainCamera;
    private Vector3 mouseWorldPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, -mainCamera.transform.position.z));
    }

    public void Shoot()
    {
        Vector3 direction = (mouseWorldPosition - transform.position).normalized;

        Bullet bullet = FindObjectOfType<ObjectPool>().Request<Bullet>();
        if (bullet != null)
        {
            bullet.SetPosition(transform.position);
            bullet.SetRotation(Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
            StartCoroutine(MoveBullet(bullet, direction));
        }
    }

    private IEnumerator MoveBullet(Bullet bullet, Vector3 direction)
    {
        while (bullet.gameObject.activeInHierarchy)
        {
            bullet.transform.position += direction * bulletSpeed * Time.deltaTime;
            yield return null;
        }
    }
}

