// using UnityEngine;
// using UnityEngine.EventSystems;
// // using UnityEngine.

// public class Slot : MonoBehaviour
// {
//     public GameObject currentItem;

// }
using UnityEngine;
public enum SlotType
{
    Inventory,
    CraftingIngredient,
    CraftingResult
}
public class Slot : MonoBehaviour
{
    public SlotType slotType;      // Set this in the inspector
    public GameObject currentItem;
    private CraftingManager craftingManager;
    

    void Awake()
    {
        craftingManager = FindFirstObjectByType<CraftingManager>();
    }

    // Call this when an item is placed in the slot
    public void SetItem(GameObject item)
    {
       currentItem = item;

        // ONLY crafting ingredient slots trigger craft update
        if (slotType == SlotType.CraftingIngredient)
        {
            craftingManager?.UpdateCraftingOutput();
        }
    }

    // Call this when the item is removed from the slot
    public void ClearItem()
    {
        currentItem = null;

        // ONLY crafting ingredient slots trigger craft update
        if (slotType == SlotType.CraftingIngredient)
        {
            craftingManager?.UpdateCraftingOutput();
        }
    }
}
