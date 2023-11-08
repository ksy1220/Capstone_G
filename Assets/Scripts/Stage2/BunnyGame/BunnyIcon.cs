using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyIcon : MonoBehaviour
{
    public void SetPosition(Transform trans)
    {
        gameObject.SetActive(true);
        transform.position = trans.position;
    }
}
