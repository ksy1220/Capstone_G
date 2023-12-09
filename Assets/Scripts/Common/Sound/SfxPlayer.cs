using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SfxPlayer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    bool playOnEnable = false;

    public Sfx sfx;

    void OnEnable()
    {
        if (playOnEnable)
            PlaySFX();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!playOnEnable)
            PlaySFX();
    }

    void PlaySFX()
    {
        SoundManager.instance.PlaySFX(sfx);
    }
}
