using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI EndGameText;
    public int Index = 0;

    private bool _isEnd = false;

    public void End() //запуск конца игры
    {
        BackgroundMusic.Instance.Play("Ending", 0.1f);

        transform.gameObject.SetActive(true);
        GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(false);
        GameStorage.Instance.PlayerInfoScript.PlayerUIScript.OffText();

        GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();
        EndGameText.text = "Конец игры";
        _isEnd = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isEnd) //при нажатии на пробел перелистывать итоги
        {
            switch(Index)
            {
                case 0:
                    EndGameText.text = "Итоги";
                    break;

                case 1:
                    EndGameText.text += $"\nИгрок получил баллы:\nРусский - {GameStorage.Instance.GameManagerScript.RussianLanguageScores}\nИстория - {GameStorage.Instance.GameManagerScript.HistoryScores}\nМатематика - {GameStorage.Instance.GameManagerScript.MathScores}\nВ сумме - {GameStorage.Instance.GameManagerScript.RussianLanguageScores + GameStorage.Instance.GameManagerScript.HistoryScores + GameStorage.Instance.GameManagerScript.MathScores}.";
                    break;

                case 2:
                    EndGameText.text += $"\n\nУмник получил баллы:\nРусский - {GameStorage.Instance.GameManagerScript.SmartGuyRussianLanguageScores}\nИстория - {GameStorage.Instance.GameManagerScript.SmartGuyHistoryScores}\nМатематика - {GameStorage.Instance.GameManagerScript.SmartGuyMathScores}\nВ сумме - {GameStorage.Instance.GameManagerScript.SmartGuyRussianLanguageScores + GameStorage.Instance.GameManagerScript.SmartGuyHistoryScores + GameStorage.Instance.GameManagerScript.SmartGuyMathScores}.";
                    break;

                case 3:
                    if (GameStorage.Instance.GameManagerScript.RussianLanguageScores > GameStorage.Instance.GameManagerScript.HistoryScores && GameStorage.Instance.GameManagerScript.RussianLanguageScores > GameStorage.Instance.GameManagerScript.MathScores)
                    {
                        EndGameText.text = "Потенциально лучший вектор развития - русский язык";
                        return;
                    }

                    if (GameStorage.Instance.GameManagerScript.HistoryScores > GameStorage.Instance.GameManagerScript.RussianLanguageScores && GameStorage.Instance.GameManagerScript.HistoryScores > GameStorage.Instance.GameManagerScript.MathScores)
                    {
                        EndGameText.text = "Потенциально лучший вектор развития - история";
                        return;
                    }

                    if (GameStorage.Instance.GameManagerScript.MathScores > GameStorage.Instance.GameManagerScript.HistoryScores && GameStorage.Instance.GameManagerScript.MathScores > GameStorage.Instance.GameManagerScript.RussianLanguageScores)
                    {
                        EndGameText.text = "Потенциально лучший вектор развития - математика";
                        return;
                    }

                    EndGameText.text = "Так как были показаны одинаковые результаты по нескольким дисциплинам - потенциально лучший вектор развития может быть огромным";
                    break;
            }

            Index++;
        }
    }
}
