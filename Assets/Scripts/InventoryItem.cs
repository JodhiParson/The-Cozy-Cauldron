using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler

{
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas rootCanvas;
    private Slot originalSlot;
    public Vector3 originalScale;
    public UIItemData uiItemData;
    public Button actionButton; // assign in inspector
    public WeaponDamage weaponDamageController;
    public PlayerEquipment playerEquipment;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        rootCanvas = GetComponentInParent<Canvas>(); // top-level Canvas

        item = GetComponent<Item>();   // <-- ADD THIS
        if (item == null)
            Debug.LogError("[INIT] No Item component found on InventoryItem object!");
        else
            Debug.Log("[INIT] Item component successfully linked.");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = rectTransform.parent;
        originalSlot = originalParent.GetComponent<Slot>(); // <-- store the slot
        rectTransform.SetParent(rootCanvas.transform); // move to top canvas so it’s on top of everything
        // rectTransform.localScale = Vector3.one;
        canvasGroup.blocksRaycasts = false;            // allow drop detection
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        // Check if we are dropping over a slot
        Slot dropSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<Slot>();

        if (dropSlot == null)
        {
            Debug.Log("dropSlot == null, swapping");
            // If we hit a child of a slot
            dropSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<Slot>();
        }

        // Slot originalSlot = originalParent.GetComponent<Slot>();
        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                // Swap items visually
                GameObject existing = dropSlot.currentItem;

                // Move existing item to original slot
                existing.transform.SetParent(originalSlot.transform, false);
                existing.transform.localPosition = Vector3.zero;
                existing.transform.localScale = existing.GetComponent<InventoryItem>().originalScale;
                originalSlot.SetItem(existing);  // triggers event

                // Move dragged item to drop slot
                rectTransform.SetParent(dropSlot.transform, false);
                rectTransform.localPosition = Vector3.zero;
                rectTransform.localScale = GetComponent<InventoryItem>().originalScale;
                dropSlot.SetItem(gameObject);  // triggers event
            }
            else
            {
                // Drop into empty slot
                rectTransform.SetParent(dropSlot.transform, false);
                rectTransform.localPosition = Vector3.zero;
                rectTransform.localScale = GetComponent<InventoryItem>().originalScale;

                dropSlot.SetItem(gameObject);   // triggers event
                originalSlot.ClearItem();        // triggers event
            }
        }
        else
        {
            // Return to original slot
            rectTransform.SetParent(originalSlot.transform, false);
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = GetComponent<InventoryItem>().originalScale;
            originalSlot.SetItem(gameObject);   // triggers event
        }
        // if (dropSlot != null)
        // {
        //     Debug.Log("dropSlot != null, moving!");
        //     if (dropSlot.currentItem != null)
        //     {
        //         GameObject existing = dropSlot.currentItem;
        //         dropSlot.SetItem(gameObject);             // triggers event
        //         existing.transform.SetParent(originalSlot.transform);
        //         existing.GetComponent<RectTransform>().localPosition = new Vector3(.15f,.15f,1);
        //         existing.GetComponent<RectTransform>().anchoredPosition = new Vector2(.15f,.15f);
        //         // originalSlot.SetItem(existing);           // triggers event
        //     }
        //     else
        //     {
        //         dropSlot.SetItem(gameObject);             // triggers event
        //         originalSlot.ClearItem();                 // triggers event
        //     }

        //     rectTransform.SetParent(dropSlot.transform);
        //     // rectTransform.localPosition = Vector3.zero;
        //     // rectTransform.localScale = Vector2.one;
        // }
        // else
        // {
        //     rectTransform.SetParent(originalSlot.transform);
        //     rectTransform.localPosition = Vector3.zero;
        //     rectTransform.localScale = Vector3.one;
        //     originalSlot.SetItem(gameObject);             // triggers event
        // }

        rectTransform.anchoredPosition = Vector2.zero;
    }
    public Item item; // the Item component on this object

    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log($"[CLICK] Item clicked: {item.Name}");

        // Check if this item is the twig
        if (item.uiItemData.itemName.ToLower() == "twig" || item.uiItemData.itemName.ToLower() == "wood sword" || item.uiItemData.itemName.ToLower() == "silver sword" || item.uiItemData.itemName.ToLower() == "pumpkin scythe" || item.uiItemData.itemName.ToLower() == "dual swords" || item.uiItemData.itemName.ToLower() == "hammer")
        {
             Debug.Log($"A WEAPON HAS BEEN CLICKED");
        }

    }
    void EquipToSlot(Slot equipSlot)
    {
        // Move item to equipment slot
        transform.SetParent(equipSlot.transform, false);
        transform.localPosition = Vector3.zero;
        equipSlot.SetItem(gameObject);

        // Clear original inventory slot
        originalSlot.ClearItem();
    }

    void SwapWithEquipSlot(Slot equipSlot)
    {
        GameObject equippedItem = equipSlot.currentItem;

        // Move equipped item back to inventory
        equippedItem.transform.SetParent(originalSlot.transform, false);
        equippedItem.transform.localPosition = Vector3.zero;
        originalSlot.SetItem(equippedItem);

        // Move clicked item to equipment slot
        transform.SetParent(equipSlot.transform, false);
        transform.localPosition = Vector3.zero;
        equipSlot.SetItem(gameObject);
    }

}
