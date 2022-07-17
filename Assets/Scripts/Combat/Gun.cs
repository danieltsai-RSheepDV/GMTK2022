using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        if (parent == null)
        {
            parent = transform.parent.transform.parent;
        }
    }

    // Fires given ammo and position
    public void Shoot(Object ammo)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Triangle Basic Shot");
        Debug.Log("Spawning");
        GameObject shot = ((GameObject) Instantiate(ammo, transform));
        shot.transform.SetParent(GameManager.twoD.transform);
        shot.SetActive(true);
    }

}
