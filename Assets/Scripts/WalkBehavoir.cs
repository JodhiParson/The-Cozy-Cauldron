using UnityEngine;

public class WalkBehavoir : StateMachineBehaviour
{
    [Header("Timers")]
    public float timer;
    public float minTime = 1f;
    public float maxTime = 3f;

    [Header("Movement")]
    public float speed = 2f;
    [Tooltip("How far below the boss's center the feet are.")]
    public float feetOffset;

    private Transform playerPos;

    // Called when entering the walk state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindWithTag("PlayerHitbox").transform;
        timer = Random.Range(minTime, maxTime);
    }

    // Called each frame while in this state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            timer -= Time.deltaTime;
        }

        // --- Offset boss position downward so it moves from feet instead of body center ---
        Vector2 target = new Vector2(playerPos.position.x, playerPos.position.y);
        Vector2 bossPos = animator.transform.position;
        Vector2 bossFeet = new Vector2(bossPos.x, bossPos.y - feetOffset);

        // Move the boss toward the target, using its feet as reference
        Vector2 newPos = Vector2.MoveTowards(bossFeet, target, speed * Time.deltaTime);

        // Restore offset so the actual transform moves correctly
        newPos.y += feetOffset;

        animator.transform.position = newPos;

        // --- Flip sprite to face player ---
        Vector2 scale = animator.transform.localScale;
        if (playerPos.position.x < animator.transform.position.x)
            scale.x = Mathf.Abs(scale.x);
        else
            scale.x = -Mathf.Abs(scale.x);

        animator.transform.localScale = scale;
    }
}
