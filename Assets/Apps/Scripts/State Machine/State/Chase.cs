using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    [Header("Stats")]
    public float moveSpeed = 3f;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        try
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
        catch
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        float distance = Vector2.Distance(transform.position, player.position);
        ChasePlayer();
    }
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        if (rb.linearVelocity.magnitude <= moveSpeed)
        {
            rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
        }
    }
}