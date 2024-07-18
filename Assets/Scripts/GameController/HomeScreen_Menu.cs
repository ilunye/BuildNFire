using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class HomeScreen_Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI text;
    private Color initialColor;
    private TextMeshProUGUI confirmText;
    public GameObject cannonImage;
    void Start()
    {
        confirmText = GetComponent<TextMeshProUGUI>();
        initialColor = text.color;
        if (text.text == "START" || text.text == "vally" || text.text == "normal")
        {
            Debug.Log("normal");
             cannonImage = GameObject.Find("cannon_img1");
        }
        else if(text.text == "darkcity"){
            cannonImage = GameObject.Find("cannon_img3");
        }
        else
        {
            Debug.Log("big");
             cannonImage = GameObject.Find("cannon_img3");
        }
        Debug.Log(cannonImage);
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
        //change mode
        if (text.text == "normal")
        {
            ChooseMode.modeChoose = 0;
            SceneManager.LoadScene("Scenes/MapSelection");
        }
        else if (text.text == "crazy")
        {
            ChooseMode.modeChoose = 1;
            SceneManager.LoadScene("Scenes/MapSelection");
        }
        else if (text.text == "big")
        {
            ChooseMode.modeChoose = 2;
            SceneManager.LoadScene("Scenes/MapSelection");
        }
        // cannonImage.SetActive(false);
        if (text.text == "START")
            SceneManager.LoadScene("Scenes/ModeSelection");
        else if (text.text == "EXIT")
            Application.Quit();
        else if (text.text == "vally")
            SceneManager.LoadScene("Scenes/VallyInstruction");
        else if (text.text == "city")
            SceneManager.LoadScene("Scenes/CityInstruction");
        else if(text.text == "darkcity")
            SceneManager.LoadScene("Scenes/DarkCityInstruction");
    }
}
