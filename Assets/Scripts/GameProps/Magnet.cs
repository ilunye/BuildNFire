using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private float magnetRadius = 5f;
    private GameObject otherPlayer;
    void Start()
    {
        StartCoroutine(DestroyAfterDelay(10f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void DealWithMagnet(GameObject gameObject1)
    {

        Collider[] colliders = Physics.OverlapSphere(gameObject1.transform.position, magnetRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (nearbyObject.tag == "Wood" || nearbyObject.tag == "Stone" || nearbyObject.tag == "Iron") //attract other player
                {
                    otherPlayer = nearbyObject.gameObject;
                    StartCoroutine(Attract_Other());
                }
            }
        }
    }





    private IEnumerator Attract_Other()
    {


        float speed = 1.0f;
        float t = 0f;
        Vector3 startposition = otherPlayer.transform.position;
        Vector3 targetposition = gameObject.transform.position;
        while (otherPlayer != null && otherPlayer.transform.position != targetposition)
        {
            t += Time.deltaTime * speed;
            otherPlayer.transform.position = Vector3.Lerp(startposition, targetposition, t);
            yield return null; // stop IEnumerator
            

        }
    }
}
