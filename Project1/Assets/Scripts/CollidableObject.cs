using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    public bool isCurrentlyColliding = false;
    
    public float radius = 1f;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public SpriteRenderer SpriteRenderer{ get { return spriteRenderer; } }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        //if colliding, turn me red
        if(isCurrentlyColliding)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void ResetCollision()
    {
        isCurrentlyColliding = false;
    }
    public void RegisterCollision()
    {
        
    }
    private void OnDrawGizmos()
    {
        if (isCurrentlyColliding)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        
        Gizmos.DrawWireSphere(transform.position, radius);

        if(spriteRenderer==null)
        {
            return;
        }
        Gizmos.DrawWireCube(transform.position, spriteRenderer.bounds.size);
    }
}
