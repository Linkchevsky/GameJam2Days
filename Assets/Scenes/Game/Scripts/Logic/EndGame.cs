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

    public void End() //������ ����� ����
    {
        BackgroundMusic.Instance.Play("Ending", 0.1f);

        transform.gameObject.SetActive(true);
        GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(false);
        GameStorage.Instance.PlayerInfoScript.PlayerUIScript.OffText();

        GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();
        EndGameText.text = "����� ����";
        _isEnd = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isEnd) //��� ������� �� ������ ������������� �����
        {
            switch(Index)
            {
                case 0:
                    EndGameText.text = "�����";
                    break;

                case 1:
                    EndGameText.text += $"\n����� ������� �����:\n������� - {GameStorage.Instance.GameManagerScript.RussianLanguageScores}\n������� - {GameStorage.Instance.GameManagerScript.HistoryScores}\n���������� - {GameStorage.Instance.GameManagerScript.MathScores}\n� ����� - {GameStorage.Instance.GameManagerScript.RussianLanguageScores + GameStorage.Instance.GameManagerScript.HistoryScores + GameStorage.Instance.GameManagerScript.MathScores}.";
                    break;

                case 2:
                    EndGameText.text += $"\n\n����� ������� �����:\n������� - {GameStorage.Instance.GameManagerScript.SmartGuyRussianLanguageScores}\n������� - {GameStorage.Instance.GameManagerScript.SmartGuyHistoryScores}\n���������� - {GameStorage.Instance.GameManagerScript.SmartGuyMathScores}\n� ����� - {GameStorage.Instance.GameManagerScript.SmartGuyRussianLanguageScores + GameStorage.Instance.GameManagerScript.SmartGuyHistoryScores + GameStorage.Instance.GameManagerScript.SmartGuyMathScores}.";
                    break;

                case 3:
                    if (GameStorage.Instance.GameManagerScript.RussianLanguageScores > GameStorage.Instance.GameManagerScript.HistoryScores && GameStorage.Instance.GameManagerScript.RussianLanguageScores > GameStorage.Instance.GameManagerScript.MathScores)
                    {
                        EndGameText.text = "������������ ������ ������ �������� - ������� ����";
                        return;
                    }

                    if (GameStorage.Instance.GameManagerScript.HistoryScores > GameStorage.Instance.GameManagerScript.RussianLanguageScores && GameStorage.Instance.GameManagerScript.HistoryScores > GameStorage.Instance.GameManagerScript.MathScores)
                    {
                        EndGameText.text = "������������ ������ ������ �������� - �������";
                        return;
                    }

                    if (GameStorage.Instance.GameManagerScript.MathScores > GameStorage.Instance.GameManagerScript.HistoryScores && GameStorage.Instance.GameManagerScript.MathScores > GameStorage.Instance.GameManagerScript.RussianLanguageScores)
                    {
                        EndGameText.text = "������������ ������ ������ �������� - ����������";
                        return;
                    }

                    EndGameText.text = "��� ��� ���� �������� ���������� ���������� �� ���������� ����������� - ������������ ������ ������ �������� ����� ���� ��������";
                    break;
            }

            Index++;
        }
    }
}
