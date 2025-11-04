using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BossDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage;
    public float timeBetweenHits = 1.5f;
    public GameObject hitbox;
    public LayerMask targetLayers;

    private float hitCooldown;
    private Collider2D targetInRange;
    private void Start()
    {
        hitbox.SetActive(false);
    }
    private void Update()
    {
        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;

        if (targetInRange != null && hitCooldown <= 0)
        {
            TryDealDamage(targetInRange);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInTargetLayer(other.gameObject))
        {
            targetInRange = other;
            TryDealDamage(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == targetInRange)
            targetInRange = null;
    }

    private void TryDealDamage(Collider2D other)
    {
        if (hitCooldown > 0 || other == null) return;

        Health targetHealth = other.GetComponentInParent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage, gameObject);
            hitCooldown = timeBetweenHits;
        }
    }
    public void EnableHitBox()
    {
        hitbox.SetActive(true);
    }
    public void DisableHitbox()
    {
        hitbox.SetActive(false);
    }
    private bool IsInTargetLayer(GameObject obj)
    {
        return (targetLayers.value & (1 << obj.layer)) != 0;
    }
}
