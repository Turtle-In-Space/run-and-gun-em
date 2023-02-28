using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

   
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        SetUpAudio();
    }    

    private void Start()
    {
        Play("MainTheme");
    }

    /*
     * Spelar upp ljudfilen med namn "name"
     */
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

    /*
     * Skapar en ljudkälla för varje ljud tillagt i editorn
     */
    private void SetUpAudio()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }
}
