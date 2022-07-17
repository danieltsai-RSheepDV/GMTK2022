using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [Range(0f, 30f)]
    [SerializeField] float moveSpeed = 1f;
    [Range(1f, 100f)]
    [SerializeField] private float sensitivity = 60f;

    [Header("Weapons")]
    [SerializeField] Gun gun;
    [SerializeField] GameObject bolt;
    [SerializeField] GameObject chaser;
    [SerializeField] GameObject turret;
    private List<GameObject> turrets = new List<GameObject>();
    [SerializeField] GameObject deflector;

    [SerializeField] private SpriteShape spriteShape;

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
        dashSpeed = moveSpeed * dashSpeedBoost;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((int) currentType);
        spriteShape.currentShape = Mathf.Lerp(spriteShape.currentShape, (int) currentType + 3, Time.deltaTime * 3f);
        
        transform.position += dir * moveSpeed * Time.deltaTime;

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
        transform.Rotate(Vector3.forward, -inputValue.Get<Vector2>().x * Time.deltaTime * sensitivity);
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
                turrets.RemoveAll(item => item == null);
                GameObject nTurret = Instantiate(turret, transform.position, transform.rotation);
                nTurret.SetActive(true);
                turrets.Add(nTurret);
                if (turrets.Count > 2)
                {
                    Destroy(turrets[0]);
                    turrets.RemoveAt(0);
                }
                break;
            case PlayerType.pentagon:
                Debug.Log("genji shimada");
                ((GameObject) Instantiate(deflector, transform.position, transform.rotation)).SetActive(true);
                break;
            case PlayerType.hexagon:
                if (dashCounter == 0)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Charge");
                    dashCounter = dashDuration;
                }

                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Projectile p = col.gameObject.GetComponent<Projectile>();
        
        if (invincible)
        {
            
        } 
        
        else if((p && p.tags.Contains("Enemy")))
        {
            health--;
            currentType = (PlayerType) Mathf.RoundToInt(Random.Range(0f, 3f));

            if (health < 1)
            {
                GameManager.die.shatter();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Enemy e = col.gameObject.GetComponent<Enemy>();
        
        if (invincible)
        {
            if (e)
            {
                e.Damage();
            }
        } 
        
        else if(e)
        {
            health--;
            currentType = (PlayerType) Mathf.RoundToInt(Random.Range(0f, 3f));

            if (health < 1)
            {
                GameManager.die.shatter();
            }
        }
    }
}
