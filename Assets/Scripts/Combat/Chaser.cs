using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 20;
    [SerializeField] float projectileDamage = 1;
    [SerializeField] int lifespan = 8;
    float lifetime;
    GameObject[] targets;
    GameObject activeTarget;

    public List<String> tags = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        lifetime = 0;
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
    void Update()
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
