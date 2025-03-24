using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int count;
    [SerializeField] private List<GameObject> generatedObjects;

    void Awake()
    {
        generatedObjects = new List<GameObject>();
        GeneratePooledObject();
        DeactivateAllObjects();
    }

    private void DeactivateAllObjects()
    {
        foreach (GameObject obj in generatedObjects)
        {
            obj.SetActive(false);
        }
    }

    private void GeneratePooledObject()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        //GameObject[] enemies = Resources.LoadAll<GameObject>("Prefabs/Enemies");

        for (int i = 0; i < count; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            generatedObjects.Add(bullet);
        }

        //for (int j = 0; j < enemies.Length; j++)
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        generatedObjects.Add(Instantiate(enemies[j], transform));
        //    }
        //}        
    }


    public T Request<T>() where T : IPoolObject
    {
        foreach (GameObject obj in generatedObjects)
        {
            T pooledObject = obj.GetComponent<T>();

            if (pooledObject == null) continue;

            if (obj.activeInHierarchy == false)
            {
                pooledObject.Activate();                
                return (T)pooledObject;
            }
        }

        return default;
    }
}
