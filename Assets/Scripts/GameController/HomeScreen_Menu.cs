using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class HomeScreen_Menu: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI text;
    private int currentIndex; 
    private Color initialColor;
    private TextMeshProUGUI confirmText;
    private GameObject cannonImage;
    void Start()
    {
        confirmText = GetComponent<TextMeshProUGUI>();
        initialColor = text.color;
        if(text.text == "START")
        {
            cannonImage = GameObject.Find("Canvas/start/cannon_img1");
        }
        else
        {
            cannonImage = GameObject.Find("Canvas/exit/cannon_img2");
        }
        cannonImage.SetActive(false);
    }

    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.red;
        cannonImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = initialColor;
        cannonImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // cannonImage.SetActive(false);
        if (text.text == "START")
        {
            SceneManager.LoadScene("Scenes/pre");
        }
        else if (text.text == "EXIT")
        {
            Application.Quit();
        }
    }
}
