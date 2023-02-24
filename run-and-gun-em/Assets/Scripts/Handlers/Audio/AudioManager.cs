using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null)
            sound.source.Play();
        else
        {
            Debug.Log("Could not play audio " + name + ", did not find!");
            return;
        }
    }
}
