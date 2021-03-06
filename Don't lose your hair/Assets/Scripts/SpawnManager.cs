using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Vector2 spawnPosJump = new Vector2(20f, -2f);
    [SerializeField] private Vector2 spawnPosCrouch = new Vector2(20f, 0f);
    [SerializeField] private Vector2 spawnPosShoot = new Vector2(20f, 0.55f);
    [SerializeField] private Vector2 spawnPosFlyingEnemy = new Vector2(19f, 3f);
    [SerializeField] private float obstacleSpawnRate = 3f;
    [SerializeField] private float difficultyFactor = 0.1f;
    [SerializeField] private float enemySpawnRate = 8f;
    [SerializeField] private int enemiesToSpawn = 1;
    [SerializeField] private int enemyWaveTracker = 1;

    [SerializeField] private bool enemySpawnStarted = false;

    public GameObject[] obstaclePrefabs;
    public GameObject[] enemyPrefabs;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.startEnemyWave += OnEnemySpawnSignal;
        GameManager.instance.playerLost += OnPlayerDie;

        if (MainManager.instance.difficulty == Difficulty.Easy)
        {
            obstacleSpawnRate = 4f;
        }
        else if (MainManager.instance.difficulty == Difficulty.Medium)
        {
            obstacleSpawnRate = 3f;
        }
        else if (MainManager.instance.difficulty == Difficulty.Hard)
        {
            obstacleSpawnRate = 2f;
        }

        StartCoroutine(SpawningObstacles());

        
        
    }

    private void OnPlayerDie() // stops coroutines when player lost
    {
        StopAllCoroutines();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle() // spawns a random obstacle
    {
        Vector2 spawnPos = new Vector2();

        int index = UnityEngine.Random.Range(0, obstaclePrefabs.Length);

        if (obstaclePrefabs[index].name == "Obstacle_Jump")
        {
            spawnPos = spawnPosJump;
        }
        else if(obstaclePrefabs[index].name == "Obstacle_Shoot")
        {
            spawnPos = spawnPosShoot;
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

        GameManager.instance.enemyCount++;

    }

    private void OnEnemySpawnSignal() // is called, when condition for enemies is met
    {
        if (!enemySpawnStarted)
        {
            enemySpawnStarted = true;
            

            StartCoroutine(SpawningEnemies(enemyWaveTracker));
        }
    }

    IEnumerator SpawningEnemies(int waveNumber) // starts enemy spawning with given rate and number of enemies
    {
        if (MainManager.instance.difficulty == Difficulty.Easy)
        {
            enemiesToSpawn = waveNumber;
            enemySpawnRate = 8f;

            if(obstacleSpawnRate - obstacleSpawnRate * difficultyFactor >= 2)
            {
                obstacleSpawnRate = obstacleSpawnRate - obstacleSpawnRate * difficultyFactor;
            }
        }
        else if (MainManager.instance.difficulty == Difficulty.Medium)
        {
            enemiesToSpawn = waveNumber + 1;
            enemySpawnRate = 6f;

            if(obstacleSpawnRate - obstacleSpawnRate * difficultyFactor >= 1.5)
            {
                obstacleSpawnRate = obstacleSpawnRate - obstacleSpawnRate * difficultyFactor;
            }
        }
        else if (MainManager.instance.difficulty == Difficulty.Hard)
        {
            enemiesToSpawn = waveNumber * 2;
            enemySpawnRate = 4f;

            if(obstacleSpawnRate - obstacleSpawnRate * difficultyFactor >= 1)
            {
                obstacleSpawnRate = obstacleSpawnRate - obstacleSpawnRate * difficultyFactor;
            }
        }

        while(GameManager.instance.enemyCount < enemiesToSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(enemySpawnRate);
        }
        enemySpawnStarted = false;
        enemyWaveTracker += 1;
        GameManager.instance.enemyCount = 0;

    }

    IEnumerator SpawningObstacles() // spawns obstacles as long as game isn't over
    {
        while (!GameManager.instance.gameOver)
        {
            yield return new WaitForSeconds(obstacleSpawnRate);
            SpawnObstacle();
        }
    }
}
