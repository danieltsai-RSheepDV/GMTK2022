using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float zaxis = Input.GetAxis("Horizontal");
        float xaxis = Input.GetAxis("Vertical");

        // flipping code, activated by keypress for now (not NIS cuz dont matter for now)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 1, 0) * 5f, ForceMode.Impulse);  // Upward force
            rb.AddTorque(Vector3.forward, ForceMode.Impulse);  // Angular force
        }
        transform.position += new Vector3(xaxis, 0, zaxis) * 0.02f;

    }
}
