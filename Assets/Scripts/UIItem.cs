using UnityEngine;

[CreateAssetMenu(fileName = "NewUIItemData", menuName = "Inventory/UI Item Data")]
public class UIItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public Color iconTint = Color.white; // optional
    public Vector2 iconSize = new Vector2(64, 64); // optional
}
