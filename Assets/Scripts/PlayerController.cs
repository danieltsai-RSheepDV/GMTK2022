using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Debug.Log(ray);
        Vector3 mousePos = new Vector3();
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mousePos = raycastHit.point;
            // Debug.Log($"Mouse: {mousePos}");
        }
        
        Vector2 lookDirection = new Vector3(mousePos.z - transform.position.z, mousePos.x - transform.position.x, 0);
        Debug.Log(lookDirection);
        transform.up = lookDirection;
    }

    private void FixedUpdate()
    {
        float vert = Input.GetAxisRaw("Vertical");
        float hori = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(hori, 0, vert) * moveSpeed;

    }

}
