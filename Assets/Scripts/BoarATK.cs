using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BoarATK : MonoBehaviour
{
    [Header("Target Settings")]
    public string targetTag = "PlayerHitbox";
    public float aggroDistance = 6f;
    public float attackRange = 1.5f; // boss-style attack distance
    public float moveSpeed = 3f;

    [Header("Damage Settings")]
    public int damage = 10;
    public float attackCooldown = 1.5f;
    public GameObject hitbox;
    public LayerMask targetLayers;

    [Header("Components")]
    public Animator animator;

    private Transform target;
    private float attackTimer;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag(targetTag)?.transform;

        if (hitbox != null)
            hitbox.SetActive(false);
    }

    private void Update()
    {
        if (target == null) return;

        attackTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, target.position);

        // Move toward player if in aggro range but outside attack range
        if (distance <= aggroDistance && distance > attackRange)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            animator.SetBool("isIdle", false);
        }
        else if (distance > aggroDistance)
        {
            // Out of range → idle
            animator.SetBool("isIdle", true);
        }

        // Attack if in range and cooldown finished
        if (distance <= attackRange && attackTimer <= 0)
        {
            animator.SetTrigger("Attack");
            attackTimer = attackCooldown;
        }
    }

    public void ProcessHit(Collider2D other)
    {
        if (!IsInTargetLayer(other.gameObject)) return;

        Health targetHealth = other.GetComponentInParent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage, gameObject);
        }
    }

    public void EnableHitBox() => hitbox?.SetActive(true);
    public void DisableHitbox() => hitbox?.SetActive(false);

    private bool IsInTargetLayer(GameObject obj)
    {
        return (targetLayers.value & (1 << obj.layer)) != 0;
    }

    // Optional: visualize attack and aggro ranges
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
    }
}
