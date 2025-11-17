using UnityEngine;

public class CloseMenus : MonoBehaviour
{
    public CraftingManager cm;
    public MenuController mc;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mc.menuCanvas.SetActive(false);
            cm.ToggleCraftingUI();          
        }
    }
}
