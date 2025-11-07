using UnityEngine;

public class HitboxTriggerForwarder : MonoBehaviour
{
    [HideInInspector] public WeaponDamage weaponDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weaponDamage?.HandleHit(collision);
    }
}
