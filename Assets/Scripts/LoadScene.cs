using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LoadScene : MonoBehaviour
{
    public string scenePath;
    public void loadScene(){
        NetworkManagerHUD.disable = true;
        Application.LoadLevel(scenePath);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
