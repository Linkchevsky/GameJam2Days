using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("����������")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;

    public void ShowDialogueText(string text) //��������� � ��������� ������ � ������� � �������
    {
        DialogueText.text = text;
        DialoguePanel.SetActive(true);
    }

    public void OffDialogueText() => DialoguePanel.SetActive(false); //���������� ������ ��� �������



    [Header("������� Canvas")]
    public SpriteRenderer DarkeningImage;



    [Header("�����������")]
    public GameObject HistoryPuzzle;
    public GameObject MathPuzzle;

    public void ChangeDarkeningTheScreen() //��������� ��� ��������� ����� ������ � ����������� �� �������� ���������
    {
        if (DarkeningImage.color.a == 0)
            StartCoroutine(TaimerDarkeningTheScreen(Time.deltaTime));
        else
            StartCoroutine(TaimerLighteningTheScreen(Time.deltaTime));
    }

    public IEnumerator TaimerDarkeningTheScreen(float time) //���������� ������
    {
        yield return new WaitForSeconds(time);
        
        DarkeningImage.color = new Color(0, 0, 0, DarkeningImage.color.a + Time.deltaTime);
        if (DarkeningImage.color.a < 1)
            StartCoroutine(TaimerDarkeningTheScreen(Time.deltaTime));
        else
            DarkeningImage.color = new Color(0, 0, 0, 1);
    }

    public IEnumerator TaimerLighteningTheScreen(float time) //���������� ������
    { 
        yield return new WaitForSeconds(time);
        
        DarkeningImage.color = new Color(0, 0, 0, DarkeningImage.color.a - Time.deltaTime);
        if (DarkeningImage.color.a > 0)
            StartCoroutine(TaimerLighteningTheScreen(Time.deltaTime));
        else
            DarkeningImage.color = new Color(0, 0, 0, 0);
    }
}
