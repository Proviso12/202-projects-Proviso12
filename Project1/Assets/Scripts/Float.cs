using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float speed = .1f;
    public Vector2 direction = Vector2.right;
    public Vector3 velocity = Vector2.zero;
    private Vector3 screenSize;
    // Start is called before the first frame update
    void Start()
    {
        direction.x = Random.Range(-100, 100);
        direction.y = Random.Range(-100, 100);
        direction.Normalize();
        velocity = direction * speed * Time.deltaTime;
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

    }

    // Update is called once per frame
    void Update()
    {
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
                new Vector3(transform.position.x, -screenSize.y, 0);
        }
        else if (-screenSize.y > transform.position.y)
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
}
