using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class ExplodeBulletSpawner : MonoBehaviour
{
    public GameObject Bullet;
    public float initialSpawnRate = 0.5f;  
    public float minSpawnRate = 0.1f;     
    public float spawnRateDecrease = 0.01f;
    private float currentSpawnRate;
    private float timer = 0f;

    public float widthOffset = 10f;
    public int initialBulletsPerSpawn = 2;
    public int maxBulletsPerSpawn = 10;  
    public float bulletsIncreaseInterval = 10f;
    private int currentBulletsPerSpawn;

    private float timeSinceLastBulletIncrease = 0f;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        currentBulletsPerSpawn = initialBulletsPerSpawn;
        spawnBullet();
    }

    void Update()
    {
        if (timer < currentSpawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnBullet();
            timer = 0f;
        }

        if (currentSpawnRate > minSpawnRate)
        {
            currentSpawnRate -= spawnRateDecrease * Time.deltaTime;
        }

        timeSinceLastBulletIncrease += Time.deltaTime;
        if (timeSinceLastBulletIncrease >= bulletsIncreaseInterval && currentBulletsPerSpawn < maxBulletsPerSpawn)
        {
            currentBulletsPerSpawn++;
            timeSinceLastBulletIncrease = 0f;
        }
    }
    
    void spawnBullet()
    {
        for (int i = 0; i < currentBulletsPerSpawn; i++)
        {
            float leftmostPoint = transform.position.x - widthOffset;
            float rightmostPoint = transform.position.x + widthOffset;
            float randomX = Range(leftmostPoint, rightmostPoint);
            Instantiate(Bullet, new Vector3(randomX, transform.position.y, 0), Quaternion.identity);
        }
    }
}
