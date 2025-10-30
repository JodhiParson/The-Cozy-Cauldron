using UnityEngine;

public class SlimeHealth : MonoBehaviour
{
    public int health;
    public int damage;
    private float timeBtwDmg = 1.5f;
    public PlayerHealth playerHealth;
    public float dmgCooldown; 

    private void Update()
    {
        if (timeBtwDmg > 0)
        {
            timeBtwDmg -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && dmgCooldown <= 0)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
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
