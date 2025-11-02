using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Inventory/Ingredient")]
public class IngredientData : ItemData
{
    private void OnEnable()
    {
        itemType = ItemType.Ingredient;
    }
}
