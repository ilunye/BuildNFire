using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkAnimationPlayer : NetworkBehaviour
{
    [Command(requiresAuthority = false)]
    public void Play(string stateName)
    {
        GetComponent<Animator>().Play(stateName);
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
