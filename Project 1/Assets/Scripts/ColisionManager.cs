using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionManager : MonoBehaviour
{
    //store all of the colidable objects in my scene
    //[HideInInspector]
    public List<CollidableObject> collidableObjects = new List<CollidableObject>();
    public bool isUsingCircleCollision = false;
    private SpriteRenderer spriteRenderer;

    public TextMesh textMesh;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMesh.text = "Collision mode: ";
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
                    textMesh.text = "Collision Mode: circle collision";
                }
                else
                {
                    if (AABBCollision(collidableObjects[i], collidableObjects[j]))
                    {
                        //do my collision resolution on them
                        collidableObjects[i].isCurrentlyColliding = true;
                        collidableObjects[j].isCurrentlyColliding = true;
                    }
                    textMesh.text = "Collision Mode: AABB";
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
        //use AABB collision detection to see if the two objects are colliding
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
}
