using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RussianPuzzle : MonoBehaviour
{
    [SerializeField] private Image _timeBar;
    [SerializeField] private TextMeshProUGUI _answerInputField;
    [SerializeField] private TextMeshProUGUI _findedWordsText;

    public int _numberOfCorrectAnswers = 0;

    private List<string> _russianWords = new List<string>() //������ ���������� ����
    {
        "��",
        "���",
        "���",
        "���",
        "���",
        "���",
        "����",
        "����",
        "�����",
        "�����",
        "�����",
        "������",
        "������"
    };
    private List<string> _findedRussianWords = new List<string>(); //������ ��������� ������� ����



    public void StartEvent() //������ ����� �� ��������
    {
        if (_findedRussianWords.Count == 0)
        {
            transform.gameObject.SetActive(true);
            StartCoroutine(StartEventTimer());
        }
    }
    public IEnumerator StartEventTimer() //���������� ������
    {
        GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(false);
        GameStorage.Instance.PlayerInfoScript.PlayerUIScript.OffText();

        GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();
        yield return new WaitForSeconds(2f);

        StartCoroutine(ResponseTime());
    }


    public void CheckAnswer() //�������� ������
    {
        if (_answerInputField.text.Length > 1)
        {
            string tmpWord = _answerInputField.text.Substring(0, _answerInputField.text.Length - 1);
            tmpWord = char.ToUpper(tmpWord[0]) + tmpWord.Substring(1);

            if (_russianWords.Contains(tmpWord) && !_findedRussianWords.Contains(tmpWord))
            {
                _findedRussianWords.Add(tmpWord);
                _findedWordsText.text += $"\n{tmpWord}";

                StartCoroutine(AnswerTaimer(true));
                return;
            }

            if (gameObject.activeSelf)
                StartCoroutine(AnswerTaimer(false));
        }
    }


    private IEnumerator AnswerTaimer(bool correct) //������� ����� � ������� � ������������ � ����������� ������
    {
        if (correct)
            _answerInputField.color = Color.green;
        else
            _answerInputField.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        _answerInputField.color = Color.white;

        if (_findedRussianWords.Count == 10) //���� 10 ���� ������� - ��������� ���� �� ��������� �������
        {
            EndEvents();
            yield return null;
        }
    }


    private IEnumerator ResponseTime() //��������� 90 ������ �� ������
    {
        yield return new WaitForSeconds(Time.deltaTime);

        if ((_timeBar.fillAmount -= Time.deltaTime / 100) > 0)
            StartCoroutine(ResponseTime());
        else if (_timeBar.fillAmount <= 0)
            EndEvents();
    }


    private void EndEvents() //����� �����
    {
        StopAllCoroutines();
        GameStorage.Instance.GameManagerScript.RussianLanguageScores = _findedRussianWords.Count;

        GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(true);
        GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();

        transform.gameObject.SetActive(false);
        GameStorage.Instance.GameManagerScript.ChangePuzzleCount();
    }
}
