using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetPlayer : NetworkBehaviour
{
    private GameObject myCannon;
    [Command]
    private void CmdNextState(){
        myCannon.GetComponent<Cannon>().next_state();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<4; i++){
            myCannon = GameObject.Find("cannon" + i.ToString());
            if(myCannon.GetComponent<Cannon>().claimed == false){
                myCannon.GetComponent<Cannon>().claimed = true;
                break;
            }
        } 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer) return;
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime, 0, Input.GetAxis("Vertical") * Time.deltaTime));
        if(Input.GetKeyDown(KeyCode.E)){
            CmdNextState();
        }
    }
}
