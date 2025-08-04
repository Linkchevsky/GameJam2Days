using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameStorage : MonoBehaviour
{
    public static GameStorage Instance;
    private void Awake() //���������� ���������
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    //������� ���������
    public List<IInteractiveObjects> InteractiveObjectsList = new List<IInteractiveObjects>();

    [Header("������� �������")]
    public Tilemap GameTilemap;
    public ChangeGlobalLight ChangeGlobalLightScript;

    [Header("�������")]
    public GameManager GameManagerScript;
    public PlayerInfo PlayerInfoScript;
    public MainUI MainUIScript;
    public EndGame EndGameScript;
}
