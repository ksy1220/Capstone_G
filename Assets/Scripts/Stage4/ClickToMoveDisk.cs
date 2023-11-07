using UnityEngine;

public class ClickToMoveDisk : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 offset;
    private Vector2 originalPosition;
    private bool[] isInsideDropArea; // 여러 개의 드롭 영역을 나타내는 배열
    public RectTransform[] dropAreas; // 드롭 영역들의 RectTransform 배열

    private void Start()
    {
        originalPosition = GetComponent<RectTransform>().anchoredPosition;
        isInsideDropArea = new bool[dropAreas.Length];
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = GetComponent<RectTransform>().anchoredPosition - GetMouseUIPosition();
    }

    private void OnMouseUp()
    {
        isDragging = false;
        bool isInsideAnyDropArea = false;

        for (int i = 0; i < dropAreas.Length; i++)
        {
            if (isInsideDropArea[i])
            {
                // 어떤 드롭 영역 내부에 들어갔을 때, 고정 위치로 이동
                GetComponent<RectTransform>().anchoredPosition = dropAreas[i].anchoredPosition;
                isInsideAnyDropArea = true;
                break;
            }
        }

        if (!isInsideAnyDropArea)
        {
            // 모든 드롭 영역 밖으로 이동하면 원래 위치로 되돌림
            GetComponent<RectTransform>().anchoredPosition = originalPosition;
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 newPosition = GetMouseUIPosition() + offset;
            GetComponent<RectTransform>().anchoredPosition = newPosition;
            CheckIfInsideDropAreas();
        }
    }

    private Vector2 GetMouseUIPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        RectTransform canvasRect = dropAreas[0].parent.GetComponent<RectTransform>();
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mousePosition, null, out localPoint))
        {
            return localPoint;
        }

        return Vector2.zero;
    }

    private void CheckIfInsideDropAreas()
    {
        for (int i = 0; i < dropAreas.Length; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(dropAreas[i], Input.mousePosition))
            {
                isInsideDropArea[i] = true;
            }
            else
            {
                isInsideDropArea[i] = false;
            }
        }
    }
}
