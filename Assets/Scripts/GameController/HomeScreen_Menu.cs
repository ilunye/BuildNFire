using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Mirror;

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
        if (text.text == "START" || text.text == "valley" || text.text == "normal")
        {

            cannonImage = GameObject.Find("cannon_img1");
        }
        else if (text.text == "EXIT" || text.text == "city" || text.text == "crazy")
        {

            cannonImage = GameObject.Find("cannon_img2");
        }
        else if (text.text == "online"){
            cannonImage = GameObject.Find("cannon_img4");
        }
        else
        {

            cannonImage = GameObject.Find("cannon_img3");
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
        else if (text.text == "START")
            SceneManager.LoadScene("Scenes/ModeSelection");
        else if (text.text == "EXIT")
            Application.Quit();
        else if (text.text == "valley")
        {
            SceneController.sceneID = 0;
            SceneManager.LoadScene("Scenes/VallyInstruction");
        }

        else if (text.text == "city")
        {
            SceneController.sceneID = 1;
            SceneManager.LoadScene("Scenes/CityInstruction");
        }
        else if (text.text == "darkcity")
        {
            SceneController.sceneID = 2;
            SceneManager.LoadScene("Scenes/darkcityInstruction");
        }else if (text.text == "online")
        {
            NetworkManagerHUD.disable = false;
            SceneManager.LoadScene("Scenes/Online/OnlineMain");
        }


    }
}
