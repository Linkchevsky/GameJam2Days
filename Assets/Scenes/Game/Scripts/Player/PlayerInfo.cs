using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public SpriteRenderer PlayerSpriteRenderer;

    [Space] //������ �������� �� ������ ��� ������������ �������������� (Messaging System)
    public PlayerMovement PlayerMovementScript;
    public PlayerClicked PlayerClickedScript;
    public PlayerUI PlayerUIScript;
    public PlayerCamera PlayerCameraScript;

    private void Start()
    {
        GameStorage.Instance.PlayerInfoScript = this; //���������� ������ � ��������� (���������)
    }
}
