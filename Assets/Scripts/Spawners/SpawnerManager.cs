using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPoolItem
    {
        public GameObject enemyPrefab;
        public int amountToPool;
        public bool expandPool;
        [HideInInspector]
        public List<GameObject> pooledObjects;
    }

    public List<Transform> spawnerList = new List<Transform>(); // List of spawn points
    public List<EnemyPoolItem> enemyPools = new List<EnemyPoolItem>(); // List of enemy pools
    public float spawnInterval = 5f; // Time interval between spawns
    public GameObject enemyPoolContainer; // Container for all enemies

    private float nextTimeToSpawn;
    private List<GameObject> activeEnemies = new List<GameObject>();
    public int maxActiveEnemies = 20; // Maximum number of enemies that can be active at once

    void Start()
    {
        // Ensure there is a container for enemies
        if (enemyPoolContainer == null)
        {
            enemyPoolContainer = new GameObject("EnemyPool");
        }

        InitializePools();
        nextTimeToSpawn = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextTimeToSpawn && activeEnemies.Count < maxActiveEnemies)
        {
            SpawnEnemy();
            nextTimeToSpawn = Time.time + spawnInterval;
        }
    }

    public void InitializePools()
    {
        foreach (var pool in enemyPools)
        {
            pool.pooledObjects = new List<GameObject>();
            for (int i = 0; i < pool.amountToPool; i++)
            {
                GameObject obj = Instantiate(pool.enemyPrefab, enemyPoolContainer.transform);
                obj.SetActive(false);
                pool.pooledObjects.Add(obj);
            }
        }
    }

    public void SpawnEnemy()
    {
        if (spawnerList.Count == 0 || enemyPools.Count == 0)
        {
            Debug.LogError("Spawner list or enemy pool is empty, cannot spawn enemies.");
            return;
        }

        Transform spawnPoint = spawnerList[Random.Range(0, spawnerList.Count)];
        EnemyPoolItem selectedPool = enemyPools[Random.Range(0, enemyPools.Count)];
        GameObject enemyToSpawn = GetPooledObject(selectedPool);

        if (enemyToSpawn != null)
        {
            enemyToSpawn.transform.position = spawnPoint.position;
            enemyToSpawn.transform.rotation = spawnPoint.rotation;
            enemyToSpawn.SetActive(true);
            activeEnemies.Add(enemyToSpawn);
        }
    }

    GameObject GetPooledObject(EnemyPoolItem pool)
    {
        foreach (GameObject obj in pool.pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        if (pool.expandPool)
        {
            GameObject obj = Instantiate(pool.enemyPrefab, enemyPoolContainer.transform);
            obj.SetActive(false);
            pool.pooledObjects.Add(obj);
            return obj;
        }

        return null; // No objects available and not allowed to expand
    }
}
