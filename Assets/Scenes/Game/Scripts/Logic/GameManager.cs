using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //���������� ������� �������
    public int PuzzleCount = 2;

    [Header("������� �����")]
    public int MathScores = 0;
    public int RussianLanguageScores = 0;
    public int HistoryScores = 0;

    [Header("������� ����� ������")]
    public int SmartGuyMathScores;
    public int SmartGuyRussianLanguageScores;
    public int SmartGuyHistoryScores;

    [Header("������� �������")]
    public TheStartingMeetingEvent TheStartingMeetingEventScript;


    [Header("������� �����������")]
    public RussianPuzzle RussianPuzzleScript;
    public MathPuzzle MathPuzzleScript;
    public HistoryPuzzle HistoryPuzzleScript;


    private void Start() //��������� ��������� ������ ������
    {
        SmartGuyMathScores = Random.Range(7, 9);
        SmartGuyRussianLanguageScores = Random.Range(7, 9);
        SmartGuyHistoryScores = Random.Range(7, 9);
    }



    public enum GameEventEnum //������ ������� �������
    {
        TheStartingMeeting,

        TheRussianPuzzle,
        TheMathPuzzle,
        TheHistoryPuzzle
    }
    public GameEventEnum GameEvent;

    public void TriggerInteract(GameEventEnum gameEventEnum) //��������� ������� �� ���������
    {
        switch(gameEventEnum)
        {
            case GameEventEnum.TheStartingMeeting:
                TheStartingMeetingEventScript.StartEvent();
                return;




            case GameEventEnum.TheMathPuzzle:
                MathPuzzleScript.StartEvent();
                return;


            case GameEventEnum.TheRussianPuzzle:
                RussianPuzzleScript.StartEvent();
                return;


            case GameEventEnum.TheHistoryPuzzle:
                HistoryPuzzleScript.StartEvent();
                return;
        }
    }


    public void ChangePuzzleCount() //������� ���������� �������
    {
        PuzzleCount++;
        GameStorage.Instance.ChangeGlobalLightScript.ChangeIntesity(0.1f + 0.1f * PuzzleCount, 20);

        if (PuzzleCount == 3)
        {
            GameStorage.Instance.EndGameScript.End();
        }
    }
}