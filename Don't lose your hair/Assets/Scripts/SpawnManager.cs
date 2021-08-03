using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Vector2 spawnPos = new Vector2(19f, -1f);
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

    void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefabs.Length);

        Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
    }
}
