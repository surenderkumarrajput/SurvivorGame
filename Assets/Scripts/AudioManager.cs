using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Audio[] audioarray;
    public static AudioManager instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (var s in audioarray)
        {
            s.audiosource = gameObject.AddComponent<AudioSource>();
            s.audiosource.clip = s.audio;
            s.audiosource.pitch = s.pitch;
            s.audiosource.loop = s.isloop;
            s.audiosource.volume = s.volume;
        }
    }
    private void Start()
    {
        play("Theme");
    }
    public void play(string name)
    {
        Audio a = Array.Find(audioarray, audioarray => audioarray.name==name);
        a.audiosource.Play();
    }
    public void stop(string name)
    {
        Audio a = Array.Find(audioarray, audioarray => audioarray.name == name);
        a.audiosource.Stop();
    }
}
