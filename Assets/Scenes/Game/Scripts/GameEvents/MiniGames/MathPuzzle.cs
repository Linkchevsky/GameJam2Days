using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MathPuzzle : MonoBehaviour
{
    [SerializeField] private Image _timeBar;
    [SerializeField] private TextMeshProUGUI _answerInputField;
    [SerializeField] private TextMeshProUGUI _textMathematicalEquation;

    private bool _answering = false;
    private int _currentIndex = 0;

    public int _numberOfCorrectAnswers = 0;

    public struct MathQuest
    {
        public string Quest;

        public int CorrectAnswerNumber;
    }

    private List<MathQuest> MathQuests = new List<MathQuest>() //вопросы для теста с ответами
    {
        new MathQuest{ Quest = "2x + ? = 15, где x = 6" , CorrectAnswerNumber = 3} ,

        new MathQuest{ Quest = "5y - ? = 3, где y = 2" , CorrectAnswerNumber = 7} ,

        new MathQuest{ Quest = "?z + 8 = 20, где z = 3" , CorrectAnswerNumber = 4} ,

        new MathQuest{ Quest = "1/2a - ? = 10, где a = 28" , CorrectAnswerNumber = 4} ,

        new MathQuest{ Quest = "?m + 5 = 17, где m = 4" , CorrectAnswerNumber = 3} ,

        new MathQuest{ Quest = "7n - ? = 19, где n = 3" , CorrectAnswerNumber = 2} ,

        new MathQuest{ Quest = "x'2 - ?x + 4 = 0, где x = 2" , CorrectAnswerNumber = 4} ,

        new MathQuest{ Quest = "5(p + ?) = 25, где p = 3" , CorrectAnswerNumber = 2} ,

        new MathQuest{ Quest = "2k'? - 8 = 0, где k = 2" , CorrectAnswerNumber = 2} ,

        new MathQuest{ Quest = "3(2b - 1) = ?, где b = 2" , CorrectAnswerNumber = 9}
    };




    public void StartEvent() //начало теста
    {
        if (_currentIndex == 0)
        {
            transform.gameObject.SetActive(true);
            StartCoroutine(StartEventTimer());
        }
    }
    public IEnumerator StartEventTimer() //начало игры с 2 секундной паузой
    {
        GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(false);
        GameStorage.Instance.PlayerInfoScript.PlayerUIScript.OffText();

        GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();
        yield return new WaitForSeconds(2f);

        _answering = true;
        NextQuestion(_currentIndex);
    }


    public void CheckAnswer() //проверка ответа
    {
        if (_answering && int.TryParse(_answerInputField.text.Substring(0, _answerInputField.text.Length - 1), out int answer))
        {
            if (answer == MathQuests[_currentIndex].CorrectAnswerNumber)
            {
                _answering = false;

                StopCoroutine(ResponseTime());

                _numberOfCorrectAnswers++;
                StartCoroutine(AnswerTaimer(true));
            }
        }
    }


    private IEnumerator AnswerTaimer(bool correct) //мигание цвета соотвествующе правильному/неправильному ответу
    {
        _answering = false;

        if (correct)
            _textMathematicalEquation.color = Color.green;
        else
            _textMathematicalEquation.color = Color.red;

        yield return new WaitForSeconds(1);

        _textMathematicalEquation.color = Color.white;

        _currentIndex++;
        if (_currentIndex >= MathQuests.Count)
        {
            GameStorage.Instance.GameManagerScript.MathScores = _numberOfCorrectAnswers;
            GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(true);
            GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();

            transform.gameObject.SetActive(false);
            GameStorage.Instance.GameManagerScript.ChangePuzzleCount();
            yield return null;
        }

        _answering = true;
        NextQuestion(_currentIndex);
    }


    private void NextQuestion(int index) //переключение вопросов
    {
        _timeBar.fillAmount = 1;
        StartCoroutine(ResponseTime());

        _textMathematicalEquation.text = $"Вопрос {index + 1}: {MathQuests[index].Quest}";
    }


    private IEnumerator ResponseTime() //время на решение задания
    {
        yield return new WaitForSeconds(Time.deltaTime);

        if ((_timeBar.fillAmount -= Time.deltaTime / 10) > 0 && _answering)
            StartCoroutine(ResponseTime());
        else if (_timeBar.fillAmount <= 0)
            StartCoroutine(AnswerTaimer(false));
    }
}
