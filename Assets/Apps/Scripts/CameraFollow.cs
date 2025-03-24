using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is missing! Camera will not move.");
            return;
        }

        Vector3 targetPosition = player.position + offset;
        targetPosition.z = -10; // Force Z to stay at -10
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}

