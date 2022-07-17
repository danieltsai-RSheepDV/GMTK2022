using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class EndEnemy : MonoBehaviour
{
    [SerializeField] int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Projectile p = col.gameObject.GetComponent<Projectile>();
        if (p && p.tags.Contains("Player"))
        {
            health--;
            if (health <= 0)
            {
                GameManager.dieFace.SetActive(false);
                GameManager.die.launch(col.transform.up, 5f);
                Destroy(gameObject);
            }
        }
    }
}
