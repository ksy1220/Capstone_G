using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public string userName;

    // key: category, value: user's choice index
    public Dictionary<string, int> Choices = new Dictionary<string, int>();

    public int mentalIndex;

    public int progress;
}
