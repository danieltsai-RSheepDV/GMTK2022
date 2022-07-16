using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Object enemy;
    [Header("Settings")]
    [Range(0, 5)]
    [SerializeField] int difficulty = 1;

    [SerializeField] Vector4[] enemyRosters;

    int enemiesToSpawn = 0;
    int counter = 0;
    int baseInterval = 5;  // In seconds
    

    void Start()
    {
        enemyRosters = new Vector4[] {  // # of triangles, squares, pentagons, and hexagons
            new Vector4(2, 0, 0, 0),
            new Vector4(3, 0, 1, 0),
            new Vector4(3, 0, 2, 1),
            new Vector4(4, 0, 4, 0),
            new Vector4(0, 1, 0, 2),
            new Vector4(3, 3, 2, 1)
        };
    }

    // At a scaling interval, spawn a scaling number of random enemies
    void Update()
    {
        if (counter * Time.deltaTime >= baseInterval)  // Interval to spawn enemies
        {
            SpawnWave();
            counter = 0;
        }
        counter++;
    }

    // Spawns a wave of enemies based on difficulty, according to the enemy roster of given difficulty
    void SpawnWave()
    {
        Vector4 roster = enemyRosters[(int)Mathf.Min(enemyRosters.Length - 1, difficulty)];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < roster[i]; j++)
            {
                SpawnEnemy(i);
            }
        }
            
    }

    // Spawns enemies in random locations in a 10x10 square around the center of the stage
    void SpawnEnemy(int type)
    {
        float randx = Random.Range(-10f, 10f);
        float randy = Random.Range(-10f, 10f);

        Vector3 offset = new Vector3(randx, randy, 0);

        GameObject mob = (GameObject)Instantiate(enemy, transform.position + offset, Quaternion.identity);
        mob.GetComponent<Enemy>().SetEnemyType(type);
    }
}
