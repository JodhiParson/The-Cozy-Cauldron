using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public GameObject itemPrefab; // The generic ItemUI prefab
    public int slotCount;
    public ItemData[] itemList; // Mix of WeaponData and IngredientData

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();

            if (i < itemList.Length)
            {
                GameObject itemObj = Instantiate(itemPrefab, slot.transform);
                ItemUI itemUI = itemObj.GetComponent<ItemUI>();
                itemUI.SetItem(itemList[i]);
                itemObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                slot.currentItem = itemObj;
            }
        }
    }
}
