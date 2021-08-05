using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Vector2 spawnPosJump = new Vector2(20f, -2f);
    public Vector2 spawnPosCrouch = new Vector2(20f, 0.8f);
    public float spawnStartTime = 2f;
    public float spawnRate = 3f;


    // Start is called before the first frame update
    void Start()
    {
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

        if (obstaclePrefabs[index].name == "Obstacle_Jump")
        {
            spawnPos = spawnPosJump;
        }
        else if (obstaclePrefabs[index].name == "Obstacle_Crouch")
        {
            spawnPos = spawnPosCrouch;
        }

        Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
    }
}
