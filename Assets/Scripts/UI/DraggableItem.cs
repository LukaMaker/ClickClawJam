using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    private Transform originalParent;
    private TrashUI trashUI;

    private void Start()
    {
        originalParent = transform.parent;
        trashUI = FindObjectOfType<TrashUI>();
    }

    //public void DragHandler(BaseEventData data)
    //{
    //    PointerEventData pointerEventData = (PointerEventData)data;
    //    Vector2 position;

    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(
    //        (RectTransform)canvas.transform,
    //        pointerEventData.position,
    //        canvas.worldCamera,
    //        out position
    //    );

    //    transform.position = canvas.transform.TransformPoint(position);
    //}

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;

        UISlot currentSlot = parentAfterDrag.GetComponent<UISlot>();
        if (currentSlot != null)
        {
            currentSlot.RemoveItem(this);
        }

        Image itemImage = GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.color = Color.white;
        }

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            transform.SetParent(canvas.transform);
        }
        else
        {
            transform.SetParent(transform.root);
        }
        
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;

        RectTransform itemRect = GetComponent<RectTransform>();
        bool hoveringTrash = trashUI != null && trashUI.RectTransformsIntersect(itemRect);

        Image itemImage = GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.color = hoveringTrash ? Color.red : Color.white;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        
        RectTransform itemRect = GetComponent<RectTransform>();

        if (trashUI != null && trashUI.RectTransformsIntersect(itemRect))
        {
            Destroy(gameObject);
            return;
        }

        UISlot[] slots = FindObjectsOfType<UISlot>();
        bool placedInSlot = false;

        foreach (UISlot slot in slots)
        {
            if (slot.RectTransformsIntersect(itemRect))
            {
                parentAfterDrag = slot.transform;
                slot.AddItem(this);
                placedInSlot = true;
                break;
            }
        }

        if (!placedInSlot)
        {
            parentAfterDrag = originalParent;
        }

        transform.SetParent(parentAfterDrag);

        Image itemImage = GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.color = placedInSlot ? Color.yellow : Color.white;
        }
    }
}
