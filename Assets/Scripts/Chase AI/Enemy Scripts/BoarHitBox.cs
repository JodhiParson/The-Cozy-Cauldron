using UnityEngine;

public class BoarHitBox : MonoBehaviour
{
    public BoarATK bossDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bossDamage?.ProcessHit(other);
    }
}
