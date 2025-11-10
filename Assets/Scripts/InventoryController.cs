using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    // private ItemDictionary itemDictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public GameObject itemUIPrefab;
    public ItemDatabase itemDatabase;
    public int slotCount;
    public GameObject[] itemPrefabs;

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

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }
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
    public bool AddItem(UIItemData data)
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("Inventory Panel is not assigned in the Inspector!");
            return false;
        }

        Debug.Log("Inventory panel has " + inventoryPanel.transform.childCount + " children");
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                // 1️⃣ Instantiate the UI item prefab as a child of the slot
                GameObject newItem = Instantiate(itemUIPrefab, slotTransform);

                // 2️⃣ Set icon sprite and color
                Image iconImage = newItem.GetComponentInChildren<Image>();
                iconImage.sprite = data.icon;
                iconImage.color = data.iconTint;

                // 3️⃣ Fix positioning and size so it appears in the slot
                RectTransform rect = newItem.GetComponent<RectTransform>();
                rect.localScale = Vector3.one;           // ensure scale is correct
                rect.anchoredPosition = Vector2.zero;    // center in slot
                rect.sizeDelta = data.iconSize;          // optional: use ScriptableObject size

                // 4️⃣ Mark slot as occupied
                slot.currentItem = newItem;

                Debug.Log("Item Added!");
                return true;
            }
        }

        Debug.Log("Inventory Full!");
        return false;
    }
    //     foreach (Transform slotTransform in inventoryPanel.transform)
    //     {
    //         Debug.Log("Checking slot: " + slotTransform.name); //coment
    //         Slot slot = slotTransform.GetComponent<Slot>();
    //         if (slot == null)
    //         {
    //             Debug.Log("No Slot script on: " + slotTransform.name);
    //         }
    //         else if (slot.currentItem != null)
    //         {
    //             Debug.Log("Slot already has item: " + slot.currentItem.name); //coment
    //         }
                
    //         if (slot != null && slot.currentItem == null)
    //         {
    //             GameObject newItem = Instantiate(itemUIPrefab, slotTransform);
                
    //             // Set icon
    //             Image iconImage = newItem.GetComponentInChildren<Image>();
    //             iconImage.sprite = data.icon;
    //             iconImage.color = data.iconTint;

    //             // Adjust size if defined
    //             RectTransform rect = newItem.GetComponent<RectTransform>();
    //             rect.sizeDelta = data.iconSize;

    //             slot.currentItem = newItem;
    //             Debug.Log("Item Added!?");
    //             return true;
    //         }
    //     }

    //     Debug.Log("Inventory is Full");
    //     return false;
    // }
    // public bool AddItem(GameObject itemPrefab)
    // {
    //     foreach (Transform slotTransform in inventoryPanel.transform)
    //     {
    //         Slot slot = slotTransform.GetComponent<Slot>();
    //         if (slot != null && slot.currentItem == null)
    //         {
    //             GameObject newItem = Instantiate(itemPrefab, slotTransform);
    //             newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    //             slot.currentItem = newItem;
    //             return true;
    //         }
    //     }
    //     Debug.Log("Inventory is Full");
    //     return false;
    // }

    // //ADD UI ITEMS
    // public void AddUIItem(UIItemData data)
    // {
    //     // create the UI object
    //     GameObject newItem = Instantiate(itemUIPrefab, inventoryPanel.transform);

    //     // get its image
    //     Image iconImage = newItem.GetComponentInChildren<Image>();
    //     iconImage.sprite = data.icon;
    //     iconImage.color = data.iconTint;

    //     // set size if you want
    //     RectTransform rect = newItem.GetComponent<RectTransform>();
    //     rect.sizeDelta = data.iconSize;

    //     // optional: add tooltip or text from data.itemName
    // }
}
