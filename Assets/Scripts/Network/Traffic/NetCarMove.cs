using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class NetCarMove : NetworkBehaviour
{
    public bool carCongestion = false;
    public GameObject lastHit;
    public bool green = true;
    private float initialVelocityX = 1f;
    private float initialVelocityZ = -1f;
    private float throwForce = 2f;

    public Vector3 car1_position = new Vector3(799.874f, 0.0f, 991.6682f);
    public Vector3 car2_position = new Vector3(799.32f, 0.0f, 991.6682f);

    [Command(requiresAuthority = false)]
    public void CmdDestroy(GameObject obj)
    {
        NetworkServer.Destroy(obj);
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
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            other.gameObject.GetComponent<NetCharacter>().CmdPlayerState(NetCharacter.PlayerState.Falling);
            other.gameObject.GetComponent<NetCharacter>().CmdSetFalling(true);
            other.gameObject.GetComponent<NetCharacter>().CmdPlay("DAMAGED01");
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

    private float lifeTime = 0f;

    private float speed = 3.5f;
    void Start()
    {
        if (green)
        {
            gameObject.transform.position = car1_position;
        }
        else
        {
            gameObject.transform.position = car2_position;
        }
        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        transform.position += speed * transform.forward * Time.deltaTime;
        if (lifeTime > 5f)
        {
            CmdDestroy(gameObject);
        }
        if (carCongestion && lastHit == null)
        {
            speed = 3.5f;
            carCongestion = false;
        }
    }

}
