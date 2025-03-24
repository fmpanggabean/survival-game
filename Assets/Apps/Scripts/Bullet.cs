using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour, IPoolObject
{
    Vector3 position;
    float lifetime = 20f;
    void Update()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            lifetime = 20f;
            Deactivate();
        }
        transform.position += position.normalized * Time.deltaTime * 10;
    }
    public void Activate()
    {
        GetComponent<TrailRenderer>().Clear();
        transform .position = GameObject.Find("Player").transform.position;
        gameObject.SetActive(true);
        GetComponent<TrailRenderer>().emitting = true;
    }

    public void Deactivate()
    {
        GetComponent<TrailRenderer>().emitting = false;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            collision.GetComponent<enemy>().hp -= 20;
            lifetime = 20f;
            Deactivate();
        }
    }
}
