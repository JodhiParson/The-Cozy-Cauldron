using UnityEngine;
using UnityEngine.UI;

public class CauldronInteraction : MonoBehaviour
{
    public GameObject craftingUI;      // Assign your crafting UI panel here
    public CraftingManager craftingManager;
    public GameObject interactionText;       // UI text for "Press F to cook!"
    private bool playerInRange = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false); // hide initially
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // Toggle crafting UI
            craftingManager.ToggleCraftingUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionText.gameObject.SetActive(false);

            // Optionally close crafting UI if player walks away
            if (craftingUI.activeSelf)
                craftingManager.ToggleCraftingUI();
        }
    }
}
