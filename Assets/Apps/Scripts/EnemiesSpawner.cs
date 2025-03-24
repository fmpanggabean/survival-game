using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public float spawnDelay;
    private ObjectPool pool;
    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemies()
    {
        Enemies b = pool.Request<Enemies>();
        b.SetPosition(new Vector3(Random.Range(-7,6.5f),Random.Range(4.3f,-9),0f));
    }
}
