using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
// using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RawImage;    
    public Sprite[] sprites = new Sprite[5];
    void Start()
    {
        if(RawImage == null){
            RawImage = GameObject.Find("Canvas/PlayerUI_1/Toggle1/Background/RawImage");
        }
        Debug.Assert(RawImage != null, "RawImage is null");
    }

    // Update is called once per frame
    void Update()
    {
        switch(GetComponent<Character>().Material){
            case Character.MaterialType.Wood:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[0];
                break;
            case Character.MaterialType.IronOre:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[1];
                break;
            case Character.MaterialType.Iron:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[2];
                break;
            case Character.MaterialType.GunPowder:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[3];
                break;
            case Character.MaterialType.CannonBall:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[4];
                break;
            case Character.MaterialType.Bomb:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[5];
                break;
            default:
                RawImage.SetActive(false);
                break;
        }
    }
}
