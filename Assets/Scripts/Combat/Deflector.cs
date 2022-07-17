using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflector : MonoBehaviour
{
    [SerializeField] private string checkTag;
    
    [SerializeField] int frames = 3;
    [SerializeField] Gun gun;
    [SerializeField] GameObject chaser;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter >= frames)
            Destroy(gameObject);
        counter++;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("test");
        Projectile p = col.gameObject.GetComponent<Projectile>();
        if (p && p.tags.Contains(checkTag))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Pentagon Successful Deflect");
            Debug.Log("test2");
            Destroy(col.gameObject);
            gun.Shoot(chaser);
        }
    }
}
