using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    private ObjectPool pool;

    Camera mainCam;
    Vector3 mousePos;
    Vector3 worldPos;
    Vector3 direction;
    float bulletSpeed = 10.0f;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

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

        mousePos = Mouse.current.position.ReadValue();
        worldPos = mainCam.ScreenToWorldPoint(mousePos);
        b.GetComponent<Rigidbody2D>().velocity = worldPos.normalized * bulletSpeed;
    }
}
