using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour, IPoolObject
{
    private Vector3 position;
    void Update()
    {
        transform.position += position * Time.deltaTime * 3;
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
        //transform.position = position;
        this.position = new Vector3((position - transform.position).normalized.x, (position - transform.position).normalized.y, 0);
    }

    internal void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
