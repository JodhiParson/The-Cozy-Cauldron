using UnityEngine;

public class Spawner : MonoBehaviour 
{
    public GameObject slime;
    public Transform spawnPoint;
    public int spawnCount;

    void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
                for (int i = 0; i < spawnCount; i++)
        {
            Vector2 randomPos = GetRandomPosition();
            GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        }
    }
}
