using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Dictionary<Bullet, Vector3> bulletDirections = new Dictionary<Bullet, Vector3>();

    void Update()
    {
        MoveBullets();
    }

    private void MoveBullets()
    {
        List<Bullet> bulletsToRemove = new List<Bullet>();

        foreach (var bulletEntry in bulletDirections)
        {
            Bullet bullet = bulletEntry.Key;
            Vector3 direction = bulletEntry.Value;

            if (bullet.gameObject.activeSelf)
            {
                bullet.transform.position += direction * bulletSpeed * Time.deltaTime;
            }
            else
            {
                bulletsToRemove.Add(bullet);
            }
        }

        foreach (Bullet bullet in bulletsToRemove)
        {
            bulletDirections.Remove(bullet);
        }
    }

    public void RegisterBullet(Bullet bullet)
    {
        if (bullet != null && !bulletDirections.ContainsKey(bullet))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;

            Vector3 direction = (mouseWorldPos - bullet.transform.position).normalized;
            bulletDirections[bullet] = direction;
        }
    }
}
