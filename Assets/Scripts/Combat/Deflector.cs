using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflector : MonoBehaviour
{
    [SerializeField] int frames = 3;
    [SerializeField] Gun gun;
    [SerializeField] Object bolt;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyProjectile")
        {
            Destroy(collision.collider.gameObject);
            gun.Shoot(bolt);
        }
    }
}
