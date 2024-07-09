using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    private Animator animator;
    public Material material;
    public void Play(){
        animator.Play("furnace");
    }
    private void Fireoff(){
        material.SetColor("_EmissionColor", Color.black);
    }
    public void AddFire(){
        material.SetColor("_EmissionColor", Color.white);
        Invoke("Fireoff", 15f);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", Color.black);
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
