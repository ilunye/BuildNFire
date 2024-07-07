using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public bool claimed = false;
    public int state = 0;
    public bool isPlaying = false;
    private Animator animator;
    private float[] pos = new float[4] {-1.38f, -1.18f, -0.64f, -0.15f};

    private void setNotPlaying(){
        isPlaying = false;
    }

    public void prev_state(){

    }

    public void next_state(){
        if(isPlaying) return;
        if(state < 4){
            state++;
            animator.Play("cannon" + state.ToString(), -1, 0f);
            isPlaying = true;
            Invoke("setNotPlaying", 5f);
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
    }
}
