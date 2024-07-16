using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Misc;
using Unity.VisualScripting;
using UnityEngine;

public class CarThrow : MonoBehaviour
{
    public float interval;
    public Vector3 spawnPosition;
    public int p;
    public float timer;
    private float throwForce;
    private float destroyDelay;
    private int bomb_num = 0;
    private int burger_num = 0;
    private int hourglass_num = 0;
    private int concrete_num = 0;
    private int projectile_num = 0;
    private int rock_num = 0;
    private int wood_num = 0;
    private int magnet_num = 0;
    private int barrel_num = 0;
    private int lock_num = 0;
    private int box_num = 0;

    // Since each scene has different time, we need to set the time for each scene
    private float overallTime = 0f;
    public int Scene_id = 0;       // main scene by default

    public float CarTime = 4.5f;
    public float bigScale = 1f;



    void Start()
    {
        float distanceAbove = 2f;
        destroyDelay = 15f;
        spawnPosition = gameObject.transform.position + transform.up * distanceAbove;
        interval = 1f;
        p = (int)Random.Range(1, 2);
        interval = interval * p;
        throwForce = 1;
        // destroyDelay = 5f;
    }

    void Update()
    {
        overallTime += Time.deltaTime;
        if (overallTime > CarTime)
            Destroy(gameObject);

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            GameObject c;
            int r = Random.Range(0, 21);//决定抛出物体
            if (r == 0)
            {
                c = Instantiate(Resources.Load("prefabs/Bomb Red") as GameObject);
                c.name = "bomb_" + "truck_" + bomb_num.ToString();
                bomb_num++;
                //c = Instantiate(Resources.Load("prefabs/explosiveBarrel") as GameObject);
            }
            else if (r == 1)
            {
                c = Instantiate(Resources.Load("prefabs/burger_1_lod0") as GameObject);
                c.name = "burger_" + "truck_" + burger_num.ToString();
                burger_num++;
                //c = Instantiate(Resources.Load("prefabs/Rock_03") as GameObject);
            }
            else if (r == 2)
            {
                c = Instantiate(Resources.Load("prefabs/Hourglass Green 1") as GameObject);
                c.name = "hourglass_" + "truck_" + hourglass_num.ToString();
                hourglass_num++;
                //c = Instantiate(Resources.Load("prefabs/projectile") as GameObject);
            }
            else if (r == 4 || r == 3)
            {
                c = Instantiate(Resources.Load("prefabs/ConcreteTubes") as GameObject);
                c.name = "concrete_" + "truck_" + concrete_num.ToString();
                concrete_num++;
            }
            else if (r == 5 || r == 6)
            {
                c = Instantiate(Resources.Load("prefabs/projectile") as GameObject);
                c.name = "projectile_" + "truck_" + projectile_num.ToString();
                projectile_num++;
            }
            else if (r == 7 || r == 8 || r == 9 || r == 10 || r == 11)
            {
                c = Instantiate(Resources.Load("prefabs/Rock_03") as GameObject);
                c.name = "rock_" + "truck_" + rock_num.ToString();
                rock_num++;
            }
            else if (r == 15 || r == 12 || r == 13 || r == 14)
            {
                c = Instantiate(Resources.Load("prefabs/Wood") as GameObject);
                c.name = "wood_" + "truck_" + wood_num.ToString();
                wood_num++;
            }
            else if (r == 16)
            {
                c = Instantiate(Resources.Load("prefabs/Magnet") as GameObject);   //drop off magnet
                c.name = "magnet_" + "truck_" + magnet_num.ToString();
                magnet_num++;
                c = Instantiate(Resources.Load("prefabs/Rock_03") as GameObject);
                c.name = "rock_" + "truck_" + rock_num.ToString();
                rock_num++;
            }
            else if (r == 17 || r == 18)
            {
                c = Instantiate(Resources.Load("prefabs/explosiveBarrel") as GameObject);
                c.name = "barrel_" + "truck_" + barrel_num.ToString();
                barrel_num++;
            }
            else if (r == 21)
            {
                c = Instantiate(Resources.Load("Prefabs/Box") as GameObject);
                c.name = "box_" + "truck_" + box_num.ToString();
                box_num++;
            }
            else
            {
                c = Instantiate(Resources.Load("prefabs/Lock Silver") as GameObject);
                c.name = "lock_" + "truck_" + lock_num.ToString();
                lock_num++;
                //c = Instantiate(Resources.Load("prefabs/Rock_03") as GameObject);
            }
            spawnPosition = transform.position + transform.up * 0.5f - transform.forward * 0.5f;
            c.transform.position = spawnPosition;
            c.transform.localScale *= bigScale;
            // Rigidbody cubeRigidbody = c.AddComponent<Rigidbody>();

            float x = Random.Range(-2f, 2f);
            float z = Random.Range(-2f, 2f);
            // set as kinetic
            Rigidbody cubeRigidbody = c.GetComponent<Rigidbody>();
            cubeRigidbody.AddForce(Vector3.up * throwForce + new Vector3(x, 0, z), ForceMode.Impulse);
            if (r == 0 || r == 1)
                StartCoroutine(BlinkAndDestroy(c, 10f));
            else
                StartCoroutine(BlinkAndDestroy(c, destroyDelay));


            timer = 0f;
            interval = 1.5f * Random.Range(1, 3);
        }
    }

    IEnumerator BlinkAndDestroy(GameObject obj, float destroyDelay)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(obj);
    }
}
