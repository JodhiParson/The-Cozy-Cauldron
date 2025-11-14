using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCraftingRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<UIItemData> ingredients; // 3 items
    public UIItemData result;
}
