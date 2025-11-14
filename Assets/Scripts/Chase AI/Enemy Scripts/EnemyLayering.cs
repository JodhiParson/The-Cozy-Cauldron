using UnityEngine;

public class SpriteDepthSorter : MonoBehaviour
{
    private SpriteRenderer enemyRenderer;
    private SpriteRenderer playerRenderer;

    private void Start()
    {
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerRenderer = player.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!enemyRenderer || !playerRenderer)
            return;

        float playerY = playerRenderer.transform.root.position.y;
        float enemyY = transform.position.y;

        if (playerY < enemyY)
            playerRenderer.sortingOrder = enemyRenderer.sortingOrder + 2;
        else
            playerRenderer.sortingOrder = enemyRenderer.sortingOrder - 2;
    }
}
