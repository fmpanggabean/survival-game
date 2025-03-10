using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Tooltip("The projectile prefab to be instantiated when shooting.")]
    public GameObject projectilePrefab;

    [Tooltip("The point from which projectiles are fired.")]
    public Transform firePoint;

    [Tooltip("The speed at which the projectile travels.")]
    public float projectileSpeed = 10f;

    [Tooltip("The rate at which the player can fire projectiles, in seconds.")]
    public float fireRate = 0.2f;

    private float nextFireTime = 0f;

    void Update()
    {
        RotateTowardsCursor();
        Shoot();
    }

    void RotateTowardsCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePoint.right * projectileSpeed;
            }
        }
    }
}
}
