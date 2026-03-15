using UnityEngine;

public class TrashUI : MonoBehaviour
{
    public bool RectTransformsIntersect(RectTransform itemRect)
    {
        RectTransform trashRect = GetComponent<RectTransform>();
        Rect rect1 = GetWorldRect(trashRect);
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