using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Projectile
{
    float lifetime;
    GameObject[] targets;
    GameObject activeTarget;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        try
        {
            activeTarget = targets[0];
        } catch
        {
            Debug.Log("No initial target.");
        }
    }

    // Update is called once per frame
    protected new void Update()
    {
        lifetime += Time.deltaTime;
        GetTarget();
        if (activeTarget != null)
        {
            Vector3 targAngle = new Vector3(activeTarget.transform.position.x - transform.position.x, activeTarget.transform.position.y - transform.position.y, 0);
            transform.up = Vector3.Lerp(transform.up, targAngle, 0.3f);
        }
        transform.position += transform.up * projectileSpeed * Time.deltaTime;
        if (lifetime > lifespan)
        {
            Destroy(gameObject);
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
}
