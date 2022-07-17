using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject DieFace;
    public static GameObject dieFace;
    [SerializeField] private Dice Die;
    public static Dice die;
    [SerializeField] private EnemySpawner EnemySpawner;
    public static EnemySpawner enemySpawner;
    
    // Start is called before the first frame update
    void Awake()
    {
        dieFace = DieFace;
        die = Die;
        enemySpawner = EnemySpawner;
    }

    public static void nextWave(int i)
    {
        enemySpawner.SpawnWave(i);
        dieFace.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
