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
            Debug.Log("Collided with Egg");
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                bool itemAdded = inventoryController.AddItem(item.uiItemData);
                if (itemAdded)
                {
                    item.PickUp();
                    Destroy(collision.gameObject);
                }
                
            }
            
        }
    }
}
