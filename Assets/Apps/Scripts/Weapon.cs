using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDelay = 0.2f;
    private ObjectPool pool;
    private Camera mainCamera;
    private Vector2 pointerPosition;
    private bool isShooting;
    private float lastAttackTime;

    private InputActionAsset inputAsset;
    private InputAction shootAction;
    private InputAction pointAction;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
        mainCamera = Camera.main;

        inputAsset = GetComponent<PlayerInput>().actions;
        var gameplayMap = inputAsset.FindActionMap("Gameplay");

        shootAction = gameplayMap.FindAction("Shoot");
        pointAction = gameplayMap.FindAction("Point");

        shootAction.performed += ctx => isShooting = true;
        shootAction.canceled += ctx => isShooting = false;
        pointAction.performed += ctx => pointerPosition = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        shootAction?.Enable();
        pointAction?.Enable();
    }

    private void OnDisable()
    {
        shootAction?.Disable();
        pointAction?.Disable();
    }

    private void Update()
    {
        if (isShooting && Time.time >= lastAttackTime + attackDelay)
        {
            Shoot();
            lastAttackTime = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(pointerPosition);
        worldMousePos.z = transform.position.z;

        Vector3 direction = (worldMousePos - transform.position).normalized;

        Bullet b = pool.Request<Bullet>();
        if (b == null) return;

        b.SetPosition(transform.position);
        b.SetRotation(Quaternion.identity);
        b.Init(direction);
    }
}
