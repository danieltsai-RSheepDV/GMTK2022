using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Object enemy;
    [Header("Settings")]
    [Range(0.5f, 5f)]
    [SerializeField] float difficulty = 1;

    int enemiesToSpawn = 0;
    int counter = 0;
    int baseInterval = 5;  // In seconds


    void Start()
    {
        
    }

    // At a scaling interval, spawn a scaling number of random enemies
    void Update()
    {
        if (counter * Time.deltaTime >= baseInterval / difficulty)  // Interval to spawn enemies
        {
            enemiesToSpawn = Mathf.RoundToInt(Random.Range(1f + difficulty, 1f + difficulty * 2));
            for (int i = 0; i < enemiesToSpawn; i++)
                SpawnEnemy();
            counter = 0;
        }
        counter++;
    }

    // Spawns enemies in random locations in a 10x10 square around the center of the stage
    void SpawnEnemy()
    {
        float randx = Random.Range(-10f, 10f);
        float randy = Random.Range(-10f, 10f);

        Vector3 offset = new Vector3(randx, randy, 0);

        Instantiate(enemy, transform.position + offset, Quaternion.identity);
    }
}
