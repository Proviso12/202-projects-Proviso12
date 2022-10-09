using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float speed = .1f;
    public Vector2 direction = Vector2.right;
    public Vector3 velocity = Vector2.zero;
    private Vector3 minPosition;
    private Vector3 maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        minPosition = Camera.main.ScreenToWorldPoint(Vector3.zero);
        maxPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0f));
        direction.x = Random.Range(-100, 100);
        direction.y = Random.Range(-100, 100);
        direction.Normalize();
        velocity = direction * speed * Time.deltaTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)velocity;
        transform.rotation =
                Quaternion.LookRotation(Vector3.forward, upwards: (Vector3)direction);
        if(transform.position.x< minPosition.x-1f)
        {
            Destroy(this);
        }
        else if (transform.position.x < minPosition.x - 1f)
        {
            Destroy(this);
        }
        else if (transform.position.x > maxPosition.x + 1f)
        {
            Destroy(this);
        }
        else if (transform.position.y < minPosition.y - 1f)
        {
            Destroy(this);
        }
        else if (transform.position.y > maxPosition.y + 1f)
        {
            Destroy(this);
        }
    }
}
