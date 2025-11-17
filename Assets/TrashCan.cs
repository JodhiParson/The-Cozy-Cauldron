using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // Make sure we dropped something
        if (eventData.pointerDrag == null) return;

        // Check if the dragged object has an Item component
        Item itemComponent = eventData.pointerDrag.GetComponent<Item>();
        if (itemComponent == null)
        {
            Debug.Log("[TrashCan] Dropped object is not an inventory item!");
            return;
        }

        // Remove it from the inventory
        bool removed = InventoryController.Instance.RemoveItem(itemComponent.uiItemData);
        if (removed)
        {
            Debug.Log("[TrashCan] Removed item: " + itemComponent.uiItemData.itemName);
        }
        else
        {
            Debug.LogWarning("[TrashCan] Failed to remove item: " + itemComponent.uiItemData.itemName);
        }
    }
}
