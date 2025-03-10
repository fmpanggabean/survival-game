using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private Camera Cam;
    private Vector3 mouse;

    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    
    [SerializeField] private float attackDelay;
    private ObjectPool pool;
    private PlayerInput playerInput;
    private InputAction fireAction;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
        playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"];
    }

    private void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        StartCoroutine(AttackEnumerator());
    }

    private void Update()
    {
        direction();
    }
    private IEnumerator AttackEnumerator()
    {
        while (true)
        {
            if (fireAction.triggered && canFire)
            {
                Shoot();
                yield return new WaitForSeconds(attackDelay);
            }
            yield return null;
        }
    }

    public void direction()
    {
        mouse = Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mouse - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation= Quaternion.Euler(0, 0, rotZ);
    }

    public void Shoot()
    {
        //Bullet b = pool.Request<Bullet>();
        //b.SetPosition(transform.position);
        //b.SetRotation(transform.rotation);

        if (pool!=null)
        {
            Bullet b = pool.Request<Bullet>();

            if (b != null)
            {
                b.SetPosition(transform.position);
                b.SetRotation(transform.rotation);
                Vector3 direction = transform.up;
                b.Fire(direction);
            }
        }
    }
}
