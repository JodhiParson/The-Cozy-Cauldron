using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public Transform craftingPanel;          // Parent containing crafting slots
    public GameObject itemUIPrefab;          // UI prefab for items in crafting
    public GameObject craftingUI;            // Main crafting UI panel
    public GameObject inventoryPanel; //inventory reference

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleCraftingUI();
        }
    }

    void ToggleCraftingUI()
    {
        isOpen = !isOpen;
        craftingUI.SetActive(isOpen);

        if (isOpen)
        {
            LoadInventoryIntoCraftingUI();
            Debug.Log("Crafting UI Opened");
        }
        else
        {
            Debug.Log("Crafting UI Closed");
        }
    }

    void LoadInventoryIntoCraftingUI()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("Inventory Panel is not assigned!");
        }

        // Clear old items in slots
        foreach (Transform slot in craftingPanel)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        // Get inventory items from InventoryController singleton
        List<Item> inventoryItems = InventoryController.Instance.GetInventoryItemsForCrafting();

        foreach (Item item in inventoryItems)
        {
            if (item.uiItemData != null)
            {
                Debug.Log("Item Name: " + item.uiItemData.itemName);
                Debug.Log("Icon: " + item.uiItemData.icon);
                Debug.Log("Icon Size: " + item.uiItemData.iconSize);
            }
            else
            {
                Debug.LogWarning("Item has no UIItemData: " + item.name);
            }
            
            for (int i = 0; i < inventoryItems.Count && i < craftingPanel.childCount; i++)
            {
                Transform slot = craftingPanel.GetChild(i);
                Item inventoryItem = inventoryItems[i];

                // Instantiate UI item prefab inside the slot
                GameObject newItem = Instantiate(InventoryController.Instance.itemUIPrefab, slot);

                Item itemComponent = newItem.GetComponent<Item>();
                if (itemComponent != null && inventoryItem.uiItemData != null)
                {
                    itemComponent.uiItemData = inventoryItem.uiItemData;
                    itemComponent.Name = inventoryItem.uiItemData.itemName;
                }

                // Set icon
                Image icon = newItem.GetComponentInChildren<Image>();
                if (icon != null && inventoryItem.uiItemData != null)
                {
                    icon.sprite = inventoryItem.uiItemData.icon;
                    icon.color = inventoryItem.uiItemData.iconTint;
                }

                // Fix RectTransform
                RectTransform rect = newItem.GetComponent<RectTransform>();
                rect.localScale = Vector3.one;
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = new Vector2(14, 14);

                Debug.Log("Loaded item into crafting UI: " + itemComponent.Name);
            }
        }
    }
}
