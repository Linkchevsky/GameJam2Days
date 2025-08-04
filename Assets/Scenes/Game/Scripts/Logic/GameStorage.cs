using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameStorage : MonoBehaviour
{
    public static GameStorage Instance;
    private void Awake() //объ€вление синглтона
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    //объекты хранилища
    public List<IInteractiveObjects> InteractiveObjectsList = new List<IInteractiveObjects>();

    [Header("»гровые объекты")]
    public Tilemap GameTilemap;
    public ChangeGlobalLight ChangeGlobalLightScript;

    [Header("—крипты")]
    public GameManager GameManagerScript;
    public PlayerInfo PlayerInfoScript;
    public MainUI MainUIScript;
    public EndGame EndGameScript;
}
