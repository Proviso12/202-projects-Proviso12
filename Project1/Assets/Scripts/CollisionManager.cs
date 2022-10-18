using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    //store all of the colidable objects in my scene
    //[HideInInspector]
    public List<CollidableObject> collidableObjects = new List<CollidableObject>();
    private SpriteRenderer spriteRenderer;
    private SpawnManager spawnManager;
    public static int playerScore =0;
    public Text textMesh;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        foreach (GameObject collidable in GameObject.FindGameObjectsWithTag("Collidable"))
        {
            collidableObjects.Add(collidable.GetComponent<CollidableObject>());
        }
        textMesh = FindObjectOfType<Text>();
        textMesh.text = "Score: ";
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CollidableObject collidableObject in collidableObjects)
        {
            //reset collision
            collidableObject.ResetCollision();
        }

        //check to see if any of the objects are colliding with each other
        for (int i = collidableObjects.Count - 1; i >= 0; i--)
        {

            for (int j = collidableObjects.Count-1; j >= 0; j--)
            {
                if (i == j) continue;
                
                //do circle collision check
                if (CircleCollision(collidableObjects[i], collidableObjects[j]))
                {
                    collidableObjects[i].isCurrentlyColliding = true;
                    collidableObjects[j].isCurrentlyColliding = true;

                    if (collidableObjects[j].GetComponent<Shots>()!=null && collidableObjects[i].GetComponent<Shots>() == null )
                    {
                        Debug.Log(collidableObjects[i] + " was shot by " + collidableObjects[j]);
                        if(OnHit(collidableObjects[i], collidableObjects[j].GetComponent<Shots>()))
                        {
                            break;
                        }
                        
                        continue;
                    }
                    
                }
                if(collidableObjects[i].GetComponent<Enemy>() != null)
                {
                    collidableObjects[j].GetComponent<Ally>()?.LookOut(collidableObjects[i].GetComponent<Enemy>());
                }
            }
        }
        textMesh.text = "Score: "+playerScore;
    }

    private bool CircleCollision(CollidableObject objectA, CollidableObject objectB)
    {
        float radiusA = objectA.radius;
        float radiusB = objectB.radius; ;
        float distance = Mathf.Sqrt((
            Mathf.Pow(objectA.transform.position.x - objectB.transform.position.x, 2) 
            + Mathf.Pow(objectA.transform.position.y - objectB.transform.position.y, 2)));
        if (distance<=(radiusA+radiusB))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpawnCollidable(CollidableObject obj)
    {
        collidableObjects.Add(obj);
    }
    public void SpawnShot(Shots shot)
    {
        collidableObjects.Add(shot.GetComponent<CollidableObject>());
    }

    public List<CollidableObject> GetAstroids()
    {
        List<CollidableObject> astroids = new List<CollidableObject>();
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            if(collidableObjects[i].GetComponent<Float>() != null)
            {
                astroids.Add(collidableObjects[i]);
            }
        }
        return astroids;
    }
    public List<CollidableObject> GetEnemies()
    {
        List<CollidableObject> enemies = new List<CollidableObject>();
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            if (collidableObjects[i].GetComponent<Enemy>() != null)
            {
                enemies.Add(collidableObjects[i]);
            }
        }
        return enemies;
    }
    public List<CollidableObject> GetEnemies2()
    {
        List<CollidableObject> enemies = new List<CollidableObject>();
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            if (collidableObjects[i].GetComponent<Enemy_Freighter>() != null)
            {
                enemies.Add(collidableObjects[i]);
            }
        }
        return enemies;
    }
    public List<CollidableObject> GetAllies()
    {
        List<CollidableObject> allies = new List<CollidableObject>();
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            if (collidableObjects[i].GetComponent<Ally>() != null)
            {
                allies.Add(collidableObjects[i]);
            }
        }
        return allies;
    }
    public void RemoveShot(Shots shot)
    {
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            if(collidableObjects[i] == shot.GetComponent<CollidableObject>())
            {
                collidableObjects.Remove(collidableObjects[i]);
            }
        }
    }
    public void RemoveEnemy(Enemy ship)
    {
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            if (collidableObjects[i] == ship.GetComponent<CollidableObject>())
            {
                collidableObjects.Remove(collidableObjects[i]);
            }
        }
    }
    public void RemoveAlly(Ally ship)
    {
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            if (collidableObjects[i] == ship.GetComponent<CollidableObject>())
            {
                collidableObjects.Remove(collidableObjects[i]);
            }
        }
    }
    public bool OnHit(CollidableObject ship, Shots shot)
    {
        if(ship.GetComponent<Enemy>()?.TakeDamage(shot.Damage)<=0)
        {
            collidableObjects.Remove(ship);
            spawnManager.RemoveEnemy(ship);
            Destroy(ship.gameObject);
            playerScore += 15;
            Debug.Log("enemy ship was destroyed, ship: " + ship);
            return true;
        }
        if (ship.GetComponent<Enemy_Freighter>()?.TakeDamage(shot.Damage) <= 0)
        {
            collidableObjects.Remove(ship);
            spawnManager.RemoveEnemy(ship);
            Destroy(ship.gameObject);
            playerScore += 500;
            Debug.Log("enemy ship was destroyed, ship: " + ship);
            return true;
        }
        if (ship.GetComponent<Ally>()?.TakeDamage(shot.Damage) <= 0)
        {
            collidableObjects.Remove(ship);
            spawnManager.RemoveAlly(ship);
            Destroy(ship.gameObject);
            playerScore -= 300;
            Debug.Log("ally ship was destroyed, ship: " + ship);
            return true;
        }
        if (ship.GetComponent<Player>()?.TakeDamage(shot.Damage) <= 0)
        {
            /*collidableObjects.Remove(ship);
            spawnManager.RemovePlayer(ship);
            Destroy(ship.gameObject);*/
            SceneManager.LoadScene("GameOver");
            Debug.Log("player ship was destroyed, ship: " + ship);
        }
        collidableObjects.Remove(shot.GetComponent<CollidableObject>());
        Destroy(shot.gameObject);
        return false;
    }
}
