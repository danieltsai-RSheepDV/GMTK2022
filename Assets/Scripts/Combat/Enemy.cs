using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] Transform target;
    [SerializeField] int health = 3;
    [Header("Weapons")]
    [SerializeField] Gun gun;
    [SerializeField] Object bolt;
    [SerializeField] Object turret;
    [SerializeField] Object deflector;
    enum PlayerType
    {
        triangle,
        square,
        pentagon,
        hexagon
    }
    [SerializeField] PlayerType currentType = PlayerType.triangle;
    int cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
        transform.position += transform.up * moveSpeed;
        if (cooldown > 0)
        {
            cooldown--;
        } else
        {
            Attack();
        }
    }

    void Attack()
    {
        switch (currentType)
        {
            case PlayerType.triangle:
                gun.Shoot(bolt);
                cooldown = 60;
                break;
            case PlayerType.square:
                Object.Instantiate(turret, transform.position, transform.rotation);
                cooldown = 360;
                break;
            case PlayerType.pentagon:
                Object.Instantiate(deflector, transform.position + transform.up * 2, transform.rotation);
                cooldown = 90;
                break;
            case PlayerType.hexagon:

                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile p = collision.gameObject.GetComponent<Projectile>();
        if (p && p.tags.Contains("Player"))
        {
            health--;
        }
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
}
