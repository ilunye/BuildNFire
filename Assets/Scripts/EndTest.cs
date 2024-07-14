using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTest : MonoBehaviour
{
    private float explosionForce=10f;
    public float explosionRadius = 10f;
    public int status;

    // Start is called before the first frame update
    void Start()
    {
        status=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(status==1){//左侧失败
            /*Debug.Log("左侧");
            GameObject boom = Instantiate(Resources.Load("prefabs/explosion") as GameObject);
            boom.transform.position=new Vector3(794.84f,0f,980.3f);
            GameObject fire = Instantiate(Resources.Load("prefabs/fire") as GameObject);
            fire.transform.position=new Vector3(794.84f,0f,980.3f);*/
            ExplosionLeft(explosionForce,new Vector3(794.84f,0f,980.3f),explosionRadius);

        }
        if(status==2){//右侧失败
            /*Debug.Log("右侧");
            GameObject boom = Instantiate(Resources.Load("prefabs/explosion") as GameObject);
            boom.transform.position=new Vector3(804.42f,0f,980.3f);
            GameObject fire = Instantiate(Resources.Load("prefabs/fire") as GameObject);
            fire.transform.position=new Vector3(804.42f,0f,980.3f);*/
            ExplosionRight(explosionForce,new Vector3(804.42f,0f,980.3f),explosionRadius);
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
}
