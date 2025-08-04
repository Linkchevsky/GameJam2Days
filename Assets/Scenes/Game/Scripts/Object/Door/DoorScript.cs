using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class DoorScript : MonoBehaviour , IInteractiveObjects //скрипт управления дверью
{
    public Transform DoorPoint; 
    public float RotationDegrees = 90f; 
    public float RotationSpeed = 50f;

    private float _startRotate;
    private float _targetAngle;
    private float _currentAngle;
    private bool _isRotating = false;
    private bool _isOpened;


    private void Start()
    {
        GameStorage.Instance.InteractiveObjectsList.Add(this);

        //установка начальных значений
        _startRotate = transform.rotation.eulerAngles.z;
        _currentAngle = Vector2.SignedAngle(Vector2.right, transform.position - DoorPoint.position);
    }


    public void Interact() //взаимодействие с дверью игроком
    {
        if (_isRotating)
            return;

        AudioManager.Instance.Play("door", 0.1f);

        if (!_isOpened)
        {
            _targetAngle = _currentAngle + RotationDegrees;
            _isOpened = true;
            _isRotating = true;
            return;
        }

        _isOpened = false;
        _targetAngle = _currentAngle - RotationDegrees;
        _isRotating = true;
    }
    public bool CanInteract() //проверка на "возможно ли взаимодействовать"
    {
        if (Vector2.Distance(transform.position, GameStorage.Instance.PlayerInfoScript.transform.position) <= 0.85f)
            return true;
        return false;
    }



    private void Update()
    {
        if (_isRotating) //поворот двери
        {
            float step = RotationSpeed * Time.deltaTime;
            _currentAngle = Mathf.MoveTowards(_currentAngle, _targetAngle, step);

            float radians = _currentAngle * Mathf.Deg2Rad;
            Vector3 newPosition = DoorPoint.position + new Vector3(Mathf.Cos(radians) * transform.localScale.y / 2, Mathf.Sin(radians) * transform.localScale.y / 2, 0);

            transform.position = newPosition;


            Vector3 direction = DoorPoint.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (Mathf.Approximately(_currentAngle, _targetAngle))
                _isRotating = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) //если игрок входит в поле двери, высвечивать текст с кнопкой для взаимодействия
    {
        if (collision.tag == "Player")
            GameStorage.Instance.PlayerInfoScript.PlayerUIScript.ShowText("'E'");
    }

    private void OnTriggerExit2D(Collider2D collision) //если игрок вышел из поля двери - убрать текст
    {
        if (collision.tag == "Player")
            GameStorage.Instance.PlayerInfoScript.PlayerUIScript.OffText();
    }
}
