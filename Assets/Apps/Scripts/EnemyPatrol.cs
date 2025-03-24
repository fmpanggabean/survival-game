using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public Transform[] waypoints; 
    public float enemySpeed = 2.0f; 
    public float waitTime = 2.0f; 

    private int currentWaypointIndex = 0; 
    private bool isWaiting = false; 



    void Start()
    {
        if (waypoints.Length > 0)
        {
            StartCoroutine(Patrol());
        }
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!isWaiting)
            {
                MoveTowardsWaypoint();
                if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
                {
                    StartCoroutine(enemyWaiting());
                }
            }
            yield return null; 
        }
    }

    void MoveTowardsWaypoint()
    {
        if (waypoints.Length == 0)

            return;

        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        float step = enemySpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, step);

        Flips(direction);
    }

    void Flips(Vector3 direction)
    {
        if (direction.magnitude > 0.01f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

    }
    IEnumerator enemyWaiting()
    {
        isWaiting = true;

        yield return new WaitForSeconds(waitTime); 
        isWaiting = false;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 

    }
}
