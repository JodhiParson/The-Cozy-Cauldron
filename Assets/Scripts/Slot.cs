// using UnityEngine;
// using UnityEngine.EventSystems;
// // using UnityEngine.

// public class Slot : MonoBehaviour
// {
//     public GameObject currentItem;

// }
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public enum SlotType
{
    Inventory,
    CraftingInventory,
    CraftingIngredient,
    CraftingResult
}
public class Slot : MonoBehaviour
{
    public SlotType slotType;      // Set this in the inspector
    public GameObject currentItem;
    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;
    private CraftingManager craftingManager;
    public Vector3 itemScale = Vector3.one; // default for inventory slots


    void Awake()
    {
        craftingManager = FindFirstObjectByType<CraftingManager>();
    }

    // Call this when an item is placed in the slot
    public void SetItem(GameObject item)
    {
        currentItem = item;
        // Make it a child of this slot
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = itemScale;   // use the slot’s scale

        // ONLY crafting ingredient slots trigger craft update
        if (slotType == SlotType.CraftingIngredient)
        {
            itemScale = new Vector3(.15f, .15f, 1); // default for inventory slots

            OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs(item));

        }

        if (slotType == SlotType.CraftingInventory)
        {
            itemScale = new Vector3(.15f, .15f, 1); // default for inventory slots

            OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs(item));
        }

    }

    // Call this when the item is removed from the slot
    public void ClearItem()
    {
        currentItem = null;

        // ONLY crafting ingredient slots trigger craft update
        if (slotType == SlotType.CraftingIngredient)
        {
            OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs(null));
        }
    }

}
public class OnItemDroppedEventArgs : EventArgs
{
    public GameObject item;

    public OnItemDroppedEventArgs(GameObject item)
    {
        this.item = item;
    }
}
