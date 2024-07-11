using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    // Start is called before the first frame update
    public int TreeNumberLeft = 0;
    public int TreeNumberRight = 0;
    private float regionleft_xmin = 794f;
    private float regionleft_xmax = 798f;
    private float regionright_xmin = 800.5f;
    private float regionright_xmax = 804f;
    private float region_zmin = 983f;
    private float region_zmax = 986f;
    void Start()
    {
        // generate tree randomly in two regions, each have 4 trees
        for(int i = 0; i < 4; i++){
            float x = Random.Range(regionleft_xmin, regionleft_xmax);
            float z = Random.Range(region_zmin, region_zmax);
            GameObject tree = Instantiate(Resources.Load("Prefabs/Tree_1_1") as GameObject);
            tree.transform.position = new Vector3(x, 0, z);
            TreeNumberLeft++;
        }
        for(int i = 0; i < 4; i++){
            float x = Random.Range(regionright_xmin, regionright_xmax);
            float z = Random.Range(region_zmin, region_zmax);
            GameObject tree = Instantiate(Resources.Load("Prefabs/Tree_1_1") as GameObject);
            tree.transform.position = new Vector3(x, 0, z);
            TreeNumberRight++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if tree number is less than 4, generate a new tree after 5 seconds
        if(TreeNumberLeft < 4){
            StartCoroutine(GenerateTree(1));
        }
        else if(TreeNumberRight < 4){
            StartCoroutine(GenerateTree(2));
        }
    }

    IEnumerator GenerateTree(int region){
        yield return new WaitForSeconds(5);
        if(region == 1){
            float x = Random.Range(regionleft_xmin, regionleft_xmax);
            float z = Random.Range(region_zmin, region_zmax);
            GameObject tree = Instantiate(Resources.Load("Prefabs/Tree_1_1") as GameObject);
            tree.transform.position = new Vector3(x, 0, z);
            TreeNumberLeft++;
        }
        else{
            float x = Random.Range(regionright_xmin, regionright_xmax);
            float z = Random.Range(region_zmin, region_zmax);
            GameObject tree = Instantiate(Resources.Load("Prefabs/Tree_1_1") as GameObject);
            tree.transform.position = new Vector3(x, 0, z);
            TreeNumberRight++;
        }
    }
}
