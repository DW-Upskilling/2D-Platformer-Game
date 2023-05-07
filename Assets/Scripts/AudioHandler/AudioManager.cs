using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                instance = new AudioManager();
            return instance;
        }
    }

    public Sound[] soundsList;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in soundsList)
        {

            AudioSource source = gameObject.AddComponent<AudioSource>();

            source.name = sound.name;
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.mute = sound.mute;
            source.loop = sound.loop;
            source.priority = sound.priority;

            sound.source = source;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Play("backgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(Sound s)
    {
        s.source.Play();
    }

    public void Play(string name)
    {
        Debug.Log("Play(string name):: " + name);
        Sound s = Array.Find(soundsList, s => s.name == name);
        s.source.Play();
    }

    public void Play(string name, bool isEnable)
    {
        Debug.Log("Play(string name, Boolean isEnable)" + name);
        Sound s = Array.Find(soundsList, s => s.name == name);
        s.source.enabled = isEnable;
    }
}
