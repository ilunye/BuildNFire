using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float[] pos;
    public Material[] mat;
    public int idx = 0;
    public bool isPlaying = false;

    IEnumerator EachNext(int idx)
    {
        isPlaying = true;
        for(float i=pos[2*idx]; i<pos[2*idx+1]; i+=0.01f)
        {
            mat[idx].SetFloat("_DisappearOffset", i);
            yield return new WaitForSeconds(0.1f);
        }
        isPlaying = false;
    }

    IEnumerator EachPrev(int idx)
    {
        isPlaying = true;
        for(float i=pos[2*idx+1]; i>pos[2*idx]; i-=0.01f)
        {
            mat[idx].SetFloat("_DisappearOffset", i);
            yield return new WaitForSeconds(0.1f);
        }
        isPlaying = false;
    }

    public void Next()
    {
        if(isPlaying) return;
        if(idx < 5)
        {
            StartCoroutine(EachNext(idx));
            idx++;
        }
    }

    public void Prev()
    {
        if(idx > 0)
        {
            StopAllCoroutines();
            isPlaying = false;
            idx--;
            mat[idx].SetFloat("_DisappearOffset", pos[2*idx]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pos = new float[10]; 
        for(int i=0; i<10; i++)
        {
            pos[i] = transform.GetChild(i).position.y;
        }
        for(int i=0; i<mat.Length; i++)
        {
            mat[i].SetFloat("_DisappearOffset", pos[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<10; i++)
        {
            pos[i] = transform.GetChild(i).position.y;
        }
    }
}
