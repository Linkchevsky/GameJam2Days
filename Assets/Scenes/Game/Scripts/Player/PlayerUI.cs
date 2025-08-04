using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfoScript;

    public TextMeshProUGUI PlayerText;
    private bool ShowedPop_upText = false;


    public void OffText() => PlayerText.color = new Color(1, 1, 1, 0); //���������� ������ ��� �������
    public void ShowText(string text) //��������� ������ ��� �������
    {
        PlayerText.color = new Color(1, 1, 1, 1);
        PlayerText.text = text;
    }


    public void ShowPop_upText(string text) //����� ������������ ������ ��� �������
    {
        PlayerText.text = text;
        PlayerText.color = new Color(1, 1, 1, 1);
        ShowedPop_upText = true;
    }



    private void Update()
    {
        if (ShowedPop_upText) //���� ����������� ��� ������� ����� ������� - ���������� ������� ��� ���� ���������� ������������
        {
            PlayerText.color = new Color(1, 1, 1, PlayerText.color.a - 0.6f * Time.deltaTime);
            if (PlayerText.color.a <= 0)
                ShowedPop_upText = false;
        }
    }
}
