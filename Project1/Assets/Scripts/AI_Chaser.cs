using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Chaser : MonoBehaviour
{
    public CollidableObject target;
    public float speed = 1f;
    public Vector2 direction = Vector2.right;
    public Vector3 velocity = Vector2.zero;
    private Vector2 offset;

    private Vector3 screenSize;
    // Start is called before the first frame update
    void Start()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        offset = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
    }

    // Update is called once per frame
    void Update()
    {
        direction = (target.transform.position+ (Vector3)offset) - transform.position;
        direction.Normalize();
        velocity = direction * speed * Time.deltaTime;
        transform.position += (Vector3)velocity;
        transform.rotation =
                Quaternion.LookRotation(Vector3.forward, upwards: (Vector3)direction);
        /*Debug.Log("Chasing target");*/
    }

    public void SetFields(CollidableObject target)
    {
        this.target = target;
    }
}
