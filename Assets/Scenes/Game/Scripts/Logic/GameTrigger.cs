using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class GameTrigger : MonoBehaviour , IInteractiveObjects
{
    private void Start()
    {
        if (!IsTrigger)
            GameStorage.Instance.InteractiveObjectsList.Add(this);
    }


    public bool IsTrigger = false;
    public float InteractDistance;
    [SerializeField] private GameEventEnum gameEventEnum; //ивент триггера

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTrigger)
        {
            GameStorage.Instance.GameManagerScript.TriggerInteract(gameEventEnum);
            return;
        }

        GameStorage.Instance.PlayerInfoScript.PlayerUIScript.ShowText("'E'");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsTrigger)
            GameStorage.Instance.PlayerInfoScript.PlayerUIScript.OffText();
    }


    public void Interact() => GameStorage.Instance.GameManagerScript.TriggerInteract(gameEventEnum);


    public bool CanInteract()
    {
        if (Vector2.Distance(transform.position, GameStorage.Instance.PlayerInfoScript.transform.position) <= InteractDistance)
            return true;
        return false;
    }
}
