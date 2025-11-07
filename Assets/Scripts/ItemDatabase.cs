using UnityEngine;

[System.Serializable]
public class ItemEntry
{
    public int ID;
    public string itemName;
    public Sprite icon;
}

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public ItemEntry[] items;

    public ItemEntry GetItem(int id)
    {
        foreach (var item in items)
        {
            if (item.ID == id)
                return item;
        }
        return null;
    }
}
