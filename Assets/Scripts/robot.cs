using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sum_msg;
    public bool enabled = false;
    public bool isPlaying = false;
    public bool lose = false;
    public bool lose_action_performing = false;
    private Animator Anim;
    private Vector3 origin_pos = new Vector3(11.91f,0.1f,6.42f);
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 绕一个点做圆周运动
        if(!lose && enabled){
            if(!isPlaying){
                Anim.Play("anim_open");
                isPlaying = true;
            }
            transform.RotateAround(new Vector3(13.69f,0f,6.42f), transform.up, 50f * Time.deltaTime);
        }else if(lose && !lose_action_performing){
            lose_action_performing = true;
            transform.position = origin_pos;
            Anim.Play("anim_close");
        }else if(lose && lose_action_performing){
            // do nothing
        }
    }
}
