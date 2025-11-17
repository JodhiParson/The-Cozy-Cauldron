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
        return;    // prevents double death

    if (sender.layer == gameObject.layer)
        return;

    health -= amount;

    if (health > 0)
    {
        OnHitWithReference?.Invoke(sender);
    }
    else
    {
        isDead = true;   // <-- move this here
        OnDeathWithReference?.Invoke(sender);
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
            }

        }
        Destroy(gameObject);
    }
    
void InstantiateLoot(GameObject lootPrefab)
{
    if (lootPrefab)
    {
        GameObject drop = Instantiate(lootPrefab, transform.position, Quaternion.identity);

        RectTransform rect = drop.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.localScale = new Vector2(16, 16);
        }
    }
}

}
