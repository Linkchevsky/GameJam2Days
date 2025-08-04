using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLogic : MonoBehaviour
{
    private void Start()
    {
       AllEvent.s_playerWalked += PlayerWalkSound;
    }

    private void PlayerWalkSound(float value)
    {
        if (!AudioManager.Instance.CurrentSound.source.isPlaying && value != 0) //проверка на проигрывается ли звук ходьбы в данный момент
            AudioManager.Instance.Play("walk", 0.1f);
    }

    private void PlayerDoorSound(float value)
    {
        AudioManager.Instance.Play("door", 1f);
    }
}
