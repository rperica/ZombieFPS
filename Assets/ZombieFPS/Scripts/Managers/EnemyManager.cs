using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    private static EnemyManager _instance;

    public static EnemyManager Instance
    {
        get
        {
            if(_instance==null)
            {
                GameObject go = new GameObject("EnemyManager");
                go.AddComponent<EnemyManager>();
            }

            return _instance;
        }
    }
    #endregion

    private void Awake()
    {
        #region Singleton
        _instance = this;
        #endregion
    }

    public float time = 2.0f;
    public int maxInstantiatedEnemies = 5;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public Transform playerTransform;

    private int spawnCounter = 0;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", time, time);
    }

    private void SpawnEnemy()
    {
        if (spawnCounter < maxInstantiatedEnemies)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[index].position, spawnPoints[index].rotation);
            spawnCounter++;

        }
    }

    public void DestroyEnemyObject(GameObject enemyObject)
    {
        Destroy(enemyObject);

        spawnCounter--;

        if (spawnCounter < 0)
            spawnCounter = 0;
    }
}
