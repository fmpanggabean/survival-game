using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    private ObjectPool pool;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
    }



    public void Rotate(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Rotate(CallbackContext context)
    {
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            Vector2 direction = mousePosition - (Vector2)transform.position;
            direction = direction.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Rotate(angle);
        }
    }

    private void Start()
    {
        StartCoroutine(AttackEnumerator());
    }

    private IEnumerator AttackEnumerator()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(attackDelay);
        }
    }

    public void Shoot()
    {
        Bullet b = pool.Request<Bullet>();
        b.SetPosition(transform.position);
        b.SetRotation(transform.rotation);
    }
}
