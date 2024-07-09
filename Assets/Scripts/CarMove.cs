using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMove : MonoBehaviour
{
    public GameObject r1;
    public GameObject r2;
    public GameObject r3;
    public GameObject r4;
    public GameObject r5;
    public GameObject r6;
    private float dirSpeed;
    Vector3 p,r;
    public float destroyDelay = 45f;
    float rotationSpeed;
    float transformSpeed;
    private bool isCircle;
    private int circleNum=1;//一共需要走的圈数
    void Start()
    {
        
        dirSpeed=0.08f;
        transformSpeed=0.05f;
        rotationSpeed=20f;
        p=gameObject.transform.localPosition;
        circleNum=(int)Random.Range(0,3);
        r1 = GameObject.Find("Road_Object/r1");
        r2 = GameObject.Find("Road_Object/r2");
        r3 = GameObject.Find("Road_Object/r3");
        r4 = GameObject.Find("Road_Object/r4");
        r5 = GameObject.Find("Road_Object/r5");
        r6 = GameObject.Find("Road_Object/r6");
        int a=Random.Range(0,2);
        if(a==0){
            isCircle=true;
        }
        else{
            isCircle=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        changePosition(dirSpeed);
        StartCoroutine(DestroyAfterDelay()); 
    }
    
    private void changePosition(float speed) {
        transform.localPosition -= speed*transform.right;
    }
    private void changeRotation(Vector3 targetEulerAngles) {
        Quaternion currentRotation = gameObject.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        Quaternion newRotation = Quaternion.LerpUnclamped(currentRotation, targetRotation, 0.1f);
        gameObject.transform.rotation = newRotation;
}

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Entered trigger with: " + other.name);
        if(other.CompareTag("car")){
            dirSpeed=0.0f;
        }


        if(isCircle){
            //Debug.Log("isCircle==true");
            if(other.CompareTag("r1")){
                changeRotation(new Vector3(0,270,0));
                changePosition(transformSpeed);
            }
            else if(other.CompareTag("r2")){
                changeRotation(new Vector3(0,0,0));
                changePosition(transformSpeed);
            }
            else if(other.CompareTag("r3")){
                changeRotation(new Vector3(0,90,0));
                changePosition(transformSpeed);
            }
            else if(other.CompareTag("r5")){
                changeRotation(new Vector3(0,180,0));
                //isCircle=false;
                changePosition(transformSpeed);
            }
            else if(other.CompareTag("r6")){
                changeRotation(new Vector3(0,270,0));
                Debug.Log("isCircle==false");
                changePosition(transformSpeed);
            }
        }
        if(!isCircle){
            if(other.CompareTag("r1")){
                changeRotation(new Vector3(0,270,0));
                changePosition(transformSpeed);
            }
            else if(other.CompareTag("r2")){
                changeRotation(new Vector3(0,0,0));
                changePosition(transformSpeed);
            }
            else if(other.CompareTag("r3")){
                changeRotation(new Vector3(0,90,0));
                changePosition(transformSpeed);
            }
            else if(other.CompareTag("r4")){
                changeRotation(new Vector3(0,0,0));
                changePosition(transformSpeed);
            }
            else if(other.CompareTag("r6")){
                changeRotation(new Vector3(0,270,0));
                Debug.Log("isCircle==false");
                changePosition(transformSpeed);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit trigger with: " + other.name);
        if(other.CompareTag("car")){
            dirSpeed=0.07f;
        }
        else if(other.CompareTag("r7")){
            isCircle=false;
        }
        else if(other.CompareTag("r8")){
            dirSpeed=0.4f;
        }
    } 

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

}


