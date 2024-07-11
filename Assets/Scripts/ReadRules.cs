using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadRules : MonoBehaviour
{
    
    public GameObject imageObject; // 图片对象
    public GameObject textObject; // 要显示的文字对象

    void Start()
    {
        // 初始化，显示图片和按钮，隐藏文字
        imageObject.SetActive(true);
        textObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        // 当按钮被点击时，隐藏图片和按钮，显示文字
        imageObject.SetActive(false);
        textObject.SetActive(true);
    }
}
