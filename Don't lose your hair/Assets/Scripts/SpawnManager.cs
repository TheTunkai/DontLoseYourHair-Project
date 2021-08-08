using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Vector2 spawnPosJump = new Vector2(20f, -2f);
    [SerializeField] private Vector2 spawnPosCrouch = new Vector2(20f, 0.8f);
    [SerializeField] private Vector2 spawnPosFlyingEnemy = new Vector2(19f, 3f);
    [SerializeField] private float spawnStartTime = 2f;
    [SerializeField] private float spawnRate = 3f;
    public static int enemyCount = 0;

    [SerializeField] private bool enemySpawnStarted = false;

    public GameObject[] obstaclePrefabs;
    public GameObject[] enemyPrefabs;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.startEnemyWave += startEnemySpawning;

        InvokeRepeating("SpawnObstacle", spawnStartTime, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle() // spawns a random obstacle
    {
        Vector2 spawnPos = new Vector2();

        int index = UnityEngine.Random.Range(0, obstaclePrefabs.Length);

        if (obstaclePrefabs[index].name == "Obstacle_Jump" || obstaclePrefabs[index].name == "Obstacle_Shoot")
        {
            spawnPos = spawnPosJump;
        }
        else if (obstaclePrefabs[index].name == "Obstacle_Crouch")
        {
            spawnPos = spawnPosCrouch;
        }

        Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
    }

    void SpawnEnemy() // spawn random enemy
    {

        Vector2 spawnPos = new Vector2();

        int index = UnityEngine.Random.Range(0, enemyPrefabs.Length);

        if (enemyPrefabs[index].name == "Enemy_Flying")
        {
            spawnPos = spawnPosFlyingEnemy;
        }

        Instantiate(enemyPrefabs[index], spawnPos, enemyPrefabs[index].transform.rotation);

        enemyCount++;

    }

    private void startEnemySpawning()
    {
        if (!enemySpawnStarted)
        {
            StartCoroutine(SpawningEnemies());
        }
    }

    IEnumerator SpawningEnemies() // starts enemy spawning with given rate and number of enemies
    {
        while(enemyCount < 8)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(4);
        }

        enemyCount = 0;

    }
}
