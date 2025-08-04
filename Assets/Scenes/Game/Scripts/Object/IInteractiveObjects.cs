using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractiveObjects //интерфейс для объектов с взаимодействием
{
    public void Interact();

    public bool CanInteract();
}
