using UnityEngine;

public class BoarWalkRotation : StateMachineBehaviour
{
    [Tooltip("Optional hitbox to flip with the sprite.")]
    public GameObject hitbox;

    private Transform playerPos;

    // Called when entering the walk state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Find the player
        playerPos = GameObject.FindWithTag("PlayerHitbox")?.transform;
    }

    // Called each frame while in this state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerPos == null) return;

        // --- Flip the sprite to face the player ---
        Vector3 scale = animator.transform.localScale;
        if (playerPos.position.x < animator.transform.position.x)
            scale.x = Mathf.Abs(scale.x);   // face left
        else
            scale.x = -Mathf.Abs(scale.x);  // face right
        animator.transform.localScale = scale;

        // --- Flip hitbox if assigned ---
        if (hitbox != null)
        {
            Vector3 localPos = hitbox.transform.localPosition;
            localPos.x = Mathf.Abs(localPos.x) * (scale.x > 0 ? 1 : -1);
            hitbox.transform.localPosition = localPos;
        }
    }
}
