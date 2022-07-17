using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class EndEnemy : MonoBehaviour
{
    [SerializeField] int health = 5;
    VFXController vfx;

    // Start is called before the first frame update
    void Start()
    {
        vfx = GameObject.Find("VFX Controller").GetComponent<VFXController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Projectile p = col.gameObject.GetComponent<Projectile>();
        if ((p && p.tags.Contains("Player")))
        {
            try
            {
                Destroy(p.gameObject);
            }
            catch (Exception e) { }

            health--;
            vfx.PlayAccumulate(5 - health);
            if (health <= 0)
            {
                vfx.PlayBurst();
                GameManager.dieFace.SetActive(false);
                GameManager.die.launch(col.transform.up, 5f);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        PlayerController pl = col.gameObject.GetComponent<PlayerController>();
        if (pl)
        {
            health--;
            vfx.PlayAccumulate(5 - health);
            if (health <= 0)
            {
                vfx.PlayBurst();
                GameManager.dieFace.SetActive(false);
                GameManager.die.launch(col.transform.up, 5f);
                Destroy(gameObject);
            }
        }
    }
}
