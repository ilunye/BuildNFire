using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageBlink : MonoBehaviour
{
    public float blinkDuration = 7f;     // 闪烁时间
    private Image image;
    private Color originalColor;
    private float timer = 0f;
    private float alpha = 0f;            
    private bool isBlinking = false;
    private bool getDark = false;        // 最开始变暗

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // Start fully transparent
        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        yield return new WaitForSeconds(4f); // 等待4秒

        isBlinking = true;
        while (isBlinking)
        {
            Blink();
            yield return null;
        }
    }

    private void Blink()
    {
        if (isBlinking)
        {
            timer += Time.deltaTime;
            if(alpha<=1&&!getDark){   //变亮
                alpha+=0.02f;
                image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                if(alpha>=0.9f){
                    getDark=true;
                }
            }
            else if(alpha>=0&&getDark){   //变暗
                alpha-=0.02f;
                image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                if(alpha<=0.1f){
                    getDark=false;
                }
            }
            if (timer >= blinkDuration)
            {
                StopBlink();
            }
        }
    }

    void StopBlink()
    {
        isBlinking = false;
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); 
        timer = 0f;
        alpha = 0f;
        getDark = false;
    }
}

