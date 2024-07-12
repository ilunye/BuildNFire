using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sum_msg : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button;
    public int status = 0;  // 0: not end, 1: player 1 won, 2: player 2 won
    public TMP_Text theText = null;
    GameObject player1, player2;
    public void ToMenu(){
        Application.LoadLevel("Scenes/HomeScreen");
    }
    void Awake(){
        theText = GetComponent<TMP_Text>();
        player1 = GameObject.Find("PlayerUI_1");
        player2 = GameObject.Find("PlayerUI_2");
    }
    void Start()
    {
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player1.GetComponent<WorkFlow>().iron_number == 6 && player1.GetComponent<WorkFlow>().wood_number == 4 && player1.GetComponent<WorkFlow>().gunpowder_number == 1 && player1.GetComponent<WorkFlow>().projectile_number == 1){
            status = 1;
        }
        else if(player2.GetComponent<WorkFlow>().iron_number == 6 && player2.GetComponent<WorkFlow>().wood_number == 4 && player2.GetComponent<WorkFlow>().gunpowder_number == 1 && player2.GetComponent<WorkFlow>().projectile_number == 1){
            status = 2;
        }
        if(status == 1){
            theText.text = "PLAYER ONE WON!";
            button.SetActive(true);
        }
        else if(status == 2){
            theText.text = "PLAYER TWO WON!";
            button.SetActive(true);
        }
    }
}
