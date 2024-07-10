using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class carMove : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject[] paths;
    public int circleNum = 1;
    public int pathIdx = 0;
    private NavMeshAgent agent;
    public float timer = 0f;
    public float waitTime;
    public bool rayCastEnable = true;

    void resetRayCast(){
        rayCastEnable = true;
    }

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        waypoints = new GameObject[5];
        for(int i=0; i<5; i++){
            waypoints[i] = GameObject.Find("PsuedoGround/waypoints/waypoint"+(i+1));
        }
        circleNum = (int)Random.Range(0,3);
        paths = new GameObject[3 + circleNum*4 + 1];
        paths[0] = waypoints[0];
        paths[1] = waypoints[1];
        paths[2] = waypoints[2];
        for(int i=0; i<circleNum; i++){
            for(int j=0; j<4; j++){
                paths[i*4+j+3] = waypoints[(j+3)%4];
            }
        }
        paths[circleNum*4+3] = waypoints[4];
        agent.SetDestination(paths[pathIdx].transform.position);
    }

    void Update(){
        RaycastHit hit;
        if(rayCastEnable){
            if(Physics.Raycast(transform.position, transform.forward, out hit, 2.0f)){
                if(hit.collider.CompareTag("car")){
                    agent.isStopped = true;
                    if(hit.collider.gameObject.transform.position.x > transform.position.x){
                        hit.collider.gameObject.GetComponent<carMove>().waitTime = 1f;
                        waitTime = 2f;
                    }else{
                        hit.collider.gameObject.GetComponent<carMove>().waitTime = 2f;
                        waitTime = 1f;
                    }
                }else{
                    agent.isStopped = false;
                }
            }else{
                agent.isStopped = false;
            }
        }
        if(agent.isStopped){
            timer += Time.deltaTime;
            if(timer >= waitTime){
                rayCastEnable = false;
                agent.isStopped = false;
                timer = 0f;
                waitTime = 0f;
                Invoke("resetRayCast", 1f);
            }
        }
        if(agent.remainingDistance!=0 && agent.remainingDistance<0.5f && pathIdx<=paths.Length){
            if(pathIdx==paths.Length){
                Destroy(gameObject);
                return;
            }
            agent.isStopped = true;
            agent.ResetPath();
            agent.isStopped = false;
            agent.SetDestination(paths[pathIdx].transform.position);
            pathIdx++;
        }
    }
    /*
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
        // Debug.Log("Entered trigger with: " + other.name);
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
    */
}


