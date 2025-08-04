using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStarted : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject StartCanvas;
    public bool IsStart;

    public GameObject Comics1;
    public GameObject Comics2;

    private int _index;
    private void Start()
    {
        GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(false);
    }


    public void StartComics() //запуск комиксов при нажатии на кнопку
    {
        StartUI.SetActive(false);
        StartCoroutine(StartComicsTimer());
    }

    private IEnumerator StartComicsTimer()
    {
        GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();
        yield return new WaitForSeconds(2f);
        IsStart = true;
        Comics1.SetActive(true);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsStart) //при нажатии на пробел пролистывать страницу
        {
            switch (_index)
            {
                case 0:
                    Comics1.SetActive(false);
                    Comics2.SetActive(true);
                    break;

                case 1:
                    StartGame();
                    break;
            }

            _index++;
        }
    }


    public void StartGame() //начало игры
    {
        GameStorage.Instance.MainUIScript.ChangeDarkeningTheScreen();
        IsStart = false;
        StartCanvas.SetActive(false);
        GameStorage.Instance.PlayerInfoScript.PlayerCameraScript.Enable = true;
        GameStorage.Instance.PlayerInfoScript.PlayerMovementScript.ChangeCanMove(true);

        BackgroundMusic.Instance.Play("Background", 0.01f);
    }
}
