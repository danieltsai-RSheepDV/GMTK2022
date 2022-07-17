using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

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
        twoD = TwoD;
        
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    public static void nextWave(int i)
    {
        enemySpawner.SpawnWave(i);
        dieFace.GetComponent<MeshRenderer>().enabled = true;
        instance.setParameterByName("Face", i - 1);
        twoD.SetActive(true);
    }
    
    public static void tutorial()
    {
        dieFace.SetActive(true);
        instance.setParameterByName("Face", 0);
        twoD.SetActive(true);
    }

    private void OnDisable()
    {
        instance.stop(STOP_MODE.IMMEDIATE);
        instance.release();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
