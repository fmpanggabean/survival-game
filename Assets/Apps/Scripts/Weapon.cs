using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    private ObjectPool pool;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
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
        //b.SetPosition(transform.position);
        b.SetPosition(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        b.SetRotation(transform.rotation);
    }
}
