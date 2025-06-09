using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject objectToSpawn; // Prefab to spawn
    public int numberOfObjects = 8;  // How many objects to spawn around
    public float innerRadius;   // Minimum distance from center
    public float outerRadius;   // Maximum distance from center

    // Call this method to spawn objects randomly in a ring around this GameObject
    [ContextMenu("Spawn Objects Around")]
    public void SpawnObjectsAround()
    {
        if (objectToSpawn == null || numberOfObjects <= 0) return;
        if (innerRadius < 0f) innerRadius = 0f;
        if (outerRadius <= innerRadius) outerRadius = innerRadius + 0.1f;

        Vector3 center = transform.position;

        float angleStep = 2f * Mathf.PI / numberOfObjects;
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * angleStep;
            float radius = Random.Range(innerRadius, outerRadius);
            Vector3 spawnPos = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
        }
    }
}
