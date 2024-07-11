using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    // Start is called before the first frame update
    private int status;

    private bool shaking = false;

    private float shake_duration = 1.0f;
    private float shake_magnitude = 1.0f;

    // private Animator Anim;

    void Start()
    {
        status = 4;
        // Anim = GetComponent<Animator>();
    }

 
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if(shaking)
            return;
        Debug.Log("collide!");
        if(other.tag == "Player" && other.GetComponent<Character>().isPunch == false){
            Debug.Log("Here am I");
            Debug.Log("status: " + other.GetComponent<Character>().playerState);
            if(other.GetComponent<Character>().playerState == Character.PlayerState.Punch){
                // Anim.Play("tree_shake");
                Shake();
                other.GetComponent<Character>().isPunch = true;
                Debug.Log("status--");
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
        shaking = false;
        if(status == 0){
            GameObject g = Instantiate(Resources.Load("Prefabs/Wood") as GameObject);
            g.transform.position = transform.position;
            if(transform.position.x < 800)
                GameObject.Find("GameController").GetComponent<TreeController>().TreeNumberLeft--;
            else
                GameObject.Find("GameController").GetComponent<TreeController>().TreeNumberRight--;
            Destroy(gameObject);
        }
    }

    void Shake()
    {
        if (!shaking)
        {
            shaking = true;
            status--;
            StartCoroutine(ShakeTreeCoroutine(shake_duration, shake_magnitude, status == 0));
        }
    }
    
}
