using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    private ObjectPool pool;
    private Vector3 _mouseWorldPosition;
    private bool _isShooting;
    private float _timer;
    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
    }
    private void Update()
    {
        FollowCursor();
        if (_isShooting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Shoot();
                _timer = attackDelay;
            }
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            _isShooting = true;

        if (ctx.canceled)
            _isShooting = false;
    }

    public void Shoot()
    {
        Bullet b = pool.Request<Bullet>();
        b.SetPosition(transform.position);
        b.SetRotation(transform.rotation);
    }
    void FollowCursor()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        _mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        Vector3 direction = _mouseWorldPosition - transform.position; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void OnDrawGizmos()
    {
        //cursor angle debugger
        Gizmos.DrawLine(_mouseWorldPosition,transform.position);
    }
}