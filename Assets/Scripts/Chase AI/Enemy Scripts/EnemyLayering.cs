using UnityEngine;

public class SpriteDepthSorter : MonoBehaviour
{
    private SpriteRenderer bossRenderer;
    private SpriteRenderer playerRenderer;

    private void Start()
    {
        // Find player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerRenderer = player.GetComponentInChildren<SpriteRenderer>();

        // Find boss
        GameObject boss = GameObject.FindWithTag("Enemy");
        if (boss != null)
            bossRenderer = boss.GetComponentInChildren<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (!bossRenderer || !playerRenderer)
            return;

        float playerY = playerRenderer.transform.root.position.y;
        float bossY = bossRenderer.transform.root.position.y;

        if (playerY < bossY)
            playerRenderer.sortingOrder = bossRenderer.sortingOrder + 2;
        else
            playerRenderer.sortingOrder = bossRenderer.sortingOrder - 2;
    }
}
