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

    public struct HistoryQest //структура для сохранения вопросов
    {
        public string Quest;

        public string Answer1;
        public string Answer2;
        public string Answer3;
        public string Answer4;

        public int CorrectAnswerNumber;
    }

    private List<HistoryQest> HistoryQuests = new List<HistoryQest>() //список вопросов с ответами
    {
        new HistoryQest{ Quest = "Кто из князей считается основателем древнерусского государства?" , Answer1 = "Владимир Святославич" , Answer2 = "Игорь Рюрикович" , Answer3 = "Олег Вещий" , Answer4 = "Ярослав Мудрый" , CorrectAnswerNumber = 3} ,

        new HistoryQest{ Quest = "Какое событие произошло в 988 году на Руси?" , Answer1 = "Крещение Руси" , Answer2 = "Убийство Бориса и Глеба" , Answer3 = "Норманская завоевание" , Answer4 = "Смерть Ярослава Мудрого" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "Какой кодекс законов был принят в Руси при Ярославе Мудром?" , Answer1 = "Русская правда" , Answer2 = "Устав Владимира" , Answer3 = "Судебник" , Answer4 = "Литовский статут" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "Какой город стал центром Русского государства после падения Киева?" , Answer1 = "Владимир" , Answer2 = "Новгород" , Answer3 = "Москва" , Answer4 = "Смоленск" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "Кто был первым царем всея Руси?" , Answer1 = "Иван III" , Answer2 = "Иван IV (Грозный)" , Answer3 = "Петр I" , Answer4 = "Федор I" , CorrectAnswerNumber = 2} ,

        new HistoryQest{ Quest = "Какой князь основал Москву?" , Answer1 = "Юрий Долгорукий" , Answer2 = "Иван III" , Answer3 = "Дмитрий Донской" , Answer4 = "Владимир Мономах" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "Какое сражение стало решающим в борьбе с монголо-татарским нашествием?" , Answer1 = "Битва на Калке" , Answer2 = "Куликовская битва" , Answer3 = "Битва при Молодях" , Answer4 = "Ледовое побоище" , CorrectAnswerNumber = 2} ,

        new HistoryQest{ Quest = "Кто из русских правителей принял титул 'царя'?" , Answer1 = "Василий III" , Answer2 = "Иван III" , Answer3 = "Иван IV" , Answer4 = "Петр I" , CorrectAnswerNumber = 3} ,

        new HistoryQest{ Quest = "Как назывался документ, регулирующий отношения между княжествами в конце XV века?" , Answer1 = "Судебник" , Answer2 = "Устав" , Answer3 = "Грамота" , Answer4 = "Хартия" , CorrectAnswerNumber = 1} ,

        new HistoryQest{ Quest = "В каком году произошло стояние на реке Угре, завершившее период монголо-татарского ига?" , Answer1 = "1380" , Answer2 = "1480" , Answer3 = "1505" , Answer4 = "1552" , CorrectAnswerNumber = 2}
    };



    public void StartEvent() //начало теста
    {
        if (_currentIndex == 0)
        {
            transform.gameObject.SetActive(true);
            StartCoroutine(StartEventTimer());
        }
    }
    public IEnumerator StartEventTimer() //начало теста с 2 секундной паузой
    {
        QuestText.text = $"Вопрос {_currentIndex + 1}: {HistoryQuests[_currentIndex].Quest}";
        GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(false);
        GameStorage.Instance.PlayerInfoScript.PlayerUIScript.OffText();

        GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();
        yield return new WaitForSeconds(2f);

        _answering = true;
        NextQuestion(_currentIndex);
    }


    public void Answer(int answerNumber) //проверка ответа
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


    private IEnumerator AnswerTaimer(int buttonIndex, bool correct) //мигание цвета соотвествующе правильному/неправильному ответу
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


    private void NextQuestion(int index) //переключение вопросов
    {
        _timeBar.fillAmount = 1;
        StartCoroutine(ResponseTime());

        QuestText.text = $"Вопрос {index + 1}: {HistoryQuests[index].Quest}";

        HistoryButtons[0].text = HistoryQuests[index].Answer1;
        HistoryButtons[1].text = HistoryQuests[index].Answer2;
        HistoryButtons[2].text = HistoryQuests[index].Answer3;
        HistoryButtons[3].text = HistoryQuests[index].Answer4;
    }


    private IEnumerator ResponseTime() //время на ответ
    {
        yield return new WaitForSeconds(Time.deltaTime);
        
        if ((_timeBar.fillAmount -= Time.deltaTime / 10) > 0 && _answering)
            StartCoroutine(ResponseTime());
        else if (_timeBar.fillAmount <= 0)
            StartCoroutine(TimeOut());
    }


    private IEnumerator TimeOut() //выполняется если время на ответ закончилось
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


    //нажатия кнопок для ответов на вопросы
    public void Button1Pressed() => Answer(1);
    public void Button2Pressed() => Answer(2);
    public void Button3Pressed() => Answer(3);
    public void Button4Pressed() => Answer(4);
}