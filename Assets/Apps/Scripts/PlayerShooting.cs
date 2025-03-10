using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Tooltip("The weapon used by the player.")]
    public Weapon weapon;  // Assign manually in the Inspector

    [Tooltip("Time between shots in seconds.")]
    public float fireRate = 0.2f; // Time between shots

    [Tooltip("Speed of the bullet.")]
    public float bulletSpeed = 10f;

    private float nextFireTime = 0f;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        RotateTowardsCursor();
        HandleShooting();
    }

    void RotateTowardsCursor()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, mainCamera.nearClipPlane));
        mouseWorldPos.z = 0f;

        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void HandleShooting()
    {
        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Bullet bullet = weapon.Shoot();
            if (bullet != null)
            {
                Vector2 shootDirection = (MousePositionInWorld() - transform.position).normalized;
                bullet.Shoot(shootDirection, bulletSpeed);
            }
        }
    }

    Vector3 MousePositionInWorld()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, mainCamera.nearClipPlane));
        mouseWorldPos.z = 0f;
        return mouseWorldPos;
    }
}
