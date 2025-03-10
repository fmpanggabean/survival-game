using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour, IPoolObject
{
    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();



        //direction = mousePos - transform.position;

    }

    void Update()
    {

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
}
