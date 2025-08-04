using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator PlayerAnimator;

    private void Start()
    {
        AllEvent.s_playerWalked += PlayerAnimControl; //подписка на событие
    }

    private void PlayerAnimControl(float value) //при вызове события (ходьбы), активировать метод
    {
        PlayerAnimator.SetFloat("walk", value);
    }
}
