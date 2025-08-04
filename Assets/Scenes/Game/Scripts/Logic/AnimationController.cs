using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator PlayerAnimator;

    private void Start()
    {
        AllEvent.s_playerWalked += PlayerAnimControl; //�������� �� �������
    }

    private void PlayerAnimControl(float value) //��� ������ ������� (������), ������������ �����
    {
        PlayerAnimator.SetFloat("walk", value);
    }
}
