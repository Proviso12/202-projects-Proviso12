using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Shots bullet;

    private List<Shots> shotsFired = new List<Shots>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnShoot()
    {
        Vector3 mousePos = GetMousePos();
        float speed = .005f;
        Vector3 direction = mousePos - transform.position;
        direction.Normalize();
        Vector3 velocity = direction * speed * Time.deltaTime;
        velocity.Normalize();
        Shots spawnedBullet = Instantiate(bullet,
            new Vector3(transform.position.x, transform.position.y, 0f),
        Quaternion.identity);
        spawnedBullet.Shoot(speed, velocity, direction);
        shotsFired.Add(spawnedBullet);
        Debug.Log("ally Shot bullets");
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        /*Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);*/
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

}