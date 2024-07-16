using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class carMove : MonoBehaviour
{
/*  public GameObject[] waypoints;
    public GameObject[] paths;
    public int circleNum = 1;
    public int pathIdx = 0;
    private NavMeshAgent agent;
*/
    public float timer = 0f;
    public float waitTime;
    public bool rayCastEnable = false;
    private bool first = true;
    public bool carCongestion = false;
    public GameObject lastHit;
    public bool green = true;
    private float initialVelocityX=1f;
    private float initialVelocityZ=-1f;
    private float throwForce=2f;

    void resetRayCast(){
        rayCastEnable = true;
    }
    void resetSpeed(){
        speed = 3.5f;
        carCongestion = false;
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            AudioSource audioSource=GetComponent<AudioSource>();
            audioSource.Play();
            other.gameObject.GetComponent<Character>().playerState = Character.PlayerState.Falling;
            other.gameObject.GetComponent<Character>().isFalling = true;
            other.gameObject.GetComponent<Animator>().Play("DAMAGED01");
            lastHit = other.gameObject;
            Rigidbody r = other.gameObject.GetComponent<Rigidbody>();
            bool chooseLeft = (int)Random.Range(0,2)==0;
            initialVelocityX = chooseLeft ? -1f : 1f;
            r.AddForce(Vector3.up*throwForce+new Vector3(initialVelocityX,0,initialVelocityZ),ForceMode.Impulse);

        }
        if(other.tag == "car"){
            lastHit = other.gameObject;
            carCongestion = true;
            speed = 0;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            Invoke("resetSpeed", 1f);
        }
        if(other.tag == "car"){
            Invoke("resetSpeed", 1f);
        }
    }
/*
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
    */
    private float lifeTime=0f;

    private float speed = 3.5f;
    void Start()
    {
        if(green){
            gameObject.transform.position=new Vector3(799.874f,0.0f,991.6682f);
        }else{
            gameObject.transform.position=new Vector3(799.32f,0.0f,991.6682f);
        }
        gameObject.transform.rotation=Quaternion.Euler(0, 180, 0);
    }

     void Update()
    {
        lifeTime+=Time.deltaTime;
        transform.position += speed *transform.forward * Time.deltaTime;
        if(transform.position.z < 972){
            Destroy(gameObject);
        }
        if(carCongestion && lastHit == null){
            speed = 3.5f;
            carCongestion = false;
        }
    }
    
}
