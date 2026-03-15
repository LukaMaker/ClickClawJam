using System.Collections.Generic;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    public BaseDepartment department;
    public List<DraggableItem> slottedItems = new List<DraggableItem>();

    public void AddItem(DraggableItem item)
    {
        if (!slottedItems.Contains(item))
        {
            slottedItems.Add(item);
        }
    }

    public void RemoveItem(DraggableItem item)
    {
        if (slottedItems.Contains(item))
        {
            slottedItems.Remove(item);
        }
    }

    public bool RectTransformsIntersect(RectTransform itemRect)
    {
        RectTransform slotRect = GetComponent<RectTransform>();
        Rect rect1 = GetWorldRect(slotRect);
        Rect rect2 = GetWorldRect(itemRect);
        return rect1.Overlaps(rect2);
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];
        return new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }
}
