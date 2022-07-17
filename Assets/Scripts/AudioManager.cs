using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [SerializeField] bool play;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            PlaySound();
            play = false;
        }
    }

    void PlaySound()
    {
        RuntimeManager.PlayOneShot("event:/SFX/Charge");
    }
}
