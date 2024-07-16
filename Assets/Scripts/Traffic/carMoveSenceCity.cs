using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class carMoveSenceCity : MonoBehaviour
{

    public bool rayCastEnable = false;
    public bool carCongestion = false;
    public GameObject lastHit;
    public bool truck = true;
    private float initialVelocityX = 1f;
    private float initialVelocityZ = -1f;
    private float throwForce = 2f;
    public float speed = 10f;

    private Vector3 car1_position = new Vector3(120f, 0f, 157f);
    private Vector3 car2_position = new Vector3(120f, 0f, 163f);

    void resetRayCast()
    {
        rayCastEnable = true;
    }
    void resetSpeed()
    {
        speed = 3.5f;
        carCongestion = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("chuangren");
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            other.gameObject.GetComponent<Character>().playerState = Character.PlayerState.Falling;
            other.gameObject.GetComponent<Character>().isFalling = true;
            other.gameObject.GetComponent<Animator>().Play("DAMAGED01");
            lastHit = other.gameObject;
            Rigidbody r = other.gameObject.GetComponent<Rigidbody>();
            bool chooseLeft = (int)Random.Range(0, 2) == 0;
            initialVelocityX = chooseLeft ? -1f : 1f;
            r.AddForce(Vector3.up * throwForce + new Vector3(initialVelocityX, 0, initialVelocityZ), ForceMode.Impulse);

        }
        if (other.tag == "car")
        {
            lastHit = other.gameObject;
            carCongestion = true;
            speed = 0;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("resetSpeed", 1f);
        }
        if (other.tag == "car")
        {
            Invoke("resetSpeed", 1f);
        }
    }




    void Start()
    {
        if (truck)
        {
            gameObject.transform.position = car1_position;
        }
        else
        {
            gameObject.transform.position = car2_position;
        }
    }

    void Update()
    {

        transform.position += speed * transform.forward * Time.deltaTime;
        if (transform.position.x > 240)
        {
            Destroy(gameObject);
        }
        if (carCongestion && lastHit == null)
        {
            speed = 3.5f;
            carCongestion = false;
        }
    }

}
