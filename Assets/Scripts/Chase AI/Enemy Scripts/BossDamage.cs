using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BossDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage = 10;
    public float timeBetweenHits = 1.5f;
    public GameObject hitbox;
    public LayerMask targetLayers;

    private float hitCooldown;

    private void Start()
    {
        if (hitbox != null)
            hitbox.SetActive(false);
    }

    public void ProcessHit(Collider2D other)
    {
        if (!IsInTargetLayer(other.gameObject)) return;
        if (hitCooldown > 0) return;

        Health targetHealth = other.GetComponentInParent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage, gameObject);
            hitCooldown = timeBetweenHits;
        }
    }

    private void Update()
    {
        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;
    }

    public void EnableHitBox() => hitbox?.SetActive(true);
    public void DisableHitbox() => hitbox?.SetActive(false);

    private bool IsInTargetLayer(GameObject obj)
    {
        return (targetLayers.value & (1 << obj.layer)) != 0;
    }
}
