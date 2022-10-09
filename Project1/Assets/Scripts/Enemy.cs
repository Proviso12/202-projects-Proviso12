using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CollidableObject target;
    public Shots bullet;
    private List<Shots> shotsFired = new List<Shots>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetFields(CollidableObject target, Shots bullet)
    {
        this.target = target;
        this.bullet = bullet;
    }
    IEnumerator Shoot()
    {
        for (;;)
        {
            //spawn 3 bullets. set 
            for (int i = 0; i < 3; i++)
            {
                float speed = .005f;
                Vector3 direction = target.transform.position - transform.position;
                direction.Normalize();
                Vector3 velocity = direction * speed * Time.deltaTime;
                velocity.Normalize();
                Shots spawnedBullet = Instantiate(bullet, 
                    new Vector3(transform.position.x, transform.position.y, 0f),
                Quaternion.identity);
                spawnedBullet.Shoot(speed,velocity,direction);
                shotsFired.Add(spawnedBullet);
                Debug.Log("Shot bullets");
            }
            
            yield return new WaitForSeconds(5);
        }
    }
}
