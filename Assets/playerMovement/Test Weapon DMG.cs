using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damageAmount = 20;
    public string targetTag = "Enemy"; // normal enemies
    public string bossTag = "Boss";    // bosses
    [Tooltip("Assign the hitbox GameObject here (child of player).")]
    public GameObject hitbox;

    private Collider2D hitboxCollider;

    private void Awake()
    {
        // Automatically pull collider from assigned hitbox
        if (hitbox != null)
        {
            hitboxCollider = hitbox.GetComponent<Collider2D>();

            if (hitboxCollider == null)
                Debug.LogError($"Hitbox '{hitbox.name}' has no Collider2D component!");

            // Hook events by using forwarding component
            var forwarder = hitbox.GetComponent<HitboxTriggerForwarder>();
            if (forwarder == null)
                forwarder = hitbox.AddComponent<HitboxTriggerForwarder>();

            forwarder.weaponDamage = this;
        }
        else
        {
            Debug.LogError("No hitbox assigned in WeaponDamage!");
        }
    }

    public void HandleHit(Collider2D collision)
    {
        Debug.Log($"Triggered with {collision.name}");

        // === Enemy ===
        if (collision.CompareTag(targetTag))
        {
            Debug.Log("Hit enemy!");
            EnemyHealth enemyHealth = collision.GetComponentInParent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount, gameObject);
                return;
            }
        }

        // === Boss ===
        if (collision.CompareTag(bossTag))
        {
            Debug.Log("Hit boss!");
            BossHealth bossHealth = collision.GetComponentInParent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damageAmount, gameObject);
                return;
            }
        }
    }

    public void EnableHitbox() => hitbox?.SetActive(true);
    public void DisableHitbox() => hitbox?.SetActive(false);
}
