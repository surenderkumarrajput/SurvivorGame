using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio 
{
    [HideInInspector]
    public AudioSource audiosource;
    public string name;
    [Range(0,1)]
    public float volume;
    [Range(0,1)]
    public float pitch;
    public bool isloop;
    public AudioClip audio;
}
