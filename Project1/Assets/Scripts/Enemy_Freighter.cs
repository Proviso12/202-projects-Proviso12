using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Freighter : MonoBehaviour
{
    public float speed = .1f;
    public Vector2 direction = Vector2.right;
    public Vector3 velocity = Vector2.zero;

    public CollidableObject target;
    public Shots ammo;
    public CollidableObject enemyShip;

    public int health = 400;

    private Vector3 screenSize;

    public int bodyguards = 5;
    private CollisionManager collisionManager;
    private List<CollidableObject> spawnedShips = new List<CollidableObject>();
    // Start is called before the first frame update
    void Start()
    {
        collisionManager = GameObject.FindGameObjectWithTag("CollisionManager").GetComponent<CollisionManager>();
        direction.x = Random.Range(-100, 100);
        direction.y = Random.Range(-100, 100);
        direction.Normalize();
        velocity = direction * speed * Time.deltaTime;
        StartCoroutine(Shoot());
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
        if (spawnedShips.Count < bodyguards) SpawnShips();
    }

    public void SetFields(CollidableObject playerTarget, Shots ammo)
    {
        this.target = playerTarget;
        this.ammo = ammo;
    }
    private void SpawnShips()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2),
            Random.Range(transform.position.y - 2, transform.position.y + 2), 0f);

        CollidableObject spawnedObject = Instantiate(enemyShip, spawnPosition, Quaternion.identity);
        spawnedObject.GetComponent<Enemy>().SetFields(target, ammo);
        GameObject.FindGameObjectWithTag("CollisionManager").GetComponent<CollisionManager>()
            .SpawnCollidable(spawnedObject);
        GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>()
            .AddEnemy(spawnedObject);
        spawnedShips.Add(spawnedObject);
       /* Debug.Log("tried to spawn a new enemy ship");*/
    }
    public int TakeDamage(int damage)
    {
        return health -= damage;
    }
    IEnumerator Shoot()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1);
            
            for (int i = 0; i < 5; i++)
            {
                float speed = 5f;
                Vector3 direction = target.transform.position - transform.position;
                direction.Normalize();
                Vector3 velocity = direction * speed;
                velocity.Normalize();
                Shots spawnedBullet = Instantiate(ammo,
                    new Vector3(transform.position.x, transform.position.y, 0f) + velocity + 
                    new Vector3(Random.Range(-3,3), Random.Range(-3, 3),0),
                    Quaternion.identity);
                spawnedBullet.Shoot(speed, velocity, 8, true);
                collisionManager.SpawnShot(spawnedBullet);
                /*Debug.Log("enemy Shot bullets");*/
            }
        }
    }
}
