using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] int lifespan = 12;
    int lifetime;

    [SerializeField] Gun gun;
    [SerializeField] Object bolt;
    [SerializeField] int framesBetweenShots = 12;
    [SerializeField] bool upgraded = false;
    int cooldown;
    GameObject[] targets;
    GameObject activeTarget;

    // Start is called before the first frame update
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        lifetime = 0;
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime++;
        // Self destruct after lifespan seconds, assuming 60 FPS
        if (lifetime > lifespan * 60)
        {
            Destroy(gameObject);
        }
        if (upgraded)
        {
            // Reacquire target every 60 frames
            if (lifetime % 30 == 0)
                GetTarget();
            // Attack active target
            if (activeTarget != null)
            {
                transform.up = new Vector3(activeTarget.transform.position.x - transform.position.x, activeTarget.transform.position.y - transform.position.y, 0);
                Shoot();
            }
        } else
        {
            Shoot();
        }
    }

    // sets target if one was found, rescans otherwise
    void GetTarget()
    {
        if (activeTarget != null)
            return;
        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                activeTarget = target;
                return;
            }
        }
        targets = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Shoot()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
        else
        {
            gun.Shoot(bolt);
            cooldown = framesBetweenShots;
        }
    }
}
