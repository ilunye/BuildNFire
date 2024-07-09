using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
// using UnityEditor.Profiling;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject RawImage;    
    void Start()
    {
        RawImage = GameObject.Find("RawImage");
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Character>().Material == Character.MaterialType.Wood)
            RawImage.SetActive(true);
        else
            RawImage.SetActive(false);
    }
}
