using UnityEngine;

public class BossHitbox : MonoBehaviour
{
    public BossDamage bossDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bossDamage?.ProcessHit(other);
    }
}
