using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;
    public Sound[] sounds = null;

    private bool _pause;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; //объ€вление синглтона

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
        Play("Opening", 0.1f); //дл€ безпроблемной проверки звука
    }


    public Sound CurrentSound;
    public void Play(string name, float volume) //проигрывание фоновой музыки
    {
        if (CurrentSound.source != null)
            CurrentSound.source.Pause();

        CurrentSound = Array.Find(sounds, sound => sound.name == name);
        CurrentSound.source.volume = volume;
        CurrentSound.source.loop = true;
        if (_pause)
            CurrentSound.source.mute = true;

        CurrentSound.source.Play();
    }  


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //если нажата P - уменьшить громкость музыки до 0
        {
            if (!CurrentSound.source.mute)
            {
                _pause = true;
                CurrentSound.source.mute = true;
                return;
            }

            _pause = false;
            CurrentSound.source.mute = false;
        }
    }
}
