using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;
    private Movement movement;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        animator.SetFloat("speed", rb2d.linearVelocity.magnitude);
        animator.SetFloat("x", movement.GetLastDirection().x);
        animator.SetFloat("y", movement.GetLastDirection().y);
    }
}
