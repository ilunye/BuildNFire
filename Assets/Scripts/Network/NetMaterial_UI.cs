using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
// using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetUIController : NetworkBehaviour
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
        switch(GetComponent<NetCharacter>().Material){
            case NetCharacter.MaterialType.Wood:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[0];
                break;
            case NetCharacter.MaterialType.IronOre:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[1];
                break;
            case NetCharacter.MaterialType.Iron:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[2];
                break;
            case NetCharacter.MaterialType.GunPowder:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[3];
                break;
            case NetCharacter.MaterialType.CannonBall:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[4];
                break;
            case NetCharacter.MaterialType.Bomb:
                RawImage.SetActive(true);
                RawImage.GetComponent<Image>().sprite = sprites[5];
                break;
            default:
                RawImage.SetActive(false);
                break;
        }
    }
}