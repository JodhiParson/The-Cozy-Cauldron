using UnityEngine;

public enum ItemType
{
    Weapon,
    Ingredient
}

public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public ItemType itemType;
    public int attackPower;
}
