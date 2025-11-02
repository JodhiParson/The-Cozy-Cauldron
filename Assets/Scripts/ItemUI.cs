using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text itemNameText;
    [HideInInspector] public ItemData itemData;

    public void SetItem(ItemData data)
    {
        itemData = data;
        icon.sprite = data.icon;
        itemNameText.text = data.itemName;
    }
}
