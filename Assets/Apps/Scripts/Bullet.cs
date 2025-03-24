using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{

    private Vector3 direction;
    private float speed = 10f;
    private float maxDistance = 20f;
    private Vector3 startPosition;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        startPosition = transform.position;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Init(Vector3 direction)
    {
        this.direction = direction;
    }

    internal void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    internal void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(1);
            Deactivate();
        }
    }
}
