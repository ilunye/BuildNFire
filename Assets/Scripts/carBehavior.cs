using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class carBehavior : MonoBehaviour
{
    private float dirSpeed;
    Vector3 p,r;
    bool move=true;//环行路控制车流
    float rotationSpeed;
    float transformSpeed;
    float road_1or2;
    private int circleNum=2;//一共需要走的圈数
    public GameObject dest;
    void Start()
    {
        //float road_1or2=Random.Range(0,1);
        dirSpeed=0.04f;
        transformSpeed=0.02f;
        rotationSpeed=160;
        p=gameObject.transform.localPosition;
        circleNum=(int)Random.Range(0,5);
        //gameObject.transform.position=new Vector3(-23,-4.9f,-7.6f);
        GetComponent<NavMeshAgent>().SetDestination(dest.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Time.time<circleNum*15f+7.5f){
        //     circleroad();
        // }
        // else{
        //     road1();
        // }     
    }
    
    private void road1(){
        if(gameObject.transform.localPosition.x<=-12f&&
                gameObject.transform.localPosition.x>=-13f&&
                gameObject.transform.localPosition.z<=-5f&&
                gameObject.transform.localPosition.z>=-8f){
                changeRotation(0);
                changePosition(transformSpeed);
            }
            else if(gameObject.transform.localPosition.z>=-1.5f&&
                gameObject.transform.localPosition.z<=1f&&
                gameObject.transform.localPosition.x>=-14f&&
                gameObject.transform.localPosition.x<=-10f
                ){
                changeRotation(90);
                changePosition(transformSpeed);
            }
            else if(gameObject.transform.localPosition.x>=1.5f&&
                gameObject.transform.localPosition.x<=3.5f&&
                gameObject.transform.localPosition.z<=1f&&
                gameObject.transform.localPosition.z>=-3f){
                changeRotation(180);
                changePosition(transformSpeed);
            }
            else if(gameObject.transform.localPosition.z<=-7.5f&&
                gameObject.transform.localPosition.z>=-9f&&
                gameObject.transform.localPosition.x<=4f&&
                gameObject.transform.localPosition.x>=0f){
                changeRotation(90);
                changePosition(transformSpeed);
            }

            else{
                changePosition(dirSpeed);
            }
    }
    private void circleroad(){
        if(gameObject.transform.localPosition.x<=-12f&&
                gameObject.transform.localPosition.x>=-13f&&
                gameObject.transform.localPosition.z<=-5f&&
                gameObject.transform.localPosition.z>=-8f){
                changeRotation(0);
                changePosition(transformSpeed);
            }
            else if(gameObject.transform.localPosition.z>=-1.5f&&
                gameObject.transform.localPosition.z<=1f&&
                gameObject.transform.localPosition.x>=-14f&&
                gameObject.transform.localPosition.x<=-10f
                ){
                changeRotation(90);
                changePosition(transformSpeed);
            }
            else if(gameObject.transform.localPosition.x>=1.5f&&
                gameObject.transform.localPosition.x<=3.5f&&
                gameObject.transform.localPosition.z<=1f&&
                gameObject.transform.localPosition.z>=-3f){
                changeRotation(180);
                changePosition(transformSpeed);
            }
            else if(gameObject.transform.localPosition.z<=-14.5f&&
                gameObject.transform.localPosition.z>=-16f&&
                gameObject.transform.localPosition.x<=4f&&
                gameObject.transform.localPosition.x>=0f){
                changeRotation(270);
                changePosition(transformSpeed);
            }
            else if(gameObject.transform.localPosition.z<=-12f&&
                gameObject.transform.localPosition.z>=-17f&&
                gameObject.transform.localPosition.x<=-12f&&
                gameObject.transform.localPosition.x>=-13f){
                changeRotation(0);
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
