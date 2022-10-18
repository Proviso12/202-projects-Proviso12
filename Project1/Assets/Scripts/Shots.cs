using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1f;
    public Vector2 direction = Vector2.right;
    public Vector3 velocity = Vector2.zero;
    public int damage;
    private float lifeTime =1.5f;
    private bool liveAmmo = false;
    public bool isEnemyShot;
    public int Damage { get { return damage; } }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * speed * Time.deltaTime;
        transform.rotation =
                Quaternion.LookRotation(Vector3.forward, velocity);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0 && liveAmmo) 
        {
            GameObject.FindGameObjectWithTag("CollisionManager").GetComponent<CollisionManager>().RemoveShot(this);
            Destroy(gameObject); 
        }
    }

    public void Shoot(float speed, Vector3 velocity, int damage, bool isEnemyShot)
    {
        this.speed = speed;
        this.velocity = velocity;
        this.damage = damage;
        this.isEnemyShot = isEnemyShot;
        liveAmmo = true;
    }
}
