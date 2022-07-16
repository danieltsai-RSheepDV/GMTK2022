using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDamage;
    [SerializeField] int lifespan = 8;
    int lifetime;

    // Start is called before the first frame update
    void Start()
    {
        lifetime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime++;
        transform.position += transform.up * projectileSpeed;
        if (lifetime > lifespan * 60)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


}
