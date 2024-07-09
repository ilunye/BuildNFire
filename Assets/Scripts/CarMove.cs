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
    bool move=true;//环行路控制车流
    float rotationSpeed;
    float transformSpeed;
    private int circleNum=2;//一共需要走的圈数
    void Start()
    {
        
        dirSpeed=0.02f;
        transformSpeed=0.02f;
        rotationSpeed=160;
        p=gameObject.transform.localPosition;
        circleNum=(int)Random.Range(0,3);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time<circleNum*15f+7.5f){
           circleroad();
        }
        else{
           road1();
        }   
    }
    
    private void road1()
{
    Collider myCollider = gameObject.GetComponent<Collider>();
    Collider c1 = r1.GetComponent<Collider>();
    Collider c2 = r2.GetComponent<Collider>();
    Collider c3 = r3.GetComponent<Collider>();
    Collider c4 = r4.GetComponent<Collider>();

    if (myCollider.bounds.Intersects(c1.bounds))
    {
        changeRotation(270);
        changePosition(transformSpeed);
        Debug.Log("碰撞 r1");
    }
    else if (myCollider.bounds.Intersects(c2.bounds))
    {
        changeRotation(0);
        changePosition(transformSpeed);
        Debug.Log("碰撞 r2");
    }
    else if (myCollider.bounds.Intersects(c3.bounds))
    {
        changeRotation(90);
        changePosition(transformSpeed);
        Debug.Log("碰撞 r3");
    }
    else if (myCollider.bounds.Intersects(c4.bounds))
    {
        changeRotation(0);
        changePosition(transformSpeed);
        Debug.Log("碰撞 r4");
    }
    else
    {
        changePosition(dirSpeed);
    }
}


    private void circleroad(){
        Collider myCollider = gameObject.GetComponent<Collider>();
        Collider c1 = r1.GetComponent<Collider>();
        Collider c2 = r2.GetComponent<Collider>();
        Collider c3 = r3.GetComponent<Collider>();
        Collider c5 = r5.GetComponent<Collider>();
        Collider c6 = r6.GetComponent<Collider>();
        if(myCollider.bounds.Intersects(c1.bounds)){
                changeRotation(270);
                changePosition(transformSpeed);
            }
            else if(myCollider.bounds.Intersects(c2.bounds)
                ){
                changeRotation(0);
                changePosition(transformSpeed);
            }
            else if(myCollider.bounds.Intersects(c3.bounds)){
                changeRotation(90);
                changePosition(transformSpeed);
            }
            else if(myCollider.bounds.Intersects(c5.bounds)){
                changeRotation(180);
                changePosition(transformSpeed);
            }
            else if(myCollider.bounds.Intersects(c6.bounds)){
                changeRotation(270);
                changePosition(transformSpeed);
            }

            else{
                changePosition(dirSpeed);
            }
    }
    
    private void changePosition(float speed) {
        transform.position += speed*transform.forward;
    }
    private void changeRotation(float targetAngle) {
        Quaternion currentRotation = gameObject.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        float angleDiff = Quaternion.Angle(currentRotation, targetRotation);
        if (angleDiff > 0.01f) { 
            Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            gameObject.transform.rotation=newRotation;
            
        }
    }      
}

