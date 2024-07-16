using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Instrucion_Menu: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI text;
    public Image[] images;   // 存储Image组件的数组
    private int currentIndex; 
    private Color initialColor;
    private TextMeshProUGUI confirmText;
    private GameObject Instruction;

    void Start()
    {
        Instruction = GameObject.Find("Canvas/PauseMenu/Instruction");
        confirmText = GetComponent<TextMeshProUGUI>();
        initialColor = text.color;
        currentIndex = 0;
        if(text.text != "CONFIRM"){
            ShowCurrentImage();
        }
    }

    void Update()
    {
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
        if (text.text == "CONFIRM")
        {
            Instruction.SetActive(false);
        }
        else if (text.text == "next")
        {
            NextImage();
        }
        else if (text.text=="last"){
            LastImage();
        }
    }
}
