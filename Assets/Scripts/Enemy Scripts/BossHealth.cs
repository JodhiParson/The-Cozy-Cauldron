using System.Collections;
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

        while(animator.GetCurrentAnimatorStateInfo(0).IsName("Death") == false)
             yield return null;
         while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
             yield return null;

        Destroy(gameObject);
    }
}
