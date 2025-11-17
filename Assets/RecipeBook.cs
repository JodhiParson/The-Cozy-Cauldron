using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    public GameObject RecipeBookUI;      // Assign your crafting UI panel here
    public RecipeManager recipeManager;

    public GameObject interactionText;       // UI text for "Press F to see Recipes!"
    private bool playerInRange = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false); // hide initially
    }

    // Update is called once per frame
    void Update()
    {
         if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // Toggle crafting UI
            recipeManager.ToggleRecipeUI();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("entered colider");
        if (other.CompareTag("Player"))
        {
            // Debug.Log("playerhitbox entered collider!");
            playerInRange = true;
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("exit colider");

        if (other.CompareTag("Player"))
        {
            // Debug.Log("exited collider!");
            playerInRange = false;
            interactionText.gameObject.SetActive(false);

            // Optionally close crafting UI if player walks away
            if (RecipeBookUI.activeSelf)
                recipeManager.ToggleRecipeUI();
        }
    }
}
