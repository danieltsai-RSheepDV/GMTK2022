using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Dice die;
    [SerializeField] private Vector2 dir;

    private float force = 2f;
    
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
            Destroy(p.gameObject);
            if (dir == Vector2.left)
            {
                die.rb.AddTorque(Vector3.forward * force, ForceMode.Impulse);
            }if (dir == Vector2.right)
            {
                die.rb.AddTorque(Vector3.back * force, ForceMode.Impulse);
            }if (dir == Vector2.up)
            {
                die.rb.AddTorque(Vector3.right * force, ForceMode.Impulse);
            }if (dir == Vector2.down)
            {
                die.rb.AddTorque(Vector3.left * force, ForceMode.Impulse);
            }
        }
    }
}
