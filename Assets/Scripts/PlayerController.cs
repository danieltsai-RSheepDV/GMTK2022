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

    [Header("Mouse Tracking")]
    [SerializeField] Camera cam;
    [SerializeField] Transform centerOfView;
    [SerializeField] Transform localCenter;
    [SerializeField] MeshRenderer quad;

    [Header("Weapons")]
    [SerializeField] Gun gun;
    [SerializeField] Object bolt;
    [SerializeField] Object turret;
    [SerializeField] Object deflector;

    int dashCounter = 0;  // positive while dashing, negative while recharging, zero when ready
    [SerializeField] int dashCooldown = 60;
    [SerializeField] int dashDuration = 60;
    [Range(1f, 10f)]
    [SerializeField] float dashSpeedBoost = 2f; 
    bool invincible = false;
    float dashSpeed;

    enum PlayerType {
        triangle,
        square,
        pentagon,
        hexagon
    }
    [SerializeField] PlayerType currentType = PlayerType.triangle;
    [SerializeField] int maxHealth = 5;
    int health;


    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        dashSpeed = moveSpeed * dashSpeedBoost;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * moveSpeed * Time.deltaTime;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        Vector3 mousePos = new Vector3();
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mousePos = raycastHit.point;
            // Debug.Log($"Mouse: {mousePos}");
        }

        float scaleFactor = 21 / quad.bounds.size.x;
        Vector3 diff = (mousePos - centerOfView.position) * scaleFactor;
        Debug.Log(quad.bounds.size);
        Vector2 localOffset = new Vector2(localCenter.position.x + diff.x, localCenter.position.y + diff.z);
        Vector2 lookDir = new Vector2(localOffset.x - transform.position.x, localOffset.y - transform.position.y);
        transform.up = lookDir;

        if (dashCounter > 0)
        {
            dir = Vector3.zero;
            transform.position += transform.up * dashSpeed * Time.deltaTime;
            invincible = true;
            dashCounter--;
            if (dashCounter == 0)
            {
                invincible = false;
                dashCounter = dashCooldown * -1;
            }
        } else if (dashCounter < 0)
        {
            dashCounter++;
        }
        
    }

    public void OnMove(InputValue inputValue)
    {
        if (dashCounter != 0)
            return;
        Vector2 inputVector = inputValue.Get<Vector2>();
        dir = new Vector3(inputVector.x, inputVector.y);
    }
    
    public void OnLook(InputValue inputValue)
    {
        // transform.Rotate(Vector3.forward, -inputValue.Get<Vector2>().x * Time.deltaTime * sensitivity);
    }

    public void OnFire()
    {
        switch (currentType)
        {
            case PlayerType.triangle:
                Debug.Log("attempting p3");
                gun.Shoot(bolt);
                break;
            case PlayerType.square:
                Debug.Log("im bad help turret");
                ((GameObject) Instantiate(turret, transform.position, transform.rotation)).SetActive(true);
                break;
            case PlayerType.pentagon:
                Debug.Log("genji shimada");
                ((GameObject) Instantiate(deflector, transform.position, transform.rotation)).SetActive(true);
                break;
            case PlayerType.hexagon:
                if (dashCounter == 0)
                    dashCounter = dashDuration;
                break;
        }
        
    }


    // Call this to inflict damage
    public void Hurt(GameObject effector)
    {
        if (invincible)
        {
            if (effector.tag == "Enemy")
            {
                // Hurt them
            }
        } else
        {
            // reroll character
            currentType = (PlayerType) Mathf.RoundToInt(Random.Range(0f, 3f));
        }
    }
}
