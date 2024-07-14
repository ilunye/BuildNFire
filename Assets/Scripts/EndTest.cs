using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTest : MonoBehaviour
{
    private float explosionForce=100f;
    public float explosionRadius = 10f;
    public int status;
    Vector3 p_left=new Vector3(804.42f,0.1f,980.3f);
    Vector3 p_right=new Vector3(794.84f,0.1f,980.3f);

    // Start is called before the first frame update
    void Start()
    {
        status=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){//左侧失败
            Debug.Log("左侧");
            GameObject boom = Instantiate(Resources.Load("prefabs/explosion") as GameObject);
            boom.transform.position=p_left;
            GameObject fire = Instantiate(Resources.Load("prefabs/fire") as GameObject);
            GameObject barrel = Instantiate(Resources.Load("prefabs/broken_barrel") as GameObject);
            barrel.transform.position=p_left;
            for (int i = 0; i < 10; i++)
            {
                GameObject plank = Instantiate(Resources.Load("prefabs/broken_plank") as GameObject);
                Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                plank.transform.position = p_left+randomPosition; 
            }
            fire.transform.position=p_left;
            ExplosionLeft(explosionForce,p_left,explosionRadius);

        }
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){//右侧失败
            Debug.Log("右侧");
            GameObject boom = Instantiate(Resources.Load("prefabs/explosion") as GameObject);
            GameObject barrel = Instantiate(Resources.Load("prefabs/broken_barrel") as GameObject);
            barrel.transform.position=p_right;
            for (int i = 0; i < 10; i++)
            {
                GameObject plank = Instantiate(Resources.Load("prefabs/broken_plank") as GameObject);
                Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                plank.transform.position = p_right+randomPosition; 
            }
            boom.transform.position=p_right;
            GameObject fire = Instantiate(Resources.Load("prefabs/fire") as GameObject);
            fire.transform.position=p_right;
            ExplosionRight(explosionForce,p_right,explosionRadius);
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
            Debug.Log("为："+hit);
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, position, radius);
            }
        }
    }
}
