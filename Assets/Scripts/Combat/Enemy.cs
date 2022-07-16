using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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
    float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        // currentType = (PlayerType) Mathf.RoundToInt(UnityEngine.Random.Range(0f, 3f));
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
        transform.position += transform.up * moveSpeed * Time.deltaTime;
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
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
                ((GameObject) Instantiate(turret, transform.position, transform.rotation)).SetActive(true);
                cooldown = 360;
                break;
            case PlayerType.pentagon:
                ((GameObject) Instantiate(deflector, transform.position, transform.rotation)).SetActive(true);
                cooldown = 90;
                break;
            case PlayerType.hexagon:

                break;
        }
    }

    // Converts int to the enum: tri - 0, sq - 1, pen - 2, hex - 3
    public void SetEnemyType(int type)
    {
        currentType = (PlayerType)type;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Projectile p = col.gameObject.GetComponent<Projectile>();
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
