using UnityEngine;

public class CloseMenus : MonoBehaviour
{
    CraftingManager cm;
    MenuController mc;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mc.menuCanvas.SetActive(false);          
        }
    }
}
