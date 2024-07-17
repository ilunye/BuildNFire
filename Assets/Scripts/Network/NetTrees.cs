using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetTrees : NetworkBehaviour
{
    // Start is called before the first frame update
    [SyncVar]
    public int status;

    public bool shaking = false;
    public bool first = true;

    private float shake_duration = 1.0f;
    private float shake_magnitude = 1.0f;
    public GameObject voice;
    private AudioSource tree_voice;
    public float changeScale = 1f;

    // private Animator Anim;
    [Command(requiresAuthority = false)]
    public void CmdSetTreeStatus(int s){
        status = s;
    }

    void Start()
    {
        voice = Instantiate(Resources.Load("Audio/Tree_hit") as GameObject);
        tree_voice = voice.GetComponent<AudioSource>();
        CmdSetTreeStatus(4);
    }

 
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if(!isServer)
            return;
        if(shaking)
            return;
        if(other.tag == "Player"){
            if(other.GetComponent<NetCharacter>().playerState == NetCharacter.PlayerState.Punch && first){
                shaking = true;
                first = false;
                CmdSetTreeStatus(status - 1);
                tree_voice.Play();
                StartCoroutine(ShakeTreeCoroutine(shake_duration, shake_magnitude, status == 0));
            }else if(other.GetComponent<NetCharacter>().playerState == NetCharacter.PlayerState.Idle){
                first = true;
            }
        }
    }
    
    IEnumerator ShakeTreeCoroutine(float duration = 1f, float magnitude = 1f, bool broken = false)
    {
        Vector3 originalRotation = transform.localEulerAngles;
        float elapsed = 0.0f;
        while(elapsed < duration * 0.7f){
            elapsed += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;  
        }

        while (elapsed < duration)
        {
            if(broken)
                duration *= 0.8f;
            float z = Mathf.Sin(elapsed * 20) * magnitude; // 20 and 0.1 are arbitrary values for frequency and magnitude
            transform.localEulerAngles = new Vector3(originalRotation.x, originalRotation.y, originalRotation.z + z);

            elapsed += Time.deltaTime;
            yield return null; // Wait until next frame
        }

        transform.localEulerAngles = originalRotation; // Reset to original rotation
        if(status == 0){
            if(isServer){
                GameObject g = Instantiate(Resources.Load("Prefabs/Online/Wood") as GameObject);
                g.transform.position = transform.position;
                g.transform.localScale *= changeScale;
                NetworkServer.Spawn(g);
            }
            if(transform.position.x < 800)
                GameObject.Find("GameController").GetComponent<NetTreeController>().TreeNumberLeft--;
            else
                GameObject.Find("GameController").GetComponent<NetTreeController>().TreeNumberRight--;
            if(isServer)
                NetworkServer.Destroy(gameObject);
        }
        shaking = false;
    }

    void Shake()
    {
        if (!shaking)
        {
            shaking = true;
            CmdSetTreeStatus(status - 1);
            // 播放声音
            tree_voice.Play();
            StartCoroutine(ShakeTreeCoroutine(shake_duration, shake_magnitude, status == 0));
        }
    }
    
}
