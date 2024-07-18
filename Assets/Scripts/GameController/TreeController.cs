using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreeController : MonoBehaviour
{
    // Start is called before the first frame update
    public int TreeNumberLeft = 0;
    public int TreeNumberRight = 0;
    public float regionleft_xmin = 794f;
    public float regionleft_xmax = 798f;
    public float regionright_xmin = 800.5f;
    public float regionright_xmax = 804f;
    public float region_zmin = 983f;
    public float region_zmax = 986f;

    public String loadTrack = "Prefabs/Tree_1_1";
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Main")
        {
            regionleft_xmin = 794f;
            regionleft_xmax = 798f;
            regionright_xmin = 800.5f;
            regionright_xmax = 804f;
            region_zmin = 983f;
            region_zmax = 986f;
            // 在场景1中执行特定的逻辑
            //Debug.Log("在场景1中");
        }
        else if(sceneName=="City"){
            regionleft_xmin = 170f;
            regionleft_xmax = 177f;
            regionright_xmin = 183f;
            regionright_xmax = 190f;
            region_zmin = 134f;
            region_zmax = 144f;
        }

        else{
            regionleft_xmin = 19.24f;
            regionleft_xmax = 20.22f;
            regionright_xmin = 19.24f;
            regionright_xmax = 20.22f;
            region_zmin = 3.75f;
            region_zmax = 19.1f;
        }
        // generate tree randomly in two regions, each have 4 trees
        for (int i = 0; i < 4; i++)
        {
            bool positionFound = false;
            while (!positionFound)
            {
                float x = UnityEngine.Random.Range(regionleft_xmin, regionleft_xmax);
                float z = UnityEngine.Random.Range(region_zmin, region_zmax);
                Collider[] hitColliders = Physics.OverlapSphere(new Vector3(x, 0, z), 1.0f);
                if (hitColliders.Length <= 1)
                {
                    GameObject tree = Instantiate(Resources.Load(loadTrack) as GameObject);
                    tree.transform.position = new Vector3(x, 0, z);
                    TreeNumberLeft++;
                    positionFound = true;
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            bool positionFound = false;
            while (!positionFound)
            {
                float x = UnityEngine.Random.Range(regionright_xmin, regionright_xmax);
                float z = UnityEngine.Random.Range(region_zmin, region_zmax);
                Collider[] hitColliders = Physics.OverlapSphere(new Vector3(x, 0, z), 1.0f);
                if (hitColliders.Length <= 1)
                {
                    GameObject tree = Instantiate(Resources.Load(loadTrack) as GameObject);
                    tree.transform.position = new Vector3(x, 0, z);
                    TreeNumberRight++;
                    positionFound = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if tree number is less than 4, generate a new tree after 5 seconds
        if (TreeNumberLeft < 4)
        {
            StartCoroutine(GenerateTree(1));
        }
        else if (TreeNumberRight < 4)
        {
            StartCoroutine(GenerateTree(2));
        }
    }

    IEnumerator GenerateTree(int region)
    {
        if (region == 1)
            TreeNumberLeft++;
        else
            TreeNumberRight++;

        bool positionFound = false;
        yield return new WaitForSeconds(5);
        while (!positionFound)
        {
            float x, z;
            if (region == 1)
            {
                x = UnityEngine.Random.Range(regionleft_xmin, regionleft_xmax);
                z = UnityEngine.Random.Range(region_zmin, region_zmax);
                // TreeNumberLeft++;
            }
            else
            {
                x = UnityEngine.Random.Range(regionright_xmin, regionright_xmax);
                z = UnityEngine.Random.Range(region_zmin, region_zmax);
                // TreeNumberRight++;
            }

            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(x, 0, z), 1.0f);
            if (hitColliders.Length <= 1)
            {
                GameObject tree = Instantiate(Resources.Load(loadTrack) as GameObject);
                tree.transform.position = new Vector3(x, 0, z);
                positionFound = true;
            }
        }
    }
}
