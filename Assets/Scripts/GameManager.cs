using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject DieFace;
    public static GameObject dieFace;
    [SerializeField] private Dice Die;
    public static Dice die;
    [SerializeField] private EnemySpawner EnemySpawner;
    public static EnemySpawner enemySpawner;
    [SerializeField] private GameObject TwoD;
    public static GameObject twoD;
    
    static private FMOD.Studio.EventInstance instance;
    public FMODUnity.EventReference fmodEvent;
    
    // Start is called before the first frame update
    void Awake()
    {
        dieFace = DieFace;
        die = Die;
        enemySpawner = EnemySpawner;
        
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    public static void nextWave(int i)
    {
        enemySpawner.SpawnWave(i);
        dieFace.SetActive(true);
        instance.setParameterByName("ParameterName", i - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
