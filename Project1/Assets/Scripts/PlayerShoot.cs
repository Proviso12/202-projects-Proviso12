using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Shots bullet;

    private float speed = 10f;
    private float maxShootTimer = .2f;
    private float shootTimer;
    private CollisionManager collisionManager;
    // Start is called before the first frame update
    void Start()
    {
        collisionManager = GameObject.FindGameObjectWithTag("CollisionManager").GetComponent<CollisionManager>();
        shootTimer = maxShootTimer;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
    }

    public void OnShoot()
    {
        if(shootTimer<=0)
        {
            Vector3 mousePos = GetMousePos();
            Vector3 velocity = mousePos - transform.position;
            velocity.Scale(new Vector3(1, 1, 0));
            velocity.Normalize();
            Shots spawnedBullet = Instantiate(bullet,
                new Vector3(transform.position.x, transform.position.y)+velocity,
            Quaternion.identity);
            spawnedBullet.Shoot(speed, velocity, 15, false);
            collisionManager.SpawnShot(spawnedBullet);
            shootTimer = maxShootTimer;
            /*Debug.Log(" player Shot bullets");*/
        }
        
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        /*Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);*/
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

}