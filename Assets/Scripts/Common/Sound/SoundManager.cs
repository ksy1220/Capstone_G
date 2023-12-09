using System.Collections;
using UnityEngine;

public enum Sfx
{
    button_ui, button_click, success, fail
}


public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance = null;
    public static SoundManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("@SoundManager"));
                _instance = obj.GetComponent<SoundManager>();
            }
            return _instance;
        }
    }

    [Header("BGM")]

    public AudioSource bgmPlayer;

    [SerializeField]
    AudioClip BGM_MainTitle, BGM_Ending;
    public float bgmVolume = 0.5f;


    [Header("SFX")]
    [SerializeField]
    AudioClip[] sfxClips;
    int channel = 8;
    public float sfxVolume = 0.5f;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public float totalVolume = 0;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);

        bgmPlayer = GetComponent<AudioSource>();
    }

    void Start()
    {
        Init();
    }


    void Init()
    {
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channel];

        for (int i = 0; i < channel; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume * totalVolume;
        }

        bgmPlayer.volume = bgmVolume * totalVolume;
    }

    public void PlayMainBGM()
    {
        bgmPlayer.clip = BGM_MainTitle;
        bgmPlayer.Play();
    }

    public void PlayEndingBGM()
    {
        bgmPlayer.clip = BGM_Ending;
        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(Sfx sfx)
    {
        for (int i = 0; i < channel; i++)
        {
            int loopIndex = (i + channelIndex) % channel;
            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void StopSFX(Sfx sfx)
    {
        if (sfxPlayers == null) return;
        foreach (AudioSource source in sfxPlayers)
        {
            if (source.clip == sfxClips[(int)sfx])
            {
                source.clip = null;
                break;
            }
        }
    }

    public void StopAllSFX()
    {
        foreach (AudioSource source in sfxPlayers)
        {
            if (source.isPlaying)
            {
                source.clip = null;
            }
        }
    }

    public void ChangeTotalVolume(float volume)
    {
        totalVolume = volume;

        bgmPlayer.volume = volume * bgmVolume;
        for (int i = 0; i < channel; i++)
        {
            sfxPlayers[i].volume = volume * sfxVolume;
        }
    }

    public void ChangeBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmPlayer.volume = bgmVolume * totalVolume;
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxVolume = volume;
        for (int i = 0; i < channel; i++)
        {
            sfxPlayers[i].volume = sfxVolume * totalVolume;
        }
    }
}
