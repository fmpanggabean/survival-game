using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    public float speed = 10f; // Speed of the bullet
    private Vector2 direction; // Direction the bullet will move in

    void Update()
    {
        // Move the bullet in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);
    }

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

    // Set the direction the bullet will move in
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }
}