using UnityEngine;

public class SlimeHealth : MonoBehaviour
{
    public int health;
    public int damage;
    private float timeBtwDmg = 1.5f;
    public PlayerHealth playerHealth;
    public float dmgCooldown;
    private Collider2D playerInRange;

    private void Update()
    {
        if (dmgCooldown > 0)
            dmgCooldown -= Time.deltaTime;

        if (playerInRange != null && dmgCooldown <= 0)    
            TryDamagePlayer(playerInRange);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            playerInRange = other;
            TryDamagePlayer(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerInRange)
        {
            playerInRange = null;
        }
    }

    private void TryDamagePlayer(Collider2D other)
    {
        if (dmgCooldown > 0 || other == null) return;

        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            dmgCooldown = timeBtwDmg;
        }
    }
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
