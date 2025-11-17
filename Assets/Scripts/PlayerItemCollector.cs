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
        Debug.Log("Collided with Item");
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            // Add item to inventory, including WeaponData if it exists
            bool itemAdded = inventoryController.AddItem(item.uiItemData, item.weaponData);
            if (itemAdded)
            {
                item.PickUp();
                Destroy(collision.gameObject);
            }
        }
    }
}
}
