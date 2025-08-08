using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnRadius = 15f;
    
    [SerializeField] GameObject enemyPrefab;
    
    [SerializeField] float spawnRate = 5f;
    [SerializeField] float spawnRateIncrease = 2f;

    float nextSpawnTime;
    
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
        spawnRate += spawnRateIncrease * Time.deltaTime;
    }
    
    void SpawnEnemy()
    {
        Vector2 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.transform.up = (Vector3)spawnPosition.normalized;
    }
}
