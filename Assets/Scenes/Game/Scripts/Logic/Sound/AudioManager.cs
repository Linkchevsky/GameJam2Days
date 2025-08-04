using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds = null;
    private void Awake()
    {
        //установка синглтона
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; 

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
        Play("walk", 0f); //костыль для безпроблемной проверки звука и исключения ошибок
    }


    public Sound CurrentSound;
    public void Play(string name, float volume) //вопроизведение звука
    {
        CurrentSound = Array.Find(sounds, sound => sound.name == name);
        CurrentSound.source.volume = volume;
        CurrentSound.source.Play();
    }
}
