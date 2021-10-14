using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] music;
    public Sound[] effect;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volumne;
            s.source.loop = s.loop;
        }
        foreach (Sound s in effect)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volumne;
            s.source.loop = s.loop;
        }
        SetMusic();
        SetEffect();
    }
    //Music
    public void SetMusic()
    {
        if (PlayerPrefs.GetInt("MUSIC") == 0)
        {
            SuwariAwayGame.Music += PlayMainMusic;
            SuwariAwayGame.Music -= StopMainMusic;
        }
        else
        {
            SuwariAwayGame.Music -= PlayMainMusic;
            SuwariAwayGame.Music += StopMainMusic;
            SuwariAwayGame.Music?.Invoke();
        }
        SuwariAwayGame.Music?.Invoke();
    }
    

    public void PlayMainMusic()
    {
        PlayMusic("MainMusic");
    }
    public void StopMainMusic()
    {
        StopMusic("MainMusic");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(music, sound => sound.name == name);
        s.source.Play();
    }
    public void StopMusic(string name)
    {
        Sound s = Array.Find(music, sound => sound.name == name);
        s.source.Stop();
    }
    // Effect
    public void SetEffect()
    {
        if (PlayerPrefs.GetInt("EFFECT") == 0)
        {
            foreach(Sound i in effect)
            {
                i.volumne = 1;
            }
        }
        else
        {
            foreach (Sound i in effect)
            {
                i.volumne = 0;
            }
        }
    
        
    }
    public void PlayEffect(string name)
    {
        Sound s = Array.Find(effect, sound => sound.name == name);
        s.source.volume = s.volumne;
        s.source.Play();
    }
}