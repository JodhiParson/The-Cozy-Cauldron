using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponData : ItemData
{
    public int damage;
    public float attackSpeed;
    public GameObject weaponPrefab; // model to equip in player’s hand

    private void OnEnable()
    {
        itemType = ItemType.Weapon;
    }
}
