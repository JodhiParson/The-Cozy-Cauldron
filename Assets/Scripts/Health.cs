using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int maxHealth, health;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    
    [SerializeField]
    public bool isDead;
    public Slider healthBar;
    public void InitializeHealth(int healthValue)
    {
        health = healthValue;
        maxHealth = healthValue;
        health = maxHealth;
        isDead = false;
    }
    public void Update()
    {
        healthBar.value = health;
    }
    public void TakeDamage(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        health -= amount;

        if (health > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);
        }
    }
}
