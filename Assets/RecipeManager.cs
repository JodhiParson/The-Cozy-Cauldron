using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject recipeUI;            // Main crafting UI panel 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleRecipeUI()
    {
        isOpen = !isOpen;
        recipeUI.SetActive(isOpen);

        if (isOpen)
        {
            Debug.Log("Recipe UI Opened");
        }
        else
        {
            Debug.Log("Recipe UI Closed");
        }
    }
}
