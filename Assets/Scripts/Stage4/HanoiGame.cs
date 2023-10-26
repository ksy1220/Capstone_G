using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiGame : MonoBehaviour
{
    public GameObject hanoi1, hanoi2, hanoi3, hanoi4;
    private GameObject selectedDisk;
    public Transform hanoiskeleton1;
    private Transform selectedTower;

    private bool isDragging = false;

    void Start()
    {
        // 초기 상태 설정
        hanoi1.transform.SetParent(hanoiskeleton1);
        hanoi2.transform.SetParent(hanoiskeleton1);
        hanoi3.transform.SetParent(hanoiskeleton1);
        hanoi4.transform.SetParent(hanoiskeleton1);

        selectedDisk = null;
        selectedTower = null;
    }

    void Update()
    {
        if (isDragging)
        {
            // 마우스 위치에 따라 원판 이동
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedDisk.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }

    public void StartDrag(GameObject disk)
    {
        if (!isDragging)
        {
            selectedDisk = disk;
            selectedTower = disk.transform.parent;
            isDragging = true;
        }
    }

    public void EndDrag(GameObject targetTower)
    {
        if (isDragging && CanMoveToTower(targetTower))
        {
            selectedDisk.transform.SetParent(targetTower.transform);
        }
        else
        {
            selectedDisk.transform.SetParent(selectedTower);
        }

        selectedDisk.transform.localPosition = Vector3.zero;
        isDragging = false;
        selectedDisk = null;
        selectedTower = null;
    }

    bool CanMoveToTower(GameObject targetTower)
    {
        // 움직일 수 있는지 검사 (원판 크기 순서 확인)
        Transform[] targetDisks = targetTower.GetComponentsInChildren<Transform>();
        if (targetDisks.Length == 0 || selectedDisk.transform.localScale.x < targetDisks[targetDisks.Length - 1].transform.localScale.x)
        {
            return true;
        }
        return false;
    }
}
