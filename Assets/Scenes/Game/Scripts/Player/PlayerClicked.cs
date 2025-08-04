using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicked : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfoScript;

    private void Update()
    {
        if (!_playerInfoScript.PlayerMovementScript.CanMove)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (IInteractiveObjects objectInterface in GameStorage.Instance.InteractiveObjectsList)
            {
                if (objectInterface.CanInteract())
                {
                    objectInterface.Interact();
                    return;
                }    
            }
        }
    }
}