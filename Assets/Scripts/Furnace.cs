using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    private Animator animator;
    public Material material;
    public GameObject player;
    private Transform outPos;
    private bool playerIn = false;
    private bool hasFire = false;
    void OnTriggerEnter(Collider other) {
        if(other.gameObject == player){
            playerIn = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject == player){
            playerIn = false;
        }
    }
    public void Play(){
        animator.Play("furnace");
    }
    private void Fireoff(){
        material.SetColor("_EmissionColor", Color.black);
        hasFire = false;
    }
    public void AddFire(){
        material.SetColor("_EmissionColor", Color.white);
        hasFire = true;
        Invoke("Fireoff", 15f);
    }
    private void smelting(){
        Instantiate(Resources.Load("Prefabs/ConcreteTubes"), outPos.position, Quaternion.identity);
        Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", Color.black);
        outPos = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIn && Input.GetKeyDown(KeyCode.E)){
            if(player.GetComponent<Character>().Material == Character.MaterialType.Wood){
                player.GetComponent<Character>().Material = Character.MaterialType.None;
                Play();
                AddFire();
            }else if(player.GetComponent<Character>().Material == Character.MaterialType.IronOre && hasFire){
                player.GetComponent<Character>().Material = Character.MaterialType.None;
                Play();
                Invoke("smelting", 5f);
            }
        }
    }
}
