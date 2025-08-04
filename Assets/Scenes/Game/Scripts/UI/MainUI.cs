using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("Диалоговое")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;

    public void ShowDialogueText(string text) //включение и изменение текста в диалоге с умником
    {
        DialogueText.text = text;
        DialoguePanel.SetActive(true);
    }

    public void OffDialogueText() => DialoguePanel.SetActive(false); //выключение текста для диалога



    [Header("Объекты Canvas")]
    public SpriteRenderer DarkeningImage;



    [Header("Головоломки")]
    public GameObject HistoryPuzzle;
    public GameObject MathPuzzle;

    public void ChangeDarkeningTheScreen() //затемнить или осветлить экран игрока в зависимости от текущего состояния
    {
        if (DarkeningImage.color.a == 0)
            StartCoroutine(TaimerDarkeningTheScreen(Time.deltaTime));
        else
            StartCoroutine(TaimerLighteningTheScreen(Time.deltaTime));
    }

    public IEnumerator TaimerDarkeningTheScreen(float time) //затемнение экрана
    {
        yield return new WaitForSeconds(time);
        
        DarkeningImage.color = new Color(0, 0, 0, DarkeningImage.color.a + Time.deltaTime);
        if (DarkeningImage.color.a < 1)
            StartCoroutine(TaimerDarkeningTheScreen(Time.deltaTime));
        else
            DarkeningImage.color = new Color(0, 0, 0, 1);
    }

    public IEnumerator TaimerLighteningTheScreen(float time) //осветление экрана
    { 
        yield return new WaitForSeconds(time);
        
        DarkeningImage.color = new Color(0, 0, 0, DarkeningImage.color.a - Time.deltaTime);
        if (DarkeningImage.color.a > 0)
            StartCoroutine(TaimerLighteningTheScreen(Time.deltaTime));
        else
            DarkeningImage.color = new Color(0, 0, 0, 0);
    }
}
