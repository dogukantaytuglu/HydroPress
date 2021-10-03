using UnityEngine.Audio;
using System;
using TMPro.Examples;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public GameObject musicIcon;

    private bool noSoundIconActive = false;
    private int isMuted;

    // Start is called before the first frame update
    void Awake()
    {
        isMuted = PlayerPrefs.GetInt("IsMuted");

        if (PlayerPrefs.GetInt("IsMuted") == 1)
        {
            musicIcon.gameObject.SetActive(true);
            noSoundIconActive = true;
        }
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
        }
    }
    
    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return;
        }

        sound.source.Play();
    }


    public void SetPitch(string name, float pitch)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return;
        }

        sound.source.pitch = pitch;
    }
    public void SetVolume(string name, float volume)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return;
        }
        sound.source.volume = volume;
    }

    public void SoundButtonFunction()
    {
        noSoundIconActive = !noSoundIconActive;

        if (isMuted == 0)
        {
            isMuted = 1;
        }
        else if(isMuted == 1)
        {
            isMuted = 0;
        }

        PlayerPrefs.SetInt("IsMuted",isMuted);

        foreach (var sound in sounds)
        {
            sound.source.mute = !sound.source.mute;
        }
        musicIcon.gameObject.SetActive(noSoundIconActive);
    }

    public void MuteOnPlay()
    {
        if (PlayerPrefs.GetInt("IsMuted") == 1)
        {
            foreach (var sound in sounds)
            {
                sound.source.mute = !sound.source.mute;
            }
            musicIcon.gameObject.SetActive(noSoundIconActive);
        }
    }
}
