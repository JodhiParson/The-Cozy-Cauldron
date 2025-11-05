 using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damageAmount = 20;
    public string targetTag = "Enemy"; // for normal enemies
    public string bossTag = "Boss";    // for bosses

    private void OnTriggerEnter2D(Collider2D collision)
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
        if (collision.CompareTag(targetTag))
        {
            Debug.Log("Hit boss!");
            BossHealth bossHealth = collision.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damageAmount, gameObject);
                return;
            }
        }
    }
}
