using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class exit : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public  TextMeshProUGUI text;
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
            SceneManager.LoadScene("Scenes/HomeScreen");

        }
    }
}
