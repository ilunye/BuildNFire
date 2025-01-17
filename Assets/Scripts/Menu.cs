using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI text;
    public Image[] images;   // 存储Image组件的数组
    private int currentIndex;
    private Color initialColor;
    private bool confirmButtonEnabled = false;  //5秒后才能按confirm
    private float timer = 5f;
    private TextMeshProUGUI confirmText;
    public bool isConfirm = false;
    private static bool isFirstTime = true;
    private string sceneName;
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        confirmText = GetComponent<TextMeshProUGUI>();
        initialColor = text.color;
        currentIndex = 0;
        ShowCurrentImage();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (isConfirm)
        {
            if (timer <= 0 || isFirstTime == false)
            {
                confirmText.text = "CONFIRM";
            }
            else
            {
                confirmText.text = "CONFIRM(" + (int)timer + ")";
            }
        }
        if ((!confirmButtonEnabled && timer <= 0) || isFirstTime == false)
        {
            confirmButtonEnabled = true;
            isFirstTime = false;
        }
        // if (Input.GetKeyUp(KeyCode.Escape))
        // {
        //     Application.Quit();
        // }
    }
    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % 5;
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
        if (!isConfirm || (isConfirm && timer <= 0) || isFirstTime == false)
        {
            text.color = Color.red;
        }
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
        else if (text.text == "CONFIRM" && confirmButtonEnabled&&sceneName.CompareTo("VallyInstruction")==0) 
        {
            //Debug.Log("confirm");
            SceneManager.LoadScene("Scenes/Main");
        }
        else if (text.text == "CONFIRM" && confirmButtonEnabled&&sceneName.CompareTo("CityInstruction")==0) 
        {
            //Debug.Log("confirm");
            SceneManager.LoadScene("Scenes/City");
        }
        else if (text.text == "CONFIRM" && confirmButtonEnabled&&sceneName.CompareTo("DarkCityInstruction")==0) 
        {
            //Debug.Log("confirm");
            SceneManager.LoadScene("Scenes/DarkCity");
        }
        else if (text.text == "instruction")
        {
            Time.timeScale = 0;
        }
        else if (text.text == "continue")
        {
            Time.timeScale = 1;
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
