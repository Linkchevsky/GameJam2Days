using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfoScript;

    [SerializeField] private Camera _gameCamera;
    public Camera GameCamera => _gameCamera;

    public bool Enable = false;

    public bool IsPlayerCamera = true; //следует ли камера за игроком или за указанной точкой

    public Vector3 CameraTarget; //цель камеры
    public float smoothing = 0.1f; // Параметр сглаживания камеры для

    private Vector3 _tmpPosition;

    private void LateUpdate()
    {
        if (Enable)
        {
            if (IsPlayerCamera)
            {
                CameraTarget = transform.position;
                _tmpPosition = CameraTarget;
            }
            else
            {
                if (Vector2.Distance(_gameCamera.transform.position, CameraTarget) > 0.01f)
                    _tmpPosition += ((CameraTarget - _gameCamera.transform.position).normalized) * 10 * Time.deltaTime;
            }


            Vector3 targetPosition = new Vector3(_tmpPosition.x, _tmpPosition.y, -10);
            _gameCamera.transform.position = targetPosition;
        }
    }


    public void ChangeCameraTarget(Vector3 target) //установка уели для камеры
    {
        IsPlayerCamera = false;
        CameraTarget = target;
    }

    public void ChangeToDefaultCameraTarget() => IsPlayerCamera = true; //установка игрока как цели для камеры
}
