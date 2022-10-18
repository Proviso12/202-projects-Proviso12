using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public CollidableObject enemyShip;
    public CollidableObject enemyFreighter;
    public CollidableObject allyShip;
    public CollidableObject astroid;

    public Shots enemyAmmo;
    public Shots allyAmmo;

    public CollidableObject player;
    public CollidableObject currentPlayer;

    private List<CollidableObject> enemyShips;
    private List<CollidableObject> enemyFreighters;
    private List<CollidableObject> allyShips;
    private List<CollidableObject> astroids;

    public CollisionManager collisionManager;

    public int maxEnemyShips = 1;
    public int maxAllyShips = 1;

    private Vector3 minPosition;
    private Vector3 maxPosition;

    private float frieghterTimer = 60f;
    // Start is called before the first frame update
    void Start()
    {
        minPosition = Camera.main.ScreenToWorldPoint(Vector3.zero);
        maxPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0f));
        astroids = collisionManager.GetAstroids();
        enemyShips = collisionManager.GetEnemies();
        enemyFreighters = collisionManager.GetEnemies2();
        allyShips = collisionManager.GetAllies();
        SpawnPlayer();

        
        SpawnFreighter();
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyShips.Count < maxEnemyShips)
        {
            SpawnEnemy();
        }

        frieghterTimer -=Time.deltaTime;

        if(frieghterTimer<=0)
        {
            SpawnFreighter();
            frieghterTimer = 60f;
        }
        if (allyShips.Count<maxAllyShips)
        {
            SpawnAlly();
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
        
        /*Debug.Log("tried to spawn a new astroid");*/
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minPosition.x - 2, maxPosition.x + 2),
            Random.Range(minPosition.y - 2, maxPosition.y + 2), 0f);

        CollidableObject spawnedObject = Instantiate(enemyShip, spawnPosition, Quaternion.identity);
        spawnedObject.GetComponent<Enemy>().SetFields(currentPlayer, enemyAmmo);

        collisionManager.SpawnCollidable(spawnedObject);
        enemyShips.Add(spawnedObject);
        /*Debug.Log("tried to spawn a new enemy ship");*/
    }
    private void SpawnPlayer()
    {
         currentPlayer = Instantiate(player, Vector3.zero, Quaternion.identity);
        collisionManager.SpawnCollidable(currentPlayer);
        /*Debug.Log("tried to spawn a new enemy ship");*/
    }

    private void SpawnFreighter()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minPosition.x - 2, maxPosition.x + 2),
            Random.Range(minPosition.y - 2, maxPosition.y + 2), 0f);

        CollidableObject spawnedObject = Instantiate(enemyFreighter, spawnPosition, Quaternion.identity);
        spawnedObject.GetComponent<Enemy_Freighter>().SetFields(currentPlayer, enemyAmmo);

        collisionManager.SpawnCollidable(spawnedObject);
        enemyFreighters.Add(spawnedObject);
        /*Debug.Log("tried to spawn a new enemy ship");*/
    }
    private void SpawnAlly()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minPosition.x - 2, maxPosition.x + 2),
            Random.Range(minPosition.y - 2, maxPosition.y + 2), 0f);

        CollidableObject spawnedObject = Instantiate(allyShip, spawnPosition, Quaternion.identity);
        spawnedObject.GetComponent<Ally>().SetFields(currentPlayer, allyAmmo);

        collisionManager.SpawnCollidable(spawnedObject);
        allyShips.Add(spawnedObject);
        /*Debug.Log("tried to spawn a new ally ship");*/
    }
    public void AddEnemy(CollidableObject enemy)
    {
        enemyShips.Add(enemy);
    }
    public void RemoveEnemy(CollidableObject ship)
    {
        for (int i = 0; i < enemyShips.Count; i++)
        {
            if (enemyShips[i] == ship)
            {
                enemyShips.Remove(enemyShips[i]);
            }
        }
    }
    public void RemovePlayer(CollidableObject ship)
    {
         if (player == ship)
            {
            player = null;
            }
        
    }
    public void RemoveAlly(CollidableObject ship)
    {
        for (int i = 0; i < allyShips.Count; i++)
        {
            if (allyShips[i] == ship)
            {
                allyShips.Remove(allyShips[i]);
            }
        }
    }
}
