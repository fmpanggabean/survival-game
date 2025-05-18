using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private List<string> targetTag;

    public UnityEvent OnTrigger;

    private void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetTag.Contains(collision.tag))
        {
            OnTrigger?.Invoke();
        }
    }
}
