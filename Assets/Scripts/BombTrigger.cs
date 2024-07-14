using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    public float explosionForce=200f;
    public float explosionRadius = 20f;
    public int status;
    Vector3 p_left=new Vector3(804.42f,0.1f,980.3f);
    Vector3 p_right=new Vector3(794.84f,0.1f,980.3f);

    // objects to add collider & rigid body
    private GameObject[] cannons = new GameObject[2];
    private GameObject[] furnaces = new GameObject[2];
    
    // Start is called before the first frame update
    void Start()
    {
        status=0;
        cannons[0] = GameObject.Find("cannon_1");
        cannons[1] = GameObject.Find("cannon_2");
        furnaces[0] = GameObject.Find("Furnace_1");
        furnaces[1] = GameObject.Find("Furnace_2");
    }

    public void rightBomb(){
        furnaces[1].AddComponent<Rigidbody>();
        cannons[1].GetComponent<MeshRenderer>().enabled = false;
        ropeAddColliderNRigidBody(cannons[1].transform.Find("ropes").gameObject);
        GameObject boom = Instantiate(Resources.Load("prefabs/explosion") as GameObject);
        boom.transform.position=p_left;
        GameObject fire = Instantiate(Resources.Load("prefabs/fire") as GameObject);
        fire.transform.position=p_left;
        GameObject barrel = Instantiate(Resources.Load("prefabs/broken_barrel") as GameObject);
        barrel.transform.position=p_left;
        GameObject brokenCannon = Instantiate(Resources.Load("prefabs/broken_cannon") as GameObject);
        brokenCannon.transform.position=p_left;
        for (int i = 0; i < 10; i++)
        {
            GameObject plank = Instantiate(Resources.Load("prefabs/broken_plank") as GameObject);
            Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            plank.transform.position = p_left+randomPosition; 
        }
        ExplosionRight(explosionForce,p_left,explosionRadius);
    }

    public void leftBomb(){
        furnaces[0].AddComponent<Rigidbody>();
        cannons[0].GetComponent<MeshRenderer>().enabled = false;
        ropeAddColliderNRigidBody(cannons[0].transform.Find("ropes").gameObject);
        GameObject boom = Instantiate(Resources.Load("prefabs/explosion") as GameObject);
        boom.transform.position=p_right;
        GameObject barrel = Instantiate(Resources.Load("prefabs/broken_barrel") as GameObject);
        barrel.transform.position=p_right;
        GameObject brokenCannon = Instantiate(Resources.Load("prefabs/broken_cannon") as GameObject);
        brokenCannon.transform.position=p_right;
        for (int i = 0; i < 10; i++)
        {
            GameObject plank = Instantiate(Resources.Load("prefabs/broken_plank") as GameObject);
            Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            plank.transform.position = p_right+randomPosition; 
        }
        GameObject fire = Instantiate(Resources.Load("prefabs/fire") as GameObject);
        fire.transform.position=p_right;
        ExplosionLeft(explosionForce,p_right,explosionRadius);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void ExplosionLeft(float force, Vector3 position, float radius){
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, position, radius);
            }
        }
    }
    private void ExplosionRight(float force, Vector3 position, float radius){
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, position, radius);
            }
        }
    }
    private void ropeAddColliderNRigidBody(GameObject ropesRoot){
        foreach(Transform child in ropesRoot.transform){
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.AddComponent<BoxCollider>();
        }
    }
}
