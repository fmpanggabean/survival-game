using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxEnemies = 10;
    
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (spawnedEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomScreenPosition();
        Vector3 finalPosition = new Vector3(spawnPosition.x, spawnPosition.y, 0); // Ensure Z = 0

        GameObject newEnemy = Instantiate(enemyPrefab, finalPosition, Quaternion.identity);
        spawnedEnemies.Add(newEnemy);

        Debug.Log("Enemy Spawned at: " + finalPosition);
    }

    private Vector2 GetRandomScreenPosition()
    {
        Camera mainCamera = Camera.main;
        float screenX = Random.Range(0f, Screen.width);
        float screenY = Random.Range(0f, Screen.height);
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(new Vector2(screenX, screenY));
        return worldPosition;
    }
}

