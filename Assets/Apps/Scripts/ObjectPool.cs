using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int bulletCount = 20;
    [SerializeField] private int enemyCount = 10;

    private List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        GeneratePool();
        DeactivateAll();
    }

    private void GeneratePool()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");

        if (bulletPrefab == null || enemyPrefab == null)
        {
            return;
        }

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    private void DeactivateAll()
    {
        foreach (GameObject obj in pooledObjects)
        {
            obj.SetActive(false);
        }
    }

    public T Request<T>() where T : class, IPoolObject
    {
        foreach (GameObject obj in pooledObjects)
        {
            T pooledObject = obj.GetComponent<T>();
            if (pooledObject == null) continue;

            if (!obj.activeInHierarchy)
            {
                pooledObject.Activate();
                return pooledObject;
            }
        }

        return null;
    }
}
