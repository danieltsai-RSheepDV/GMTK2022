using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Range(0f, 30f)]
    [SerializeField] float moveSpeed = 1f;
    [Range(1f, 100f)]
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] Camera cam;

    [Header("Weapons")]
    [SerializeField] Gun gun;
    [SerializeField] Object bolt;
    [SerializeField] Object turret;

    enum PlayerType {
        triangle,
        square,
        pentagon,
        hexagon
    }
    [SerializeField] PlayerType currentType = PlayerType.triangle;


    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * moveSpeed * Time.deltaTime;
        // Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //
        // // Debug.Log(ray);
        // Vector3 mousePos = new Vector3();
        // if (Physics.Raycast(ray, out RaycastHit raycastHit))
        // {
        //     mousePos = raycastHit.point;
        //     // Debug.Log($"Mouse: {mousePos}");
        // }
        //
        // Vector3 lookDirection = new Vector3(mousePos.x - transform.position.x, 0, mousePos.z - transform.position.z);
        // Debug.Log(lookDirection);
        // transform.forward = lookDirection;
    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        dir = new Vector3(inputVector.x, inputVector.y);
    }
    
    public void OnLook(InputValue inputValue)
    {
        transform.Rotate(Vector3.forward, -inputValue.Get<Vector2>().x * Time.deltaTime * sensitivity);
    }

    public void OnFire()
    {
        switch (currentType)
        {
            case PlayerType.triangle:
                Debug.Log("attempting p3");
                gun.Shoot(bolt, transform.position, transform.rotation);
                break;
            case PlayerType.square:
                Debug.Log("im bad help turret");
                Object.Instantiate(turret, transform.position, transform.rotation);
                break;
            case PlayerType.pentagon:
                break;
            case PlayerType.hexagon:

                break;
        }
        
    }
}
