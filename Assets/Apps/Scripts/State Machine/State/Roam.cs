using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : State
{
    [Header("Stats")]
    public float moveSpeed = 3f;
    public float roamRadius = 10f;
    public float roamTime = 3f;

    private Vector2 roamTarget;
    private float roamTimer;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Roaming();
    }
    void Roaming()
    {
        roamTimer -= Time.deltaTime;
        if (roamTimer <= 0 || Vector2.Distance(transform.position, roamTarget) < 0.5f)
        {
            PickNewRoamTarget();
        }
        Vector2 direction = (roamTarget - new Vector2(transform.position.x, transform.position.y)).normalized;
        if (rb.linearVelocity.magnitude <= moveSpeed)
        {
            rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
        }
    }
    void PickNewRoamTarget()
    {
        Vector2 randomPoint = Random.insideUnitCircle * roamRadius;
        roamTarget = new Vector2(transform.position.x + randomPoint.x, transform.position.y + randomPoint.y);
        roamTimer = roamTime;
    }
}