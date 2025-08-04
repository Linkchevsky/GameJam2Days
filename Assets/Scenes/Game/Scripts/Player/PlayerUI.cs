using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfoScript;

    public TextMeshProUGUI PlayerText;
    private bool ShowedPop_upText = false;


    public void OffText() => PlayerText.color = new Color(1, 1, 1, 0); //выключение текста над игроком
    public void ShowText(string text) //включение текста над игроком
    {
        PlayerText.color = new Color(1, 1, 1, 1);
        PlayerText.text = text;
    }


    public void ShowPop_upText(string text) //показ всплывающего текста над игроком
    {
        PlayerText.text = text;
        PlayerText.color = new Color(1, 1, 1, 1);
        ShowedPop_upText = true;
    }



    private void Update()
    {
        if (ShowedPop_upText) //если всплывающий над игроком текст показан - постепенно убирать его путём увеличения прозрачности
        {
            PlayerText.color = new Color(1, 1, 1, PlayerText.color.a - 0.6f * Time.deltaTime);
            if (PlayerText.color.a <= 0)
                ShowedPop_upText = false;
        }
    }
}
