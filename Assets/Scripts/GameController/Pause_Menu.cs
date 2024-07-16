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
    private bool confirmButtonEnabled = false;  //15秒后才能按confirm
    private TextMeshProUGUI confirmText;
    public bool isInst = false;
    private GameObject Controller;

    void Start()
    {
        confirmText = GetComponent<TextMeshProUGUI>();
        Controller = GameObject.Find("Canvas/Pause_Controller");
        initialColor = text.color;
        currentIndex = 0;
        if(isInst)
            ShowCurrentImage();
    }

    void Update()
    {
        if(isInst)
            confirmText.text = "CONFIRM";
        if (!confirmButtonEnabled)
        {
            confirmButtonEnabled = true;
        }
        // if (Input.GetKeyUp(KeyCode.Escape))
        // {
        //     Application.Quit();
        // }
    }
    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % 3;
        ShowCurrentImage();
    }
    private void ShowCurrentImage()
    {
        // 隐藏所有图片
        foreach (Image image in images)
        {
            image.gameObject.SetActive(false);
        }

        // 显示当前索引的图片
        images[currentIndex].gameObject.SetActive(true);
    }

    public void LastImage()
    {
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        ShowCurrentImage();
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
        else if (text.text == "CONFIRM"&&confirmButtonEnabled) //15秒后可点击
        {
            //Debug.Log("confirm");
            SceneManager.LoadScene("Scenes/Main");
        }
        else if (text.text == "instruction")
        {
            Time.timeScale=0;
        }
        else if (text.text=="continue"){
            Time.timeScale=1;
        }

        else if (text.text == "EXIT")
        {
            Application.Quit();
        }
        else if (text.text == "next")
        {
            //Debug.Log("next");
            NextImage();
        }
        else if (text.text == "last")
        {
            //Debug.Log("last");
            LastImage();
        }
    }
}
