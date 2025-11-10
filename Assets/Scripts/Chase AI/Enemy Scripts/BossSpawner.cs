using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class BossSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject bossPrefab;          // The boss to spawn
    public Transform spawnPoint;           // Optional custom spawn position
    public float respawnTime = 10f;        // Cooldown after boss death

    [Header("Player Detection")]
    public string playerTag = "Player";    // Player's tag for detection

    private bool canSpawn = true;
    private GameObject currentBoss;

    private void Start()
    {
        // Ensure collider is a trigger
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canSpawn) return;
        if (!other.CompareTag(playerTag)) return;

        SpawnBoss();
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null) return;

        // Prevent re-spawning while boss is alive
        canSpawn = false;

        Vector2 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
        currentBoss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        // Start checking if boss is dead
        StartCoroutine(WaitForBossDeath());
    }

    private IEnumerator WaitForBossDeath()
    {
        // Wait until the boss is destroyed
        while (currentBoss != null)
        {
            yield return null; // Wait 1 frame
        }

        // Boss is gone → begin respawn cooldown
        yield return new WaitForSeconds(respawnTime);
        canSpawn = true;
    }
}
