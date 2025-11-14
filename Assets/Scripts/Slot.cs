// using UnityEngine;
// using UnityEngine.EventSystems;
// // using UnityEngine.

// public class Slot : MonoBehaviour
// {
//     public GameObject currentItem;

// }
using UnityEngine;

public class Slot : MonoBehaviour
{
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

        if (craftingManager != null)
            craftingManager.UpdateCraftingOutput();
    }

    // Call this when the item is removed from the slot
    public void ClearItem()
    {
        currentItem = null;

        if (craftingManager != null)
            craftingManager.UpdateCraftingOutput();
    }
}
