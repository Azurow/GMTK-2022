using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public AnimationCurve spawnRate;
    public float spawnTime;
    private float spawnCountdown;

    void Start()
    {
        spawnCountdown = spawnTime;
    }

    void Update()
    {
        spawnTime = spawnRate.Evaluate(Time.timeSinceLevelLoad);
        spawnCountdown -= Time.deltaTime;
        if(spawnCountdown <= 0)
        {
            spawnEnemy();
            spawnCountdown = spawnTime;
        }
    }

    void spawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
    }
}
