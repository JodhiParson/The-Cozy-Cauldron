using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int health = 100;

    [Header("Health UI")]
    public Slider healthBar;

    [Header("Respawn Settings")]
    public Transform respawnPoint;             // Where player respawns
    public RespawnMenuController respawnMenu;  // Menu shown on death
    public float deathDelay = 0.5f;            // fallback if no animation

    [Header("Animation")]
    public Animator animator;
    public string deathTrigger = "Die";

    [Header("Events")]
    public UnityEvent<GameObject> OnHitWithReference;
    public UnityEvent<GameObject> OnDeathWithReference;

    private bool isDead = false;

    void Start()
    {
        health = maxHealth;
        if (healthBar != null)
            healthBar.maxValue = maxHealth;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (healthBar != null)
            healthBar.value = health;
    }

    // ---------------------------------------------
    //  TAKE DAMAGE
    // ---------------------------------------------
    public void TakeDamage(int amount, GameObject sender)
    {
        if (isDead)
            return;

        if (sender.layer == gameObject.layer)
            return;

        health -= amount;

        if (health > 0)
            OnHitWithReference?.Invoke(sender);
        else
            Die(sender);
    }

    // ---------------------------------------------
    //  DIE
    // ---------------------------------------------
    private void Die(GameObject sender)
    {
        isDead = true;
        health = 0;

        OnDeathWithReference?.Invoke(sender);

        // Play death animation
        if (animator != null && !string.IsNullOrEmpty(deathTrigger))
            animator.SetTrigger(deathTrigger);

        // Freeze player movement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Start coroutine to wait for animation to finish
        if (respawnMenu != null)
            StartCoroutine(WaitForDeathAnimationCoroutine());
    }

    private IEnumerator WaitForDeathAnimationCoroutine()
    {
        if (animator != null && !string.IsNullOrEmpty(deathTrigger))
        {
            // Wait until Animator has entered the death state
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(deathTrigger))
            {
                yield return null;
            }

            // Wait for the length of the animation
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);
        }
        else
        {
            // Fallback if no animator or trigger
            yield return new WaitForSeconds(deathDelay);
        }
        DestroyAllEnemies();
        ShowRespawnMenu();
    }

    private void ShowRespawnMenu()
    {
        if (respawnMenu != null)
            respawnMenu.ShowMenu();
    }

    // ---------------------------------------------
    //  RESPAWN BUTTON CALLS THIS
    // ---------------------------------------------
    public void Respawn()
    {
        health = maxHealth;
        isDead = false;

        // Move player
        if (respawnPoint != null)
            transform.position = respawnPoint.position;

        // Update UI
        if (healthBar != null)
            healthBar.value = maxHealth;
    }
    public void DestroyAllEnemies()
{
    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    {
        Destroy(enemy);
    }
}
}
