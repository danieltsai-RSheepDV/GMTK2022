using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float projectileDamage;
    [SerializeField] protected int lifespan = 8;
    protected float lifetime;

    public List<String> tags = new List<string>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        lifetime = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        lifetime += Time.deltaTime;
        transform.position += transform.up * projectileSpeed * Time.deltaTime;
        if (lifetime > lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
