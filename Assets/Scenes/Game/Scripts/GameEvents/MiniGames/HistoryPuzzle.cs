using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static HistoryPuzzle;

public class HistoryPuzzle : MonoBehaviour
{
    [SerializeField] private Image _timeBar;

    private bool _answering = false;
    private int _currentIndex = 0;

    public int _numberOfCorrectAnswers = 0;
    public TextMeshProUGUI QuestText;
    public TextMeshProUGUI[] HistoryButtons = new TextMeshProUGUI[4];

    public struct HistoryQest //��������� ��� ���������� ��������
    {
        public string Quest;

        public string Answer1;
        public string Answer2;
        public string Answer3;
        public string Answer4;

        public int CorrectAnswerNumber;
    }

    private List<HistoryQest> HistoryQuests = new List<HistoryQest>() //������ �������� � ��������
    {
        new HistoryQest{ Quest = "��� �� ������ ��������� ����������� �������������� �����������?" , Answer1 = "�������� �����������" , Answer2 = "����� ���������" , Answer3 = "���� �����" , Answer4 = "������� ������" , CorrectAnswerNumber = 3} ,

        new HistoryQest{ Quest = "����� ������� ��������� � 988 ���� �� ����?" , Answer1 = "�������� ����" , Answer2 = "�������� ������ � �����" , Answer3 = "���������� ����������" , Answer4 = "������ �������� �������" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "����� ������ ������� ��� ������ � ���� ��� �������� ������?" , Answer1 = "������� ������" , Answer2 = "����� ���������" , Answer3 = "��������" , Answer4 = "��������� ������" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "����� ����� ���� ������� �������� ����������� ����� ������� �����?" , Answer1 = "��������" , Answer2 = "��������" , Answer3 = "������" , Answer4 = "��������" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "��� ��� ������ ����� ���� ����?" , Answer1 = "���� III" , Answer2 = "���� IV (�������)" , Answer3 = "���� I" , Answer4 = "����� I" , CorrectAnswerNumber = 2} ,

        new HistoryQest{ Quest = "����� ����� ������� ������?" , Answer1 = "���� ����������" , Answer2 = "���� III" , Answer3 = "������� �������" , Answer4 = "�������� �������" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "����� �������� ����� �������� � ������ � �������-��������� ����������?" , Answer1 = "����� �� �����" , Answer2 = "����������� �����" , Answer3 = "����� ��� �������" , Answer4 = "������� �������" , CorrectAnswerNumber = 2} ,

        new HistoryQest{ Quest = "��� �� ������� ���������� ������ ����� '����'?" , Answer1 = "������� III" , Answer2 = "���� III" , Answer3 = "���� IV" , Answer4 = "���� I" , CorrectAnswerNumber = 3} ,

        new HistoryQest{ Quest = "��� ��������� ��������, ������������ ��������� ����� ����������� � ����� XV ����?" , Answer1 = "��������" , Answer2 = "�����" , Answer3 = "�������" , Answer4 = "������" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "� ����� ���� ��������� ������� �� ���� ����, ����������� ������ �������-���������� ���?" , Answer1 = "1380" , Answer2 = "1480" , Answer3 = "1505" , Answer4 = "1552" , CorrectAnswerNumber = 2}
    };



    public void StartEvent() //������ �����
    {
        if (_currentIndex == 0)
        {
            transform.gameObject.SetActive(true);
            StartCoroutine(StartEventTimer());
        }
    }
    public IEnumerator StartEventTimer() //������ ����� � 2 ��������� ������
    {
        QuestText.text = $"������ {_currentIndex + 1}: {HistoryQuests[_currentIndex].Quest}";
        GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(false);
        GameStorage.Instance.PlayerInfoScript.PlayerUIScript.OffText();

        GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();
        yield return new WaitForSeconds(2f);

        _answering = true;
        NextQuestion(_currentIndex);
    }


    public void Answer(int answerNumber) //�������� ������
    {
        if (_answering)
        {
            _answering = false;

            StopCoroutine(ResponseTime());

            if (HistoryQuests[_currentIndex].CorrectAnswerNumber == answerNumber)
            {
                _numberOfCorrectAnswers++;
                StartCoroutine(AnswerTaimer(answerNumber, true));
            }
            else
                StartCoroutine(AnswerTaimer(answerNumber, false));
        }
    }


    private IEnumerator AnswerTaimer(int buttonIndex, bool correct) //������� ����� ������������� �����������/������������� ������
    {
        if (correct)
            HistoryButtons[buttonIndex - 1].color = Color.green;
        else
            HistoryButtons[buttonIndex - 1].color = Color.red;

        yield return new WaitForSeconds(1);

        HistoryButtons[buttonIndex - 1].color = Color.white;

        _currentIndex++;
        if (_currentIndex >= HistoryQuests.Count)
        {
            GameStorage.Instance.GameManagerScript.HistoryScores = _numberOfCorrectAnswers;
            GameStorage.Instance.GameManagerScript.ChangePuzzleCount();
            GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(true);
            GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();

            transform.gameObject.SetActive(false);
            _answering = false;
            yield return null;
        }

        _answering = true;
        NextQuestion(_currentIndex);
    }


    private void NextQuestion(int index) //������������ ��������
    {
        _timeBar.fillAmount = 1;
        StartCoroutine(ResponseTime());

        QuestText.text = $"������ {index + 1}: {HistoryQuests[index].Quest}";

        HistoryButtons[0].text = HistoryQuests[index].Answer1;
        HistoryButtons[1].text = HistoryQuests[index].Answer2;
        HistoryButtons[2].text = HistoryQuests[index].Answer3;
        HistoryButtons[3].text = HistoryQuests[index].Answer4;
    }


    private IEnumerator ResponseTime() //����� �� �����
    {
        yield return new WaitForSeconds(Time.deltaTime);
        
        if ((_timeBar.fillAmount -= Time.deltaTime / 10) > 0 && _answering)
            StartCoroutine(ResponseTime());
        else if (_timeBar.fillAmount <= 0)
            StartCoroutine(TimeOut());
    }


    private IEnumerator TimeOut() //����������� ���� ����� �� ����� �����������
    {
        foreach (TextMeshProUGUI buttonText in HistoryButtons)
            buttonText.color = Color.red;

        yield return new WaitForSeconds(1);

        foreach (TextMeshProUGUI buttonText in HistoryButtons)
            buttonText.color = Color.white;

        _currentIndex++;
        if (_currentIndex >= HistoryQuests.Count)
        {
            GameStorage.Instance.GameManagerScript.HistoryScores = _numberOfCorrectAnswers;
            GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(true);
            GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();

            transform.gameObject.SetActive(false);
            _answering = false;
            GameStorage.Instance.GameManagerScript.ChangePuzzleCount();
            yield return null;
        }

        _answering = true;
        NextQuestion(_currentIndex);
    }


    //������� ������ ��� ������� �� �������
    public void Button1Pressed() => Answer(1);
    public void Button2Pressed() => Answer(2);
    public void Button3Pressed() => Answer(3);
    public void Button4Pressed() => Answer(4);
}