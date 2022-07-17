using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    [SerializeField] VisualEffect accumulate;
    [SerializeField] VisualEffect burst;
    [SerializeField] bool play;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            counter++;
            PlayAccumulate(counter);
            play = false;
            if (counter > 5)
                counter = 0;
        }
    }

    public void PlayAccumulate(int stackingCharge)
    {
        accumulate.SetInt(Shader.PropertyToID("StackingCharge"), stackingCharge);
        accumulate.Play();
    }

    public void PlayBurst()
    {
        burst.Play();
    }

}
