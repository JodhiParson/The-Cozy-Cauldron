using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public float attackSpeed;

    // Optional: logic for attacks
    public void OnEquip()
    {
        // Called when the player equips this weapon
    }

    public void OnUnequip()
    {
        // Called when the weapon is unequipped
    }
}
