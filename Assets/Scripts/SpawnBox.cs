using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public List<GameObject> mob;
    public SpriteRenderer spawnArea;
    public int spawnCount;
    public float respawnTime;
    public float maxSpawn;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (currentEnemies < maxSpawn)
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
            if (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxSpawn)
                break;
            GameObject prefab = mob[Random.Range(0, mob.Count)];
            float randX = Random.Range(bounds.min.x, bounds.max.x);
            float randY = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 spawnPos = new Vector2(randX, randY);

            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
        
    }
}
