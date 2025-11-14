using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Loot")]
    public List<LootItem> lootTable = new List<LootItem>();
    [SerializeField]
    public float maxHealth, health;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    public bool isDead;
    public Slider healthBar;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
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
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        animator.SetTrigger("isDead");

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") == false)
            yield return null;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        //loottable
        foreach (LootItem lootItem in lootTable)
        {
            if (Random.Range(0f, 100f) <= lootItem.dropChance)
            {
                InstantiateLoot(lootItem.itemPrefab);
                break; // ✅ only stop once something drops
            }

        }
        Destroy(gameObject);
    }
    
    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
}
