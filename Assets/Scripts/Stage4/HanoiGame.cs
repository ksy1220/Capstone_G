using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiGame : MonoBehaviour
{
    public RectTransform hanoi1, hanoi2, hanoi3, hanoi4;
    public RectTransform leftTower, middleTower, rightTower;

    // Start is called before the first frame update
    void Start()
    {
        //첫번째 tower의 x좌표는 -350, 두번째 tower의 x좌표는 0, 세번째 타워의 x좌표는 360
        //각 원반의 초기 위치 설정
        Vector2 startPositionHanoi1 = new Vector2(-350f, -374f);//1층
        Vector2 startPositionHanoi2 = new Vector2(-350f, -315f);//2층
        Vector2 startPositionHanoi3 = new Vector2(-350f, -256f);//3층
        Vector2 startPositionHanoi4 = new Vector2(-350f, -197f);//4층
        hanoi1.anchoredPosition = startPositionHanoi1;
        hanoi2.anchoredPosition = startPositionHanoi2;
        hanoi3.anchoredPosition = startPositionHanoi3;
        hanoi4.anchoredPosition = startPositionHanoi4;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
