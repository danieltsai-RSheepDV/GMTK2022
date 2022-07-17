using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject particles;
    
    public GameObject face;
    Vector3 lookForward;
    Vector3 lookUpward;

    private bool inAir = false;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 lastVelocity = Vector3.zero;

    public Dictionary<Vector3, int> values = new()
    {
        {Vector3.up, 2},
        {Vector3.down, 4},
        {Vector3.forward, 6},
        {Vector3.back, 5},
        {Vector3.right, 1},
        {Vector3.left, 3},
    };

    [NonSerialized] public Rigidbody rb;

    readonly List<string> FaceRepresent = new() {"", "1", "2", "3", "4", "5", "6"};
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.hasChanged)
        {
            if (inAir)
            {
                acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;
                lastVelocity = rb.velocity;
                if (acceleration.magnitude < 0.1f)
                {
                    inAir = false;
                    GameManager.nextWave(values[getDirection(Vector3.up)]);
                }
            }
            
            lookUpward = getDirection(Vector3.up);
            
            face.transform.localPosition = 2.0001f * lookUpward;
            Vector3 rotation = Quaternion.Inverse(transform.rotation).eulerAngles;
            float x = Mathf.Round(rotation.x/90) * 90;
            float y = Mathf.Round(rotation.y/90) * 90;
            float z = Mathf.Round(rotation.z/90) * 90;
            face.transform.localRotation = Quaternion.Euler(x, y, z);

            transform.hasChanged = false;
        }
    }
    
    public Vector3 getDirection(Vector3 relative)
    {
        if (Vector3.Dot(relative, transform.right) > 0.9f)
        {
            return Vector3.right;
        }
        else if (Vector3.Dot(relative, -transform.right) > 0.9f)
        {
            return Vector3.left;
        }
        else if (Vector3.Dot(relative, transform.up) > 0.9f)
        {
            return Vector3.up;
        }
        else if (Vector3.Dot(relative, -transform.up) > 0.9f)
        {
            return Vector3.down;
        }
        else if (Vector3.Dot(relative, transform.forward) > 0.9f)
        {
            return Vector3.forward;
        }
        else if (Vector3.Dot(relative, -transform.forward) > 0.9f)
        {
            return Vector3.back;
        }
        else
        {
            return relative;
        }
    }

    public void launch(Vector2 direction, float power)
    {
        rb.AddForce(new Vector3(direction.x, 5, direction.y) * power, ForceMode.Impulse);
        rb.AddTorque((new Vector3(Random.value, Random.value,Random.value)).normalized * power, ForceMode.Impulse);

        inAir = true;
    }

    public void launch()
    {
        rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
    }

    public void shatter()
    {
        model.SetActive(false);
        particles.SetActive(true);
    }
}