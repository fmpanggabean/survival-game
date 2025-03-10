using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float finalSpeed;
    private Rigidbody2D rb2d;
    private Vector2 lastDirection;
    List<Buff> buffs = new List<Buff>();

    [SerializeField] private GameObject bulletPrefab; // Assign your bullet prefab in the Inspector
    [SerializeField] private Transform firePoint; // Point from where the bullet is fired

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lastDirection = new Vector2();
    }

    private void Start()
    {
        buffs.Add(new Buff(1.1f, 30));
        buffs.Add(new Buff(1.1f, 30));
    }

    public void SetDirection(Vector2 direction)
    {
        finalSpeed = speed * GetAllBuffModifier();
        rb2d.velocity = direction * finalSpeed;
    }

    private float GetAllBuffModifier()
    {
        float modifier = 1;
        foreach (Buff b in buffs)
        {
            modifier *= b.modifier;
        }
        return modifier;
    }

    public void SetDirection(CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed || ctx.phase == InputActionPhase.Canceled)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();

            if (ctx.phase == InputActionPhase.Performed)
            {
                lastDirection = direction;
            }

            SetDirection(direction);
        }
    }

    public Vector2 GetLastDirection() => lastDirection;

    // Handle shooting input
    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse position
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        // Activate a bullet from the object pool
        GameObject bullet = ObjectPool.Instance.GetObject();
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}

public class Buff
{
    public float modifier;
    public float duration;

    public Buff(float modifier, float duration)
    {
        this.modifier = modifier;
        this.duration = duration;
    }
}