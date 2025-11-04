using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;   // The parent panel (must be under Canvas)
    public GameObject slotPrefab;       // The slot prefab (UI element with RectTransform)
    public GameObject itemPrefab;       // The ItemUI prefab (RectTransform, Image + TMP_Text)

    [Header("Inventory Settings")]
    public int slotCount = 36;          // Total number of slots
    public ItemData[] itemList;         // ScriptableObjects for items (WeaponData, IngredientData)

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            // 1️⃣ Create a slot in the inventory panel
            GameObject slotObj = Instantiate(slotPrefab, inventoryPanel.transform);
            Slot slot = slotObj.GetComponent<Slot>();

            // Ensure RectTransform is correct
            RectTransform slotRT = slotObj.GetComponent<RectTransform>();
            slotRT.localScale = Vector3.one;
            slotRT.anchoredPosition = Vector2.zero;

            // 2️⃣ If we have an item for this slot
            if (i < itemList.Length)
            {
                // Instantiate ItemUI prefab
                GameObject itemObj = Instantiate(itemPrefab);

                // Snap into slot properly
                itemObj.transform.SetParent(slotObj.transform, false); // 'false' keeps local position relative to parent

                // Ensure it’s centered
                RectTransform itemRT = itemObj.GetComponent<RectTransform>();
                itemRT.anchoredPosition = Vector2.zero;
                itemRT.localScale = Vector3.one;

                // Assign the item data to the UI
                ItemUI itemUI = itemObj.GetComponent<ItemUI>();
                itemUI.SetItem(itemList[i]);

                // Register the item in the slot
                slot.currentItem = itemObj;
            }
        }
    }
}
