using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    [SerializeField] private float _speed = 5f;
    void Update()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("It Wok");
        if (!other.CompareTag("Player"))
        {
            IDamageable Enemies = other.GetComponent<IDamageable>();
            if(Enemies != null)
                Enemies.Damaged();
            Deactivate();
        }
    }

}
