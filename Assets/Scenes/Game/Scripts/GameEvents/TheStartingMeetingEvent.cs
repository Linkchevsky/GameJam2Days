using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TheStartingMeetingEvent : MonoBehaviour
{
    public Transform SmartGuy;

    public bool EventEnabled = false;
    private int _dialogueIndex = 0;
    private List<string> _dialogueList = new List<string>() //диалог с умником
    {
        "Здравствуй.",
        "Привет?",
        "Вижу ты решил сюда поступить?",
        "Верно, а что?",
        "Та просто не уверен что ты осилишь обучение здесь,ты явно не выглядишь как человек знающий что такое гепатинуза",
        "Вообще то Гипотенуза",
        "ТЫ уверен что  ТЫ можешь меня поправлять????!!!",
        "Слушай не хочу конфликтовать в перв..",
        "Устроим соревнование",
        "Что? Слушай я лучше пой...",
        "НЕ ПЕРЕБИВАЙ,  ИДИОТ",
        "....",
        "Так вот, кто  в сумме наберет на всех парах лучшие оценки, тот и выиграл",
        "Ладно засранец, я готов",
        "Хорошо хорошо,подведем итоги вечером, надеюсь твой не заостренный ум сможет сравнится с моим величайшим гением",
        "Чел...."
    };

    public void StartEvent() //начало события
    {
        if (_dialogueIndex == 0)
        {
            GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(false);

            GameStorage.Instance.MainUIScript.ShowDialogueText(_dialogueList[_dialogueIndex]);
            _dialogueIndex++;

            GameStorage.Instance.PlayerInfoScript.PlayerCameraScript.ChangeCameraTarget(SmartGuy.transform.position);

            EventEnabled = true;
        }
    }



    private void Update()
    {
        if (EventEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //при нажатии на пробел - следующая фраза
            {
                if (_dialogueIndex % 2 == 0)
                    GameStorage.Instance.PlayerInfoScript.PlayerCameraScript.ChangeCameraTarget(SmartGuy.transform.position);
                else
                    GameStorage.Instance.PlayerInfoScript.PlayerCameraScript.ChangeCameraTarget(GameStorage.Instance.PlayerInfoScript.transform.position);


                GameStorage.Instance.MainUIScript.ShowDialogueText(_dialogueList[_dialogueIndex]);

                if ((_dialogueIndex += 1) >= _dialogueList.Count)
                {
                    EventEnabled = false;
                    GameStorage.Instance.PlayerInfoScript.PlayerCameraScript.ChangeToDefaultCameraTarget();
                    GameStorage.Instance.MainUIScript.OffDialogueText();
                    GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(true);
                }
            }
        }
    }
}
