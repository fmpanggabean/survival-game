using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class ExplodeBullet : MonoBehaviour
{
    public float speed = 5f;              
    public float minLifeTime = 1f;       
    public float maxLifeTime = 5f;      
    public GameObject explosionEffect;    

    private float timer = 0f;
    private float lifeTime;              

    void Start()
    {
        lifeTime = Range(minLifeTime, maxLifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
