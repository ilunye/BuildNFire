using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    public float explosionForce = 250f;
    public float explosionRadius = 25f;
    public int status;
    public Vector3 p_left;
    public Vector3 p_right;
    public int sceneID = 1;
    // objects to add collider & rigid body
    private GameObject[] cannons = new GameObject[2];
    private GameObject[] furnaces = new GameObject[2];
    public GameObject player_one;
    public GameObject player_two;
    public sum_msg sum_Msg;

    // Start is called before the first frame update
    void Start()
    {

        switch (sceneID)
        {


            case 1:
                p_left = new Vector3(804.42f, 0.1f, 980.3f);
                p_right = new Vector3(794.84f, 0.1f, 980.3f);
                break;

            case 2:
                p_right = new Vector3(161f, 3.3f, 140f);
                p_left = new Vector3(197f, 3.43f, 139f);

                break;
            default:

                break;
        }
        player_one = GameObject.Find("animal_people_wolf_1");
        player_two = GameObject.Find("animal_people_wolf_2");

        status = 0;
        cannons[0] = GameObject.Find("cannon_1");
        cannons[1] = GameObject.Find("cannon_2");
        furnaces[0] = GameObject.Find("Furnace_1");
        furnaces[1] = GameObject.Find("Furnace_2");
        
    }

    public void rightBomb()
    {
        furnaces[1].SetActive(false);
        cannons[1].GetComponent<MeshRenderer>().enabled = false;
        ropeAddColliderNRigidBody(cannons[1].transform.Find("ropes").gameObject);
        GameObject boom = Instantiate(Resources.Load("prefabs/explosion") as GameObject);
        boom.transform.position = p_left;
        GameObject fire = Instantiate(Resources.Load("prefabs/fire") as GameObject);
        fire.transform.position = p_left;
        GameObject barrel = Instantiate(Resources.Load("prefabs/broken_barrel") as GameObject);
        barrel.transform.position = p_left;
        if (cannons[1].GetComponent<Cannon>().idx == 10)
        {
            GameObject brokenCannon = Instantiate(Resources.Load("prefabs/broken_cannon") as GameObject);
            brokenCannon.transform.position = p_left;
        }
        for (int i = 0; i < 10; i++)
        {
            GameObject plank = Instantiate(Resources.Load("prefabs/broken_plank") as GameObject);
            Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            plank.transform.position = p_left + randomPosition;
        }
        ExplosionRight(explosionForce, p_left, explosionRadius);
        Destroy(sum_Msg.explode_bomb);
    }

    public void leftBomb()
    {
        furnaces[0].SetActive(false);
        cannons[0].GetComponent<MeshRenderer>().enabled = false;
        ropeAddColliderNRigidBody(cannons[0].transform.Find("ropes").gameObject);
        GameObject boom = Instantiate(Resources.Load("prefabs/explosion") as GameObject);
        boom.transform.position = p_right;
        GameObject barrel = Instantiate(Resources.Load("prefabs/broken_barrel") as GameObject);
        barrel.transform.position = p_right;
        if (cannons[0].GetComponent<Cannon>().idx == 10)
        {
            GameObject brokenCannon = Instantiate(Resources.Load("prefabs/broken_cannon") as GameObject);
            brokenCannon.transform.position = p_right;
        }
        for (int i = 0; i < 10; i++)
        {
            GameObject plank = Instantiate(Resources.Load("prefabs/broken_plank") as GameObject);
            Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            plank.transform.position = p_right + randomPosition;
        }
        GameObject fire = Instantiate(Resources.Load("prefabs/fire") as GameObject);
        fire.transform.position = p_right;
        ExplosionLeft(explosionForce, p_right, explosionRadius);
        Destroy(sum_Msg.explode_bomb);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void ExplosionLeft(float force, Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, position, radius);
            }
        }
        Destroy(player_one);
        GameObject player = Instantiate(Resources.Load("Prefabs/animal_people_wolf_1") as GameObject);
        player.GetComponent<Character>().wasd = true;
        player.GetComponent<Character>().enabled = false;
        player.transform.localPosition = new Vector3(794.2f, 1.6f, 981f);
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        playerRigidbody.AddForce(Vector3.up * 10 + new Vector3(1, 0, 1), ForceMode.Impulse);
    }
    private void ExplosionRight(float force, Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, position, radius);
            }
        }
        Destroy(player_two);
        GameObject player = Instantiate(Resources.Load("Prefabs/animal_people_wolf_2") as GameObject);
        player.GetComponent<Character>().wasd = false;
        player.GetComponent<Character>().enabled = false;
        player.transform.localPosition = new Vector3(804.8f, 1.6f, 981f);
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        playerRigidbody.AddForce(Vector3.up * 10 + new Vector3(-1, 0, 1), ForceMode.Impulse);
    }
    private void ropeAddColliderNRigidBody(GameObject ropesRoot)
    {
        foreach (Transform child in ropesRoot.transform)
        {
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.AddComponent<BoxCollider>();
        }
    }
}
