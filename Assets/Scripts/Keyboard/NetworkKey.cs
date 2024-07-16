using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkKey : NetworkBehaviour
{
    public bool wasd = true;
    private VirtualKey virtualKey;
    // Start is called before the first frame update
    void Start()
    {
        virtualKey = GetComponent<VirtualKey>();     
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer) return;
        if(wasd){
            if(Input.GetKeyDown(KeyCode.W)){
                StartCoroutine(virtualKey.pressKey(0));
            }
            if(Input.GetKeyUp(KeyCode.W)){
                StartCoroutine(virtualKey.releaseKey(0));
            }
            if(Input.GetKeyDown(KeyCode.S)){
                StartCoroutine(virtualKey.pressKey(1));
            }
            if(Input.GetKeyUp(KeyCode.S)){
                StartCoroutine(virtualKey.releaseKey(1));
            }
            if(Input.GetKeyDown(KeyCode.A)){
                StartCoroutine(virtualKey.pressKey(2));
            }
            if(Input.GetKeyUp(KeyCode.A)){
                StartCoroutine(virtualKey.releaseKey(2));
            }
            if(Input.GetKeyDown(KeyCode.D)){
                StartCoroutine(virtualKey.pressKey(3));
            }
            if(Input.GetKeyUp(KeyCode.D)){
                StartCoroutine(virtualKey.releaseKey(3));
            }
            if(Input.GetKeyDown(KeyCode.E)){
                StartCoroutine(virtualKey.pressKey(4));
            }
            if(Input.GetKeyUp(KeyCode.E)){
                StartCoroutine(virtualKey.releaseKey(4));
            }
        }else{
            // Arrow keys and enter(return)
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                StartCoroutine(virtualKey.pressKey(0));
            }
            if(Input.GetKeyUp(KeyCode.UpArrow)){
                StartCoroutine(virtualKey.releaseKey(0));
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                StartCoroutine(virtualKey.pressKey(1));
            }
            if(Input.GetKeyUp(KeyCode.DownArrow)){
                StartCoroutine(virtualKey.releaseKey(1));
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                StartCoroutine(virtualKey.pressKey(2));
            }
            if(Input.GetKeyUp(KeyCode.LeftArrow)){
                StartCoroutine(virtualKey.releaseKey(2));
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                StartCoroutine(virtualKey.pressKey(3));
            }
            if(Input.GetKeyUp(KeyCode.RightArrow)){
                StartCoroutine(virtualKey.releaseKey(3));
            }
            if(Input.GetKeyDown(KeyCode.Return)){
                StartCoroutine(virtualKey.pressKey(4));
            }
            if(Input.GetKeyUp(KeyCode.Return)){
                StartCoroutine(virtualKey.releaseKey(4));
            }
        }
    }
}
