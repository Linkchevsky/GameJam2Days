using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //контроллер игровых события
    public int PuzzleCount = 2;

    [Header("Игровые баллы")]
    public int MathScores = 0;
    public int RussianLanguageScores = 0;
    public int HistoryScores = 0;

    [Header("Игровые баллы умника")]
    public int SmartGuyMathScores;
    public int SmartGuyRussianLanguageScores;
    public int SmartGuyHistoryScores;

    [Header("Скрипты событий")]
    public TheStartingMeetingEvent TheStartingMeetingEventScript;


    [Header("Скрипты головоломок")]
    public RussianPuzzle RussianPuzzleScript;
    public MathPuzzle MathPuzzleScript;
    public HistoryPuzzle HistoryPuzzleScript;


    private void Start() //установка случайных баллов умника
    {
        SmartGuyMathScores = Random.Range(7, 9);
        SmartGuyRussianLanguageScores = Random.Range(7, 9);
        SmartGuyHistoryScores = Random.Range(7, 9);
    }



    public enum GameEventEnum //список игровых событий
    {
        TheStartingMeeting,

        TheRussianPuzzle,
        TheMathPuzzle,
        TheHistoryPuzzle
    }
    public GameEventEnum GameEvent;

    public void TriggerInteract(GameEventEnum gameEventEnum) //обработка событий из триггеров
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


    public void ChangePuzzleCount() //подсчёт пройденных квестов
    {
        PuzzleCount++;
        GameStorage.Instance.ChangeGlobalLightScript.ChangeIntesity(0.1f + 0.1f * PuzzleCount, 20);

        if (PuzzleCount == 3)
        {
            GameStorage.Instance.EndGameScript.End();
        }
    }
}