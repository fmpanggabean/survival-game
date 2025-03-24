using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawnerScript : MonoBehaviour
{
    GameObject enemyPrefab;
    public Transform walkableGrid;
    public Transform blockedGrid;
    Tilemap[] excludedTilemaps;
    Tilemap[] availableTilemaps;
    Vector2Int spawnAreaMin = new Vector2Int(-12, -12);
    Vector2Int spawnAreaMax = new Vector2Int(12, 10);
    Vector3Int cellPos;
    Vector3 spawnPos = Vector3.zero;

    private void Start()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Hexagon Enemy");
        excludedTilemaps = blockedGrid.GetComponentsInChildren<Tilemap>();
        availableTilemaps = walkableGrid.GetComponentsInChildren<Tilemap>();

        StartCoroutine(ConstantSpawn());
    }

    private IEnumerator ConstantSpawn()
    {
        while(true)
        {
            spawnPos = Vector3.zero;
            while (spawnPos == Vector3.zero)
            {
                Debug.Log("Getting Pos");
                spawnPos = GetSpawnPos();
            }
            Debug.Log("Spawning");
            SpawnEnemy();
            yield return new WaitForSeconds(1);
        }
    }

    Vector3 GetSpawnPos()
    {
        int x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        int y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        cellPos = new Vector3Int(x, y, 0);

        bool isBlocked = false;
        bool isWalkable = false;

        foreach(var map in availableTilemaps)
        {
            if(map.HasTile(cellPos))
            {
                isWalkable = true;
                break;
            }
        }

        foreach (var map in excludedTilemaps)
        {
            if (map.HasTile(cellPos))
            {
                isBlocked = true;
                break;
            }
        }

        if (isWalkable && !isBlocked)
        {
            return excludedTilemaps[0].CellToWorld(cellPos) + new Vector3(0.5f, 0.5f, 0);
        }
        return Vector3.zero;
    }

    void SpawnEnemy()
    {
        GameObject obj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 1000;
    }
}
