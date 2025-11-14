using Unity.VisualScripting;
using UnityEngine;

public class MenuController:MonoBehaviour
{
    public GameObject menuCanvas;
    public CraftingManager cm;
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && cm.isOpen == false)
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
