using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : Condition
{
    [Header("Stats")]
    public float maxDetectionRange = 5f;
    public float minDetectionRange = 5f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override bool ConditionCheck()
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
        if (distance >= minDetectionRange && distance <= maxDetectionRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}