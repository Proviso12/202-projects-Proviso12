using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    public CollidableObject player;
    private Enemy enemyTarget;
    public Shots bullet;

    private int health = 55;

    private float viewRadius = 5f;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    private bool CanSeeEnemy = false;

    public CollisionManager collisionManager;

    private float speed = 1f;
    private Vector2 direction = Vector2.right;
    private Vector2 lookDirection = Vector2.right;
    private Vector3 velocity = Vector2.zero;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        collisionManager = GameObject.FindGameObjectWithTag("CollisionManager").GetComponent<CollisionManager>();
        StartCoroutine(Shoot());
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        offset = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
    }

    // Update is called once per frame
    void Update()
    {
        if(CanSeeEnemy)
        {
            direction = (enemyTarget.transform.position + (Vector3)offset) - transform.position;
            lookDirection = enemyTarget.transform.position - transform.position;
        }
        else
        {
            direction = (player.transform.position + (Vector3)offset) - transform.position;
            lookDirection = player.transform.position - transform.position;
        }
        
        direction.Normalize();
        lookDirection.Normalize();
        velocity = direction * speed * Time.deltaTime;
        transform.position += (Vector3)velocity;
        transform.rotation =
                Quaternion.LookRotation(Vector3.forward, upwards: (Vector3)direction);
    }
    public void SetFields(CollidableObject target, Shots bullet)
    {
        this.player = target;
        this.bullet = bullet;
    }

    public int TakeDamage(int damage)
    {
        return health -= damage;
    }

    public void LookOut( Enemy objectB)
    {
        float radiusB = objectB.gameObject.GetComponent<CollidableObject>().radius;
        float distance = Mathf.Sqrt((
            Mathf.Pow(transform.position.x - objectB.transform.position.x, 2)
            + Mathf.Pow(transform.position.y - objectB.transform.position.y, 2)));
        if(distance<=radiusB+viewRadius)
        {
            CanSeeEnemy = true;
            enemyTarget = objectB;
        }
        else
        {
            CanSeeEnemy = false;
            enemyTarget = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (CanSeeEnemy)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireSphere(transform.position, viewRadius);

        if (spriteRenderer == null)
        {
            return;
        }
        Gizmos.DrawWireCube(transform.position, spriteRenderer.bounds.size);
    }

    IEnumerator Shoot()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1);
            if (enemyTarget != null)
            {
                //spawn 3 bullets. set 
                for (int i = 0; i < 1; i++)
                {
                    float speed = 5f;
                    Vector3 direction = enemyTarget.transform.position - transform.position;
                    direction.Normalize();
                    Vector3 velocity = direction * speed;
                    velocity.Normalize();
                    Shots spawnedBullet = Instantiate(bullet,
                        new Vector3(transform.position.x, transform.position.y, 0f) + velocity,
                    Quaternion.identity);
                    spawnedBullet.Shoot(speed, velocity, 2, false);
                    collisionManager.SpawnShot(spawnedBullet);
                    /*Debug.Log("enemy Shot bullets");*/
                }
            }
        }
    }
}
