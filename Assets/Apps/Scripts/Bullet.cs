using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour, IPoolObject
{
    public float speed = 10f;
    private Vector3 target;
    void Update()
    {
        if (gameObject.activeSelf)
        {
            MoveTowardsTarget();
        }
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
    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget;
        target.z = 0f; 

        Vector3 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Deactivate();
        }
    }
    

}
