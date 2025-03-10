using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    private float speed = 10f;
    private Vector3 dir;
    
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    internal void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    internal void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void Fire(Vector3 fireDirection)
    {
        dir = fireDirection.normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = dir * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectPool pool = FindObjectOfType<ObjectPool>();
    }
}
