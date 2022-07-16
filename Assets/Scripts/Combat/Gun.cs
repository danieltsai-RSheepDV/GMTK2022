using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform parentCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Fires given ammo and position
    public void Shoot(Object ammo, Vector3 position, Quaternion rotation)
    {
        Debug.Log("Spawning");
        Object.Instantiate(ammo, position, rotation, parentCam);
    }

}
