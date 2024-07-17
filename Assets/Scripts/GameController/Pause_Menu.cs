using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Pause_Menu: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI text;
    public Image[] images;   // 存储Image组件的数组
    private int currentIndex; 
    private Color initialColor;
    private TextMeshProUGUI confirmText;
    private GameObject Controller;
    private GameObject Instruction;

    void Start()
    {
        confirmText = GetComponent<TextMeshProUGUI>();
        Controller = GameObject.Find("Canvas/Pause_Controller");
        Instruction = GameObject.Find("Canvas/PauseMenu/Instruction");
        initialColor = text.color;
        currentIndex = 0;
        Instruction.SetActive(false);
    }

    void Update()
    {
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = initialColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (text.text == "resume")
        {
            Controller.GetComponent<PauseController>().OnResume();
        }
        else if (text.text == "instruction")
        {
            Instruction.SetActive(true);
        }
        else if (text.text=="main menu"){
            SceneManager.LoadScene("Scenes/HomeScreen");
        }

    }
}
