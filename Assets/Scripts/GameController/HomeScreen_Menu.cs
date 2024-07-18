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
        if(text.text == "START" || text.text == "vally")
        {
            cannonImage = GameObject.Find("cannon_img1");
        }
        else
        {
            cannonImage = GameObject.Find("cannon_img2");
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
            SceneManager.LoadScene("Scenes/MapSelection");
        else if (text.text == "EXIT")
            Application.Quit();
        else if(text.text == "vally")
            SceneManager.LoadScene("Scenes/VallyInstruction");
        else if(text.text == "city")
            SceneManager.LoadScene("Scenes/CityInstruction");
    }
}
