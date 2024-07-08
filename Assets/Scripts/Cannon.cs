using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Cannon : MonoBehaviour
{
    public bool claimed = false;
    public bool isPlaying = false;
    private Material[] material = new Material[5];
    private Transform disOffset;
    private Transform down;
    private Transform up;

    IEnumerator each_next(float offset){
        isPlaying = true;
        float eachOffset = offset / 50;
        for(int i=0; i<50; i++){
            disOffset.position = new Vector3(disOffset.position.x, disOffset.position.y + eachOffset, disOffset.position.z);
            yield return new WaitForSeconds(0.1f);
        }
        isPlaying = false;
    }

    public void next_state(){
        if(isPlaying) return;
        StartCoroutine(each_next((up.position.y - down.position.y)/10));
    }

    void Start()
    {
        disOffset = gameObject.transform.GetChild(0);
        down = gameObject.transform.GetChild(1);
        up = gameObject.transform.GetChild(2);
        for(int i=0; i<5; i++){
            material[i] = GetComponent<MeshRenderer>().materials[i];
            material[i].SetFloat("_DisappearOffset", disOffset.position.y);
        }
    }

    void Update()
    {
        for(int i=0; i<5; i++){
            material[i].SetFloat("_DisappearOffset", disOffset.position.y);
        }
    }
}
