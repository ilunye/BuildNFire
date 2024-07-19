using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
// using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RawImage;    
    public Sprite[] sprites = new Sprite[6];
    void Start()
    {
        if(sprites[0] == null){
            sprites[0] = Resources.Load<Sprite>("Textures/wood");
            sprites[1] = Resources.Load<Sprite>("Textures/ironOre");
            sprites[2] = Resources.Load<Sprite>("Textures/iron");
            sprites[3] = Resources.Load<Sprite>("Textures/gunPowder");
            sprites[4] = Resources.Load<Sprite>("Textures/cannonBall");
            sprites[5] = Resources.Load<Sprite>("Textures/bomb");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(RawImage == null) return;
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
