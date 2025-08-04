using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfoScript;


    private bool _canMove = true;
    public bool CanMove => _canMove;

    [Space]
    public float MoveSpeed = 2f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _playerCollider;

    private Vector2 _moveInput;

    private void Update() //установка направления движения путём отслеживания нажатия игрока и запуск анимации ходьбы
    {
        if (_canMove)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
            
            _moveInput.Normalize();
        }

        if (_moveInput != Vector2.zero)
            AllEvent.s_playerWalked?.Invoke(1);
        else
            AllEvent.s_playerWalked?.Invoke(0);
    }

    private void FixedUpdate() //просчёт движения каждый раз как обновляется физика unity
    {
        if (_moveInput != Vector2.zero)
        {
            _rb.MovePosition(_rb.position + _moveInput * MoveSpeed * Time.fixedDeltaTime);
            _rb.rotation = Mathf.Atan2(_moveInput.y, _moveInput.x) * Mathf.Rad2Deg + 90;
        }
    }


    public void ChangeCanMove(bool status) //изменение возможности игрока двигаться
    {
        _moveInput = Vector2.zero;
        _canMove = status;
    }
}
