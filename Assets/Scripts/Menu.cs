using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI text;
    private Color initialColor;

    void Start()
    {
        initialColor = text.color;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = initialColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (text.text == "START")
        {
            SceneManager.LoadScene("Scenes/pre");
        }
        else if (text.text == "Confirm")
        {
            SceneManager.LoadScene("Scenes/Main");
        }
        else if (text.text == "EXIT")
        {
            Application.Quit();
        }
    }
}
