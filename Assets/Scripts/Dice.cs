using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public GameObject face;
    Vector3 lookForward;
    Vector3 lookUpward;
    
    public Vector3Int DirectionValues;
    private Vector3Int OpposingDirectionValues;

    private Rigidbody rb;

    readonly List<string> FaceRepresent = new() {"", "1", "2", "3", "4", "5", "6"};
    
    void Start()
    {
        OpposingDirectionValues = 7 * Vector3Int.one - DirectionValues;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.hasChanged)
        {
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
    
    Vector3 getDirection(Vector3 relative)
    {
        if (Vector3.Dot(relative, transform.right) > 0.9f)
        {
            Debug.Log(relative);
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

    void launch(Vector2 direction, float power)
    {
        rb.AddForce(new Vector3(direction.x, 5, direction.y) * power, ForceMode.Impulse);
        rb.AddTorque(Vector3.forward, ForceMode.Impulse);
    }


}