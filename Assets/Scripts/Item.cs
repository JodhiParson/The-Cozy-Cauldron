using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public UIItemData uiItemData; // 💡 link the ScriptableObject here
    public WeaponData weaponData;
    public string Name;
    public virtual void PickUp()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
        if (ItemPickupUIController.Instance != null)
        {
            ItemPickupUIController.Instance.ShowItemPickup(Name, itemIcon);
        }

    }
    public void Initialize(UIItemData data, WeaponData wData = null)
    {
        uiItemData = data;
        weaponData = wData;
        Name = data.itemName;

        // Apply icon immediately
        Image icon = GetComponentInChildren<Image>();
        if (icon != null)
        {
            icon.sprite = data.icon;
            icon.color = data.iconTint;
        }

        RectTransform rect = GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.sizeDelta = data.iconSize;
            rect.localScale = Vector3.one;
            rect.anchoredPosition = Vector2.zero;
        }
    }
}
