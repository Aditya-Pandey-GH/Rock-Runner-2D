using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        foreach (Sounds s in sounds)
        {
            s.src = gameObject.AddComponent<AudioSource>();
            s.src.clip = s.clip;
            s.src.volume = s.volume;
            s.src.loop = s.loop;
        }
    }
    void Start()
    {
        PlayAudio("BGM");
    }

    public void PlayAudio(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("File with the name " + name + " was not found");
            return;
        }
        s.src.Play();
    }
}
