using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    //UI
    public GameObject craftingUI;            // Main crafting UI panel 
    public Transform craftingPanel;          // Parent containing crafting slots
    public GameObject itemUIPrefab;          // UI prefab for items in crafting
    public GameObject inventoryPanel; //inventory reference
    public GameObject slotPrefab;
    public bool isOpen = false;

    //Crafting System
    public Slot[] ingredientSlots; // size = 3 (assigned in inspector)
    public Slot resultSlot;

    public List<CraftingRecipe> recipes; // assign ScriptableObjects in inspector

    public Button craftButton;
    void Start()
    {
        for (int i = 0; i < 36; i++)
        {
            Slot slot = Instantiate(slotPrefab, craftingPanel.transform).GetComponent<Slot>();
        }

        foreach (Slot slot in ingredientSlots)
        {
            slot.OnItemDropped += IngredientSlotChanged;
        }
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.F))
    //     {
    //         ToggleCraftingUI();
    //     }
    // }

    public void ToggleCraftingUI()
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

    public void LoadInventoryIntoCraftingUI()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("Inventory Panel is not assigned!");
            return;
        }

        // Clear old items in slots
        foreach (Transform slot in craftingPanel)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        // Get inventory items
        List<Item> inventoryItems = InventoryController.Instance.GetInventoryItemsForCrafting();
        for (int i = 0; i < inventoryItems.Count && i < craftingPanel.childCount; i++)
        {
            Transform slot = craftingPanel.GetChild(i);
            Item inventoryItem = inventoryItems[i];

            if (inventoryItem.uiItemData == null)
            {
                Debug.LogWarning("Item has no UIItemData: " + inventoryItem.name);
                continue;
            }

            // Instantiate UI item
            GameObject newItem = Instantiate(InventoryController.Instance.itemUIPrefab, slot);

            // Assign properties
            Item itemComponent = newItem.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemComponent.Initialize(inventoryItem.uiItemData);
            }

            // Set icon
            Image icon = newItem.GetComponentInChildren<Image>();
            if (icon != null)
            {
                icon.sprite = inventoryItem.uiItemData.icon;
                icon.color = inventoryItem.uiItemData.iconTint;
            }

            // Position
            RectTransform rect = newItem.GetComponent<RectTransform>();
            rect.localScale = new Vector2(.15f, .15f);
            rect.anchoredPosition = Vector2.zero;

            Debug.Log("Loaded item into crafting UI: " + inventoryItem.uiItemData.itemName);

        }
    }
    public UIItemData CheckForRecipe()
    {
        List<UIItemData> currentItems = new List<UIItemData>();

        foreach (Slot s in ingredientSlots)
        {
            if (s.currentItem == null)
            {
                // Just skip empty slots instead of returning
                continue;
            }

            Item item = s.currentItem.GetComponent<Item>();
            if (item == null)
            {
                Debug.Log("Slot has NO Item component");
                continue; // skip invalid slot
            }

            if (item.uiItemData == null)
            {
                Debug.Log("Slot item has NO uiItemData!");
                continue; // skip invalid item
            }

            Debug.Log("Found ingredient: " + item.uiItemData.itemName);
            currentItems.Add(item.uiItemData);
        }

        if (currentItems.Count == 0)
            return null; // no ingredients at all

        // Check recipes (unordered match)
        foreach (CraftingRecipe recipe in recipes)
        {
            if (recipe.ingredients.Count != currentItems.Count)
                continue;

            bool match = true;
            foreach (UIItemData ingredient in recipe.ingredients)
            {
                if (!currentItems.Contains(ingredient))
                {
                    match = false;
                    break;
                }
            }

            if (match)
                return recipe.result;
        }

        return null;
    }
    // public UIItemData CheckForRecipe()
    // {
    //     List<UIItemData> currentItems = new List<UIItemData>();

    //     foreach (Slot s in ingredientSlots)
    //     {
    //         if (s.currentItem == null)
    //         {
    //             Debug.Log("Ingredient slot empty!");
    //             return null;
    //         }

    //         Item item = s.currentItem.GetComponent<Item>();
    //         if (item == null)
    //         {
    //             Debug.Log("Slot has NO Item component");
    //             return null;
    //         }

    //         if (item.uiItemData == null)
    //         {
    //             Debug.Log("Slot item has NO uiItemData!");
    //             return null;
    //         }

    //         Debug.Log("Found ingredient: " + item.uiItemData.itemName);

    //         currentItems.Add(item.uiItemData);
    //     }

    //     // Check recipes (unordered match)
    //     foreach (CraftingRecipe recipe in recipes)
    //     {
    //         Debug.Log(recipe.name);
    //         if (recipe.ingredients.Count != currentItems.Count)
    //             continue;

    //         bool match = true;

    //         foreach (UIItemData ingredient in recipe.ingredients)
    //         {
    //             if (!currentItems.Contains(ingredient))
    //             {
    //                 match = false;
    //                 break;
    //             }
    //         }

    //         if (match)
    //             return recipe.result;
    //     }

    //     return null;
    // }

    public void UpdateCraftingOutput()
    {
        Debug.Log("UpdatingCraftingOutput...");

        // Clear previous result
        if (resultSlot.currentItem != null)
            Destroy(resultSlot.currentItem);

        UIItemData result = CheckForRecipe();

        if (result == null)
        {
            Debug.Log("result == null");
            craftButton.interactable = false;
            return;
        }

        // Instantiate result item (non-clickable)
        GameObject craftedItem = Instantiate(InventoryController.Instance.itemUIPrefab, resultSlot.transform);
        Item itemComponent = craftedItem.GetComponent<Item>();
        itemComponent.Initialize(result);

        // Set scale
        RectTransform rect = craftedItem.GetComponent<RectTransform>();
        rect.localScale = new Vector3(0.15f, 0.15f, 1f);
        rect.anchoredPosition = Vector2.zero;

        // Disable interaction (remove InventoryItem if it has drag scripts)
        InventoryItem dragComp = craftedItem.GetComponent<InventoryItem>();
        if (dragComp != null)
            Destroy(dragComp); // or dragComp.enabled = false;

        resultSlot.currentItem = craftedItem;

        craftButton.interactable = true;
    }

    // public void CraftItem()
    // {
    //     if (resultSlot.currentItem == null)
    //     {
    //         Debug.Log("nothing in resultSlot!");
    //         return;
    //     }

    //     Item resultItem = resultSlot.currentItem.GetComponent<Item>();
    //     if (resultItem == null || resultItem.uiItemData == null)
    //     {
    //         Debug.Log("resultItem == null || resultItem.uiItemData == null");
    //         return;

    //     }
    //     UIItemData resultData = resultItem.uiItemData;

    //     // Remove ingredients
    //     foreach (Slot s in ingredientSlots)
    //     {
    //         if (s.currentItem != null)
    //             Destroy(s.currentItem);

    //         s.currentItem = null;
    //     }

    //     // Clear result slot
    //     Destroy(resultSlot.currentItem);
    //     resultSlot.currentItem = null;

    //     // Add crafted item to inventory
    //     InventoryController.Instance.AddItem(resultData);

    //     craftButton.interactable = false;
    // }
    public void CraftItem()
    {
        if (resultSlot.currentItem == null)
        {
            Debug.Log("nothing in resultSlot!");
            return;
        }

        Item resultItem = resultSlot.currentItem.GetComponent<Item>();
        if (resultItem == null || resultItem.uiItemData == null)
        {
            Debug.Log("resultItem == null || resultItem.uiItemData == null");
            return;
        }

        UIItemData resultData = resultItem.uiItemData;

        // Remove ingredients from ingredient slots AND inventory
        foreach (Slot s in ingredientSlots)
        {
            if (s.currentItem != null)
            {
                Item ingredientItem = s.currentItem.GetComponent<Item>();
                if (ingredientItem != null && ingredientItem.uiItemData != null)
                {
                    InventoryController.Instance.RemoveItem(ingredientItem.uiItemData);
                }

                Destroy(s.currentItem); // remove from crafting slot
            }

            s.currentItem = null;
        }

        // Clear result slot
        Destroy(resultSlot.currentItem);
        resultSlot.currentItem = null;

        // Add crafted item to inventory
        InventoryController.Instance.AddItem(resultData);

        craftButton.interactable = false;
    }
    private void IngredientSlotChanged(object sender, OnItemDroppedEventArgs e)
    {
        Debug.Log("IngredientSlotChanged!");
        UpdateCraftingOutput();
    }
}
