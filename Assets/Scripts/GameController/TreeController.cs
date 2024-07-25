using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

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
    private int max_round = 100000;
    public int lrBorder = 800;

    public String loadTrack = "Prefabs/Tree_1_1";
    void Start()
    {
        
        // generate tree randomly in two regions, each have 4 trees
        for (int i = 0; i < 4; i++)
        {
            bool positionFound = false;
            int round = 0;
            while (!positionFound && round < max_round)
            {
                round++;
                float x = UnityEngine.Random.Range(regionleft_xmin, regionleft_xmax);
                float z = UnityEngine.Random.Range(region_zmin, region_zmax);
                bool hitTree = false;
                Collider[] hitColliders = Physics.OverlapSphere(new Vector3(x, 0, z), 1.0f);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.tag == "Tree")
                    {
                        hitTree = true;
                        break;
                    }
                }
                if (!hitTree)
                {
                    GameObject tree = Instantiate(Resources.Load(loadTrack) as GameObject);
                    tree.transform.position = new Vector3(x, 0, z);
                    tree.GetComponent<Trees>().lrBorder = lrBorder;
                    TreeNumberLeft++;
                    positionFound = true;
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            int round = 0;
            bool positionFound = false;
            while (!positionFound && round < max_round)
            {
                round++;
                float x = UnityEngine.Random.Range(regionright_xmin, regionright_xmax);
                float z = UnityEngine.Random.Range(region_zmin, region_zmax);
                bool hitTree = false;
                Collider[] hitColliders = Physics.OverlapSphere(new Vector3(x, 0, z), 1.0f);
                foreach(var hitCollider in hitColliders)
                {
                    if(hitCollider.gameObject.tag == "Tree")
                    {
                        hitTree = true;
                        break;
                    }
                }
                if (!hitTree)
                {
                    GameObject tree = Instantiate(Resources.Load(loadTrack) as GameObject);
                    tree.transform.position = new Vector3(x, 0, z);
                    tree.GetComponent<Trees>().lrBorder = lrBorder;
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
        int round = 0;
        while (!positionFound && round < max_round)
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
            bool hitTree = false;
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.gameObject.tag == "Tree")
                {
                    hitTree = true;
                    break;
                }
            }
            if (!hitTree)
            {
                GameObject tree = Instantiate(Resources.Load(loadTrack) as GameObject);
                tree.transform.position = new Vector3(x, 0, z);
                tree.GetComponent<Trees>().lrBorder = lrBorder;
                positionFound = true;
            }
            round += 1;
            if(round == max_round)
            {
                Debug.Log("Cannot find a position to generate tree");
            }
        }
    }
}
