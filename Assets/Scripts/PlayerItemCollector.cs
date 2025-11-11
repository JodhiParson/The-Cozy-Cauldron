using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;

    void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Item"))
    
        {
            // Debug.Log("Collided with Egg");
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                Collider2D col = collision.GetComponent<Collider2D>();
                if (col != null)
                    col.enabled = false;  // disable trigger
                
                inventoryController.AddItem(item.uiItemData);
                Destroy(collision.gameObject);
            }
            // if (item != null)
            // {
            //     bool itemAdded = inventoryController.AddItem(collision.gameObject);
            //     if (itemAdded)
            //     {
            //         Destroy(collision.gameObject);
            //     }
            // }
            // {

            //TESTING FOR COLLISIONS
        //     Item item = collision.GetComponent<Item>();
        //     if (item != null && item.uiItemData != null)
        //     {
        //         Debug.Log("Item picked up: " + item.uiItemData.itemName);
        //         Debug.Log("Icon sprite: " + item.uiItemData.icon);
        //         Debug.Log("Icon tint: " + item.uiItemData.iconTint);
        //         Debug.Log("Icon size: " + item.uiItemData.iconSize);

        //         // Optional: add to inventory
        //         // inventoryController.AddItem(item.uiItemData);

        //         // Destroy(collision.gameObject);
        //     }
        //     else
        //     {
        //         Debug.LogWarning("Item or uiItemData missing!");
        //     }
        // }
        }
    }
}
