using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    // 멘탈지수
    public int mentalIndex;

    // 게임의 진행도
    public Progress progress;

    // 미니게임 결과: 필요한 경우에만 저장
    public bool miniGameSucceed;
}
