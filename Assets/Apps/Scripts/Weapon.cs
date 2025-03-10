using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    private ObjectPool pool;
    //Bullet b = pool.Request<Bullet>();

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
        if (pool == null)
        {
            Debug.LogError("ObjectPool is NULL! Make sure it's assigned in the scene.");
            return;
        }

        Bullet b = pool.Request<Bullet>(); // Request bullet from pool correctly

        if (b != null)
        {
            b.SetPosition(transform.position);
            b.SetTarget(GetMouseWorldPosition());
            b.Activate();
        }
        else
        {
            Debug.LogWarning("No bullets available in the pool!");
        }
    }
    private Vector3 GetMouseWorldPosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue(); 
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height); 
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
    }
}
