using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Slider healthBar;

    public void Start()
    {
        health = maxHealth;
    }
    public void Update()
    {
        healthBar.value = health;
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
