using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Tooltip("The weapon used by the player.")]
    public Weapon weapon;

    [Tooltip("The rate at which the player can fire projectiles, in seconds.")]
    public float fireRate = 0.2f;

    private float nextFireTime = 0f;

    void Update()
    {
        RotateTowardsCursor();
        HandleShooting();
    }

    void RotateTowardsCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            weapon.Shoot();
        }
    }
}
