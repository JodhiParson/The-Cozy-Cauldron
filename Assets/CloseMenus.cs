using UnityEngine;

public class CloseMenus : MonoBehaviour
{
    public CraftingManager cm;
    public MenuController mc;
    public RecipeManager rm;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mc.menuCanvas.SetActive(false);
            cm.CloseCraftingUI();
            rm.CloseRecipeUI();        
        }
    }
}
