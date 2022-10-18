using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CollidableObject target;
    public Shots ammo;
    public int health = 1;
    public float speed = 1f;
    public Vector2 direction = Vector2.right;
    public Vector3 velocity = Vector2.zero;
    private Vector2 offset;

    private CollisionManager collisionManager;
    // Start is called before the first frame update
    void Start()
    {
        collisionManager = GameObject.FindGameObjectWithTag("CollisionManager").GetComponent<CollisionManager>();
        StartCoroutine(Shoot());
        offset = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
    }

    // Update is called once per frame
    void Update()
    {
        direction = (target.transform.position + (Vector3)offset) - transform.position;
        direction.Normalize();
        velocity = direction * speed * Time.deltaTime;
        transform.position += (Vector3)velocity;

        direction = target.transform.position - transform.position;
        transform.rotation =
                Quaternion.LookRotation(Vector3.forward, upwards: (Vector3)direction);
    }
    public void SetFields(CollidableObject target, Shots bullet)
    {
        this.target = target;
        this.ammo = bullet;
    }

    public int TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject + " took " + damage + " damage. ship has " + health + " left");
        return health;
    }
    IEnumerator Shoot()
    {
        for (;;)
        {
            yield return new WaitForSeconds(1);
            //spawn 3 bullets. set 
            for (int i = 0; i < 1; i++)
            {
                float speed = 5f;
                Vector3 direction = target.transform.position - transform.position;
                direction.Normalize();
                Vector3 velocity = direction * speed;
                velocity.Normalize();
                Shots spawnedBullet = Instantiate(ammo, 
                    new Vector3(transform.position.x, transform.position.y, 0f)+velocity,
                Quaternion.identity);
                spawnedBullet.Shoot(speed,velocity, 2, true);
                collisionManager.SpawnShot(spawnedBullet);
                /*Debug.Log("enemy Shot bullets");*/
            }
        }
    }
}
