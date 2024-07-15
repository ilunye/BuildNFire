using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{

    public GameObject explosionEffect;
    void Start()
    {
        Invoke("DelayedDestroy", 2f);
    }



    private void DelayedDestroy()
    {
        Destroy(explosionEffect);
    }


}