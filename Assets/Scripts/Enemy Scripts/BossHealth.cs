using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
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
        isDead = false;
        Debug.Log("health initialized!");
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

        Debug.Log("took damage!");
        health -= amount;
        Debug.Log("hp" + health);


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
