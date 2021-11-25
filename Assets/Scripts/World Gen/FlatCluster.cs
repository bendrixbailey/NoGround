using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatCluster: Cluster 
{
    private int block_count;
    private Vector3 origin_point;
    private float max_origin_offset;
    private GameObject block;
    private GameObject clusterParent;

    private List<GameObject> cubes;
    

    /*
     * Constructor for a single flat cluster. This generates a random cluster of a random number of randomly sized cubes
     * with some variation for different sub-types of clusters. Initializes them at a given vector3 point in the world 
     * determined by the world generator.
     * 
     */
    public void MakeFlatCluster(GameObject parent, GameObject worldBlock, int seed, Color top_color, Color body_color, Vector3 origin_point) { 

        Material[] materials = worldBlock.GetComponent<MeshRenderer>().sharedMaterials;
        block = worldBlock;
        //Random.InitState(seed);
        this.origin_point = origin_point;
        materials[1].color = top_color;
        materials[0].color = body_color;
        clusterParent = parent;

        block_count = Random.Range(4, 10);

        if (block_count > 6)
        {
            max_origin_offset = Random.Range(3, 5);
        }
        else {
            max_origin_offset = Random.Range(1, 3);
        }


        for (int i = 0; i < block_count; i++)
        {   //main loop, runs through generation logic for however many blocks there are within this one piece


            //GameObject newblock = Instantiate(block, CalculatePoint(origin_point, max_origin_offset), Quaternion.Euler(-90, Random.Range(0, 180), 0));
            //GameObject newblock = Instantiate(block, CalculatePoint(origin_point, max_origin_offset), Quaternion.Euler(-90, 0, 0));
            GameObject newblock = Instantiate(block, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
            
            AddSurfaceDetail(newblock, block, 0, 0);
            newblock.transform.localScale = CalculateScale(0.33f, 5, WorldType.Type.Flat);
            newblock.transform.position = CalculatePoint(origin_point, max_origin_offset);
            newblock.transform.parent = clusterParent.transform;

        }
    }
}