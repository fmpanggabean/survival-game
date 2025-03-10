using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private ObjectPool pool;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
    }

    public Bullet Shoot()
    {
        Bullet b = pool.Request<Bullet>();
        if (b != null)
        {
            b.SetPosition(transform.position);
            b.SetRotation(transform.rotation);
            return b;
        }
        return null;
    }
}
