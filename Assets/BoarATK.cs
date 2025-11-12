using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoarATK : MonoBehaviour
{
    private Animator animator;

    [Header("Detection")]
    public string playerTag = "PlayerHitbox";
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;

    private float attackTimer;

    private Transform player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;
    }

    void Update()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        // Check if player is close enough to attack
        if (distance <= attackRange && attackTimer <= 0)
        {
            animator.SetTrigger("Attack");
            attackTimer = attackCooldown; // reset cooldown
        }
    }

    // Optional: visualize attack range in scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
