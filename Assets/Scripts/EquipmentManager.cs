using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    private void Awake() => instance = this;

    [Header("UI References")]
    public Transform weaponSlotUI; // Assign your WeaponSlot from PlayerPage in Inspector

    public ItemData equippedWeapon;
    public SpriteRenderer playerWeaponRenderer; // Optional for player sprite display

    public void Equip(InventoryItem item)
    {
        ItemData newItem = item.itemData;
        if (newItem.itemType != ItemType.Weapon)
            return;

        // Remove item from inventory visually
        item.transform.SetParent(weaponSlotUI);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;

        equippedWeapon = newItem;

        Debug.Log("Equipped weapon: " + newItem.itemName);
    }
}
