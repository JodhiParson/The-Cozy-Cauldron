using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public List<GameObject> mob;
    public SpriteRenderer spawnArea;
    public int spawnCount = 1;
    public float respawnTime = 3f;
    public int maxSpawn = 5;   // maximum enemies THIS SPAWNER will have active

    private int currentSpawned = 0;   // <-- tracks only this spawner's enemies

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (currentSpawned < maxSpawn)
            {
                SpawnEnemies();
            }

            yield return new WaitForSeconds(respawnTime);
        }
    }

    private void SpawnEnemies()
    {
        if (mob == null || spawnArea == null) return;

        Bounds bounds = spawnArea.bounds;

        for (int i = 0; i < spawnCount; i++)
        {
            if (currentSpawned >= maxSpawn)
                break;

            GameObject prefab = mob[Random.Range(0, mob.Count)];

            float randX = Random.Range(bounds.min.x, bounds.max.x);
            float randY = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 spawnPos = new Vector2(randX, randY);

            GameObject newEnemy = Instantiate(prefab, spawnPos, Quaternion.identity);

            // Tell the enemy who spawned it
            EnemySpawnTracker tracker = newEnemy.AddComponent<EnemySpawnTracker>();
            tracker.spawner = this;

            currentSpawned++;
        }
    }

    // called when an enemy dies
    public void OnEnemyKilled()
    {
        currentSpawned = Mathf.Max(0, currentSpawned - 1);
    }
}
