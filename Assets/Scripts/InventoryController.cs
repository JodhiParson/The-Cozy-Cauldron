using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    private ItemDictionary itemDictionary;
    public ItemDatabase itemDatabase;
    // private ItemDictionary itemDictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public GameObject itemUIPrefab;

    public int slotCount;
    public GameObject[] itemPrefabs;

    void Awake()
    {
        if (Instance == null)
            Instance = this;  // Assign this object to the static reference
        else
            Destroy(gameObject); // Prevent duplicates
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDictionary = FindFirstObjectByType<ItemDictionary>();

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
        Debug.Log("slotCount is " + slotCount);
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }
        return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // for (int i = 0; i < slotCount; i++)
        // {
        //     Instantiate(slotPrefab, inventoryPanel.transform);
        // }
        foreach (InventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
        }
    }
    // Adds a UI item to the first empty slot
    public bool AddItem(UIItemData data)
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("Inventory Panel is not assigned!");
            return false;
        }

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                // Instantiate UI item inside the slot
                GameObject newItem = Instantiate(itemUIPrefab, slotTransform);

                // Get Item component and initialize it with ScriptableObject
                Item itemComponent = newItem.GetComponent<Item>();
                if (itemComponent != null)
                {
                    itemComponent.Initialize(data); // ✅ Assigns uiItemData, Name, icon, size, etc.
                }

                slot.currentItem = newItem;

                Debug.Log("Added item to inventory: " + data.itemName);
                return true;
            }
        }

        Debug.Log("Inventory Full!");
        return false;
    }

    public List<Item> GetInventoryItemsForCrafting()
    {
        List<Item> items = new List<Item>();

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item != null)
                {
                    if (item.uiItemData != null)
                    {
                        Debug.Log("Item has UIItemData: " + item.uiItemData.itemName);
                    }
                    else
                    {
                        Debug.LogWarning("Item has NO UIItemData: " + item.name);
                    }

                    items.Add(item);
                }
            }
        }

        return items;
    }
}
