using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTest : MonoBehaviour
{
    private float explosionForce=10f;
    public float explosionRadius = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.B)){//左侧失败
            Debug.Log("左侧");
            GameObject boom = Instantiate(Resources.Load("prefabs/Explosion single fallback") as GameObject);
            boom.transform.position=new Vector3(794.84f,0f,980.3f);
            GameObject fire = Instantiate(Resources.Load("prefabs/Fire fuel sharp") as GameObject);
            fire.transform.position=new Vector3(794.84f,0f,980.3f);
            ExplosionLeft(explosionForce,new Vector3(794.84f,0f,980.3f),explosionRadius);

        }
        if(Input.GetKey(KeyCode.N)){//右侧失败
            Debug.Log("右侧");
            GameObject boom = Instantiate(Resources.Load("prefabs/Explosion single fallback") as GameObject);
            boom.transform.position=new Vector3(804.42f,0f,980.3f);
            GameObject fire = Instantiate(Resources.Load("prefabs/Fire fuel sharp") as GameObject);
            fire.transform.position=new Vector3(804.42f,0f,980.3f);
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
