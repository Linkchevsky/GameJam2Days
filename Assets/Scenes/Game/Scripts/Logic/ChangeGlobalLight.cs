using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangeGlobalLight : MonoBehaviour
{
    [SerializeField] private Light2D _globalLight2D;


    private void Start()
    {
        GameStorage.Instance.ChangeGlobalLightScript = this;
    }

    private bool _changeIntensity = false;
    private bool _positive;

    private float _intensityLvl;
    private float _speedChange;

    private void Update()
    {
        //изменение глобального освещения
        if (_changeIntensity)
        {
            if (!_positive)
            {
                if ((_globalLight2D.intensity += _speedChange * Time.deltaTime) <= _intensityLvl)
                {
                    _globalLight2D.intensity = _intensityLvl;
                    _changeIntensity = false;
                }
            }
            else
            {
                if ((_globalLight2D.intensity += _speedChange * Time.deltaTime) >= _intensityLvl)
                {
                    _globalLight2D.intensity = _intensityLvl;
                    _changeIntensity = false;
                }
            }
        }
    }


    public void ChangeIntesity(float intesityLvl, float speed) //включение изменения глобального освещения с установкой указанных значений
    {
        _intensityLvl = intesityLvl;

        if (intesityLvl >= _globalLight2D.intensity)
        {
            _positive = true;
            _speedChange = speed;
        }
        else
        {
            _positive = false;
            _speedChange = -speed;
        }

        _changeIntensity = true;
    }



    public void ChangeGlobalLightColor(Color color) => _globalLight2D.color = color; //изменение цвета глобального освещения
}
