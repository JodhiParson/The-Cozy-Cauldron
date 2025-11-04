using UnityEngine;

public class SpriteDepthSorter : MonoBehaviour
{
    public SpriteRenderer bossRenderer;   // The boss sprite
    private SpriteRenderer playerRenderer; // The player sprite

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerRenderer = player.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (bossRenderer == null || playerRenderer == null)
            return;

        // Compare Y positions and adjust player sorting order
        if (playerRenderer.transform.position.y < bossRenderer.transform.position.y)
        {
            // Player is below boss → draw player in front
            playerRenderer.sortingOrder = bossRenderer.sortingOrder + 2;
        }
        else
        {
            // Player is above boss → draw player behind
            playerRenderer.sortingOrder = bossRenderer.sortingOrder - 2;
        }
    }
}
