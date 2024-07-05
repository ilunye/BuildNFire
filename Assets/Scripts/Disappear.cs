using UnityEngine;
using UnityEngine.UI;

public class Disappear : MonoBehaviour
{
    public GameObject cannon;
    public Material[] mat;
    public Slider slider;

    void Start()
    {
        slider.onValueChanged.AddListener(value=>SliderValue(value));
    }

    private void SliderValue(float value)
    {
        for(int i=0; i<mat.Length; i++)
        {
            mat[i].SetFloat("_DisappearOffset", value);
        }
    }
}
