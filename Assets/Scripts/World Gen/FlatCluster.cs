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

    //Since this is a flat cluster, the minimum and max will be smaller than usual, and closer together
    private float minScale;
    private int maxScale;

    

    /*
     * Constructor for a single cubic cluster. This generates a random cluster of a random number of randomly sized cubes
     * with some variation for different sub-types of clusters. Initializes them at a given vector3 point in the world 
     * determined by the cubic world generator.
     * 
     */
    public void MakeCubicCluster(GameObject parent, GameObject worldBlock, int seed, Color top_color, Color body_color) { 

        Material[] materials = worldBlock.GetComponent<MeshRenderer>().sharedMaterials;
        block = worldBlock;
        clusterParent = parent;
        materials[1].color = top_color;
        materials[0].color = body_color;

        block_count = Random.Range(2, 8);

        if (block_count > 6)
        {
            max_origin_offset = Random.Range(3, 5);
        }
        else {
            max_origin_offset = Random.Range(1, 3);
        }


        for (int i = 0; i <= block_count; i++)
        {   //main loop, runs through generation logic for however many blocks there are within this one piece


            //GameObject newblock = Instantiate(block, CalculatePoint(origin_point, max_origin_offset), Quaternion.Euler(-90, Random.Range(0, 180), 0));
            //Block is created
            GameObject newblock = Instantiate(block, CalculatePoint(origin_point, max_origin_offset), Quaternion.Euler(-90, 0, 0));

            //small detail is added to each block randomly
            
            
            //block is stretched/scaled
            newblock.transform.localScale = CalculateScale();

            //block is moved to final place in cluster
            newblock.transform.parent = clusterParent.transform;

        }
}