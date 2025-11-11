using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    public Animator animator;
    public WeaponDamage weaponDamage; // 👈 reference to WeaponDamage script

    private string[] weaponBools = { "EquipTwig", "EquipWSword", "EquipSSword", "EquipHammer" };

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void Equip(WeaponData weaponData)
    {
        if (weaponData == null)
        {
            Debug.LogWarning("No WeaponData provided to EquipWeapon!");
            return;
        }

        // --- 1️⃣ Update animation ---
        foreach (string boolName in weaponBools)
            animator.SetBool(boolName, false);

        if (System.Array.Exists(weaponBools, name => name == weaponData.equipAnimationBool))
            animator.SetBool(weaponData.equipAnimationBool, true);
        else
            Debug.LogWarning($"No animation bool found for '{weaponData.equipAnimationBool}'");

        // --- 2️⃣ Update weapon damage ---
        if (weaponDamage != null)
            weaponDamage.SetWeaponData(weaponData);

        Debug.Log($"Equipped {weaponData.weaponName} (Damage: {weaponData.damage})");
    }
}
