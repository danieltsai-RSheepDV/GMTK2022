using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject endEnemy;
    [SerializeField] private GameObject enemy;
    [Header("Settings")]
    [Range(0, 5)]
    [SerializeField] int difficulty = 1;

    [SerializeField] Vector4[] enemyRosters;

    public int enemiesLeft = 0;
    

    void Start()
    {
        //enemyRosters = new Vector4[] {  // # of triangles, squares, pentagons, and hexagons
        //    new Vector4(2, 0, 0, 0),
        //    new Vector4(3, 0, 1, 0),
        //    new Vector4(3, 0, 2, 1),
        //    new Vector4(4, 0, 4, 0),
        //    new Vector4(0, 1, 0, 2),
        //    new Vector4(3, 3, 2, 1)
        //};
        
        // SpawnWave();
    }

    // At a scaling interval, spawn a scaling number of random enemies
    void Update()
    {
        
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

    public void SpawnWave(int diff)
    {
        difficulty = diff;
        SpawnWave();
    }

    // Spawns enemies in random locations in a 10x10 square around the center of the stage
    void SpawnEnemy(int type)
    {
        float randx = Random.Range(-10f, 10f);
        float randy = Random.Range(-10f, 10f);

        Vector3 offset = new Vector3(randx, randy, 0);

        GameObject mob = Instantiate(enemy, transform.position + offset, Quaternion.identity, GameManager.twoD.transform);
        mob.GetComponent<Enemy>().SetEnemyType(type);

        enemiesLeft++;
    }

    public void DecrementEnemyCounter()
    {
        enemiesLeft--;
        if (enemiesLeft == 0)
        {
            Instantiate(endEnemy, transform.position, Quaternion.identity, GameManager.twoD.transform);
        }
    }
}
