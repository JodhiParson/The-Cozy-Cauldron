using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage = 10;
    public float timeBetweenHits = 1.5f;

    [Header("Optional Filters")]
    [Tooltip("Layers this object can damage")]
    public LayerMask targetLayers;

    private float hitCooldown;
    private Collider2D targetInRange;

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

    private bool IsInTargetLayer(GameObject obj)
    {
        return (targetLayers.value & (1 << obj.layer)) != 0;
    }
}
