using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public CollidableObject enemyShip;
    public CollidableObject allyShip;
    public CollidableObject astroid;

    public Shots enemyAmmo;
    public Shots allyAmmo;

    public CollidableObject player;

    private List<CollidableObject> enemyShips;
    private List<CollidableObject> allyShips = new List<CollidableObject>();
    private List<CollidableObject> astroids;

    public CollisionManager collisionManager;

    public int activeEnemyShips = 5;
    public int activeAllyShips = 1;
    public int activeAstroids = 3;

    private Vector3 minPosition;
    private Vector3 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawn Manager on");
        minPosition = Camera.main.ScreenToWorldPoint(Vector3.zero);
        maxPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0f));
        astroids = collisionManager.GetAstroids();
        enemyShips = collisionManager.GetEnemies();

        for (int i = 0; i < 4; i++)
        {
            SpawnEnemy();
        }
        /*for (int i = 0; i < allyShips.Count - activeAllyShips; i++)
        {
            SpawnObject(allyShip);
        }*/
        for (int i = 0; i < 3; i++)
        {
            SpawnAstroid();
            Debug.Log("Spawning starting astroid");
        }

    }

    // Update is called once per frame
    void Update()
    {
        /*if(enemyShips.Count<activeEnemyShips)
        {
            SpawnEnemy();
            Debug.Log("low number of enemy ships");
        }*/
        if(allyShips.Count<activeEnemyShips)
        {/*
            SpawnObject(allyShip);*/
        }
    }

    private void SpawnAstroid()
    {
        /*Vector3 spawnPosition = new Vector3(Gaussian(0, (maxPosition.x - minPosition.x) / 8),
            Gaussian(0, (maxPosition.y - minPosition.y) / 8), 0f);*/
        /*SpriteRenderer spawnedObject = Instantiate(ChooseRandomCreature(), spawnPosition, Quaternion.identity);
        spawnedObject.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);*/

        Vector3 spawnPosition = new Vector3( Random.Range(minPosition.x-2, maxPosition.x+2), 
            Random.Range(minPosition.y-2, maxPosition.y+2), 0f); 
         CollidableObject spawnedObject = Instantiate(astroid, spawnPosition, Quaternion.identity);
        collisionManager.SpawnCollidable(spawnedObject);
        activeAstroids++;
        Debug.Log("tried to spawn a new astroid");
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minPosition.x - 2, maxPosition.x + 2),
            Random.Range(minPosition.y - 2, maxPosition.y + 2), 0f);
        CollidableObject spawnedObject = Instantiate(enemyShip, spawnPosition, Quaternion.identity);
        spawnedObject.GetComponent<Enemy>().SetFields(player, enemyAmmo);
        spawnedObject.GetComponent<AI_Chaser>().SetFields(player);
        collisionManager.SpawnCollidable(spawnedObject);
        Debug.Log("tried to spawn a new object");
    }
}
