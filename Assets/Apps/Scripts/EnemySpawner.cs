using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnRadius = 15f;

    [SerializeField] private Transform player;

    [SerializeField] private int maxEnemies = 10;
    private int currentEnemies = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemies < maxEnemies)
            {
                Spawn();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn()
    {
        int safety = 0;
        Vector3 spawnPos = Vector3.zero;

        while (safety < 20)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            spawnPos = player.position + new Vector3(randomOffset.x, randomOffset.y);
            spawnPos.z = 0f;

            if (Vector2.Distance(spawnPos, player.position) < 3f)
            {
                safety++;
                continue;
            }

            RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, 1f);
            if (hit.collider != null)
            {
                GameObject surface = hit.collider.gameObject;

                if (surface.CompareTag("floor") && !Physics2D.OverlapCircle(spawnPos, 0.5f, LayerMask.GetMask("Barrier")))
                {
                    break;
                }
            }

            safety++;
        }

        Enemy enemy = FindObjectOfType<ObjectPool>().Request<Enemy>();
        if (enemy == null) return;

        enemy.transform.position = spawnPos;
        enemy.transform.rotation = Quaternion.identity;

        enemy.OnDeath += () => currentEnemies--;
        currentEnemies++;

        enemy.GetComponent<Enemy>().OnDeath += () => currentEnemies--;
    }
}
