using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    #region Singleton
    public static AudioManager instance;

    public static AudioManager Get()
    {
        return instance;
    }
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public List<AudioSource> soundSources;
    public List<AudioSource> musicSources;
    public Sprite onSound;
    public Sprite offSound;
    public Sprite onMusic;
    public Sprite offMusic;
    public Image imageSound;
    public Image imageMusic;
    private bool soundsActivated = true;
    private bool musicActivated = true;

    private void Start()
    {
        soundSources = new List<AudioSource>();
        musicSources = new List<AudioSource>();
    }

    private void Update()
    {
        for (int i = soundSources.Count - 1; i >= 0; i--)
        {
            if (soundSources[i] == null)
            {
                soundSources.Remove(soundSources[i]);
            }
        }
    }

    public void AddSound(AudioSource s)
    {
        soundSources.Add(s);
    }

    public void AddMusic(AudioSource m)
    {
        musicSources.Add(m);
    }

    public void SetOnOffSounds()
    {
        soundsActivated = !soundsActivated;
        if (soundsActivated)
            imageSound.sprite = onSound;
        else
            imageSound.sprite = offSound;
        foreach (AudioSource aSource in soundSources)
        {
            aSource.mute = !soundsActivated;
        }
    }

    public void SetOnOffMusic()
    {
        musicActivated = !musicActivated;
        if (musicActivated)
            imageMusic.sprite = onMusic;
        else
            imageMusic.sprite = offMusic;
        foreach (AudioSource aSource in musicSources)
        {
            aSource.mute = !musicActivated;
        }
    }

}
