using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1f;
    public Vector2 direction = Vector2.right;
    public Vector3 velocity = Vector2.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)velocity;
        transform.rotation =
                Quaternion.LookRotation(Vector3.forward, upwards: (Vector3)direction);
    }

    public void Shoot(float speed, Vector3 velocity, Vector2 direction)
    {
        this.speed = speed;
        this.velocity = velocity;
        this.direction = direction;
    }
}
