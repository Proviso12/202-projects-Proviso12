using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed = 1f;
    public Vector2 direction = Vector2.right;
    public Vector3 velocity = Vector2.zero;
    private Vector2 movementInput;
    private Vector3 screenSize;
    public Slider healthbar;
    
    public int health;
    public int maxHealth = 350;

    // Start is called before the first frame update
    void Start()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        healthbar = FindObjectOfType<Slider>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        direction = movementInput;
        //multiply velocity
        //step 1 our velocity is our direction multiplied by speed
        velocity = direction * speed * Time.deltaTime;

        // step 2 add our velocity to the position so that our object moves
        transform.position += (Vector3)velocity;

        if (screenSize.x < transform.position.x)
        {
            transform.position = 
                new Vector3(-screenSize.x, transform.position.y, 0);
        }
        else if (-screenSize.x > transform.position.x)
        {
            transform.position =
               new Vector3(screenSize.x, transform.position.y, 0);
        }
        if (screenSize.y < transform.position.y)
        {
            transform.position = 
                new Vector3(transform.position.x ,-screenSize.y, 0);
        }
        else if(-screenSize.y > transform.position.y)
        {
            transform.position = 
                new Vector3(transform.position.x, screenSize.y, 0);
        }
        if (direction != Vector2.zero)
        {
          transform.rotation = 
                Quaternion.LookRotation(Vector3.forward, upwards: (Vector3)direction);
        }
    }

    public void OnMove(InputAction.CallbackContext moveContext)
    {
        movementInput = moveContext.ReadValue<Vector2>();
    }

    public int TakeDamage(int damage)
    {
        health -= damage;
        healthbar.value = (float)health/maxHealth;
        return health;
    }
}
