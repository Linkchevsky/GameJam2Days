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
    private List<string> _dialogueList = new List<string>() //������ � �������
    {
        "����������.",
        "������?",
        "���� �� ����� ���� ���������?",
        "�����, � ���?",
        "�� ������ �� ������ ��� �� ������� �������� �����,�� ���� �� ��������� ��� ������� ������� ��� ����� ����������",
        "������ �� ����������",
        "�� ������ ���  �� ������ ���� ����������????!!!",
        "������ �� ���� ������������� � ����..",
        "������� ������������",
        "���? ������ � ����� ���...",
        "�� ���������,  �����",
        "....",
        "��� ���, ���  � ����� ������� �� ���� ����� ������ ������, ��� � �������",
        "����� ��������, � �����",
        "������ ������,�������� ����� �������, ������� ���� �� ����������� �� ������ ��������� � ���� ���������� ������",
        "���...."
    };

    public void StartEvent() //������ �������
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
            if (Input.GetKeyDown(KeyCode.Space)) //��� ������� �� ������ - ��������� �����
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
