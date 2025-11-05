using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;

    [HideInInspector] public Transform parentAfterDrag;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;

        // Find the DragLayer in the canvas
        Transform dragLayer = canvas.transform.Find("DragLayer");
        transform.SetParent(dragLayer != null ? dragLayer : canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move with the mouse
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);

        // Reset position relative to the slot
        RectTransform rect = transform as RectTransform;
        rect.anchoredPosition = Vector2.zero;
        rect.localScale = Vector3.one;
    }
}
