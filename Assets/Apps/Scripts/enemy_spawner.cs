using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class enemy_spawner : MonoBehaviour
{
    ObjectPool pool;
    // Start is called before the first frame update
    void Start()
    {
        pool = GetComponent<ObjectPool>();
        StartCoroutine(spawnIEnumerator());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void spawn()
    {
        pool.Request<enemy>();
    }
    private IEnumerator spawnIEnumerator()
    {
        while (true)
        {
            spawn();
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
    }
}
