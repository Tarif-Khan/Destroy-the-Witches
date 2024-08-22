using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public float xMin = -25;
    public float xMax = 25;
    public float yMin = 8;
    public float yMax = 25;
    public float zMin = -25;
    public float zMax = 25;
    public float spawnTime = 3;
    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies() 
    {
        Vector3 enemyPosition;
        enemyPosition.x = Random.Range(xMin, xMax);
        enemyPosition.y = Random.Range(yMin, yMax);
        enemyPosition.z = Random.Range(zMin, zMax);

        Vector3 enemyPosition2;
        enemyPosition2.x = Random.Range(xMin, xMax);
        enemyPosition2.y = Random.Range(yMin, yMax);
        enemyPosition2.z = Random.Range(zMin, zMax);

        GameObject spawnedEnemy = Instantiate(enemyPrefab1, enemyPosition, transform.rotation) as GameObject;
        GameObject spawnedEnemy2 = Instantiate(enemyPrefab2, enemyPosition2, transform.rotation) as GameObject;
        spawnedEnemy.transform.parent = gameObject.transform;
        spawnedEnemy2.transform.parent = gameObject.transform;
    }
}
