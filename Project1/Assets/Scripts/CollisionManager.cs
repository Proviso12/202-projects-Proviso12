using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    //store all of the colidable objects in my scene
    //[HideInInspector]
    public List<CollidableObject> collidableObjects = new List<CollidableObject>();
    public bool isUsingCircleCollision = false;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("collision manager on");
        spriteRenderer = GetComponent<SpriteRenderer>();

        foreach (GameObject collidable in GameObject.FindGameObjectsWithTag("Collidable"))
        {
            collidableObjects.Add(collidable.GetComponent<CollidableObject>());
            Debug.Log("found collidable");
        }
        collidableObjects.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<CollidableObject>());
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
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            for (int j = i+1; j < collidableObjects.Count; j++)
            {
                if (isUsingCircleCollision)
                {
                    //do circle collision check
                    if(CircleCollision(collidableObjects[i], collidableObjects[j]))
                    {
                        collidableObjects[i].isCurrentlyColliding = true;
                        collidableObjects[j].isCurrentlyColliding = true;
                    }

                }
                else
                {
                    if (AABBCollision(collidableObjects[i], collidableObjects[j]))
                    {
                        //do my collision resolution on them
                        collidableObjects[i].isCurrentlyColliding = true;
                        collidableObjects[j].isCurrentlyColliding = true;
                    }

                }
                
            }
        }
    }
    private bool AABBCollision(CollidableObject objectA, CollidableObject objectB)
    {
        Vector3 aMin = objectA.spriteRenderer.bounds.min;
        Vector3 aMax = objectA.spriteRenderer.bounds.max;
        Vector3 bMin = objectB.spriteRenderer.bounds.min;
        Vector3 bMax = objectB.spriteRenderer.bounds.max;
        if(aMax.x>bMin.x &&
           aMin.x<bMax.x &&
           aMax.y > bMin.y &&
           aMin.y < bMax.y)
        {
            return true;
        }

        return false;
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

    public void SwitchToCircleCollision()
    {
        isUsingCircleCollision = true;
    }

    public void SwitchToAABB()
    {
        isUsingCircleCollision = false;
    }

    public void SpawnCollidable(CollidableObject obj)
    {
        collidableObjects.Add(obj);
        Debug.Log("placed a ship or astroid");
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
    /*public List<CollidableObject> GetAllyShips()
    {
        List<CollidableObject> allyShips = new List<CollidableObject>();
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            if (collidableObjects[i].GetComponent<Float>() != null)
            {
                allyShips.Add(collidableObjects[i]);
            }
        }
        return allyShips;
    }*/
}
