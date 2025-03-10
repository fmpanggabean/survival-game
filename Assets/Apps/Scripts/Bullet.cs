using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet is missing Rigidbody2D!");
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        rb.velocity = Vector2.zero;  // Stop movement when deactivating
    }

    internal void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    internal void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void Shoot(Vector2 direction, float speed)
    {
        if (rb != null)
        {
            rb.velocity = direction * speed;  // Set velocity directly
        }
        else
        {
            Debug.LogError("Bullet Rigidbody2D is missing!");
        }
    }
}
