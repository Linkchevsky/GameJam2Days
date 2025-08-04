using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampScript : MonoBehaviour
{
    public Light2D LampLight;


    public bool IsGarland;
    public List<Color> colors; // ������ ������ ��� �����
    public float duration = 2f; // ����� ����� �����

    private void Start()
    {
        if (IsGarland) //���� ������� ����� �������� - ���������
            StartCoroutine(ChangeColors());
    }

    private IEnumerator ChangeColors() //�������� ����� ����� ��� ������ ��������
    {
        int currentColorIndex = 0;

        while (true)
        {
            Color startColor = colors[currentColorIndex];
            Color endColor = colors[(currentColorIndex + 1) % colors.Count];

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                LampLight.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // ���� ���������� �����
            }

            endColor.a = 1;
            LampLight.color = endColor;
            currentColorIndex = (currentColorIndex + 1) % colors.Count;
        }
    }
}
