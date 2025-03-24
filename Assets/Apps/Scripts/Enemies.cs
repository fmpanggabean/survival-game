using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour, IPoolObject, IDamageable
{
    private GameObject target;
    [SerializeField] private float speed = 5f;
    private Rigidbody2D _rb;

    private void Awake()
    {  
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        target = FindObjectOfType<Movement>().gameObject;
    }
    public void Damaged()
    {
        Deactivate();
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
    private void FixedUpdate()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        _rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
