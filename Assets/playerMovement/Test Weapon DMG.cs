using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class WeaponDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public WeaponData weaponData;
    public EquipWeapon equipController;
    public string targetTag = "Enemy";
    public string bossTag = "Boss";

    [Tooltip("Assign one or more hitbox GameObjects here (children of player).")]
    public List<GameObject> hitboxes = new List<GameObject>();

    private List<Collider2D> hitboxColliders = new List<Collider2D>();

    private void Awake()
    {
        if (hitboxes.Count == 0)
        {
            Debug.LogError("No hitboxes assigned in WeaponDamage!");
            return;
        }

        for (int i = 0; i < hitboxes.Count; i++)
        {
            var hitbox = hitboxes[i];
            if (hitbox == null) continue;

            var collider = hitbox.GetComponent<Collider2D>();
            if (collider == null)
            {
                Debug.LogError($"Hitbox '{hitbox.name}' has no Collider2D component!");
                continue;
            }

            hitboxColliders.Add(collider);

            var forwarder = hitbox.GetComponent<HitboxTriggerForwarder>();
            if (forwarder == null)
                forwarder = hitbox.AddComponent<HitboxTriggerForwarder>();

            forwarder.weaponDamage = this;

            // Make sure all hitboxes start disabled
            hitbox.SetActive(false);
        }
    }

    public void HandleHit(Collider2D collision)
    {
        int damageAmount = weaponData != null ? weaponData.damage : 10;

        if (collision.CompareTag(targetTag))
        {
            EnemyHealth enemy = collision.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(damageAmount, gameObject);
        }

        if (collision.CompareTag(bossTag))
        {
            BossHealth boss = collision.GetComponentInParent<BossHealth>();
            if (boss != null)
                boss.TakeDamage(damageAmount, gameObject);
        }
    }

public void SetWeaponData(WeaponData newData)
{
    weaponData = newData;
    Debug.Log($"Weapon data updated to: {weaponData.weaponName}");
}


    // --- Animation Event Functions ---

    public void EnableHitbox1() => EnableHitbox(0);
    public void DisableHitbox1() => DisableHitbox(0);

    public void EnableHitbox2() => EnableHitbox(1);
    public void DisableHitbox2() => DisableHitbox(1);

    // Add more if you have more hitboxes, or use a generic method below

    public void EnableHitbox(int index)
    {
        if (index >= 0 && index < hitboxes.Count)
            hitboxes[index]?.SetActive(true);
    }

    public void DisableHitbox(int index)
    {
        if (index >= 0 && index < hitboxes.Count)
            hitboxes[index]?.SetActive(false);
    }
}
