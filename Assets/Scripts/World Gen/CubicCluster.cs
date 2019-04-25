using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This class generates a cubic looking structure randomized by a seed. Used by Test_World_Gen.cs.
 * Uses a predetermined randomized seed given by the worldgen class to seed the random variables to be repeatable.
 * 
 * Changes the colors of the block based off seed. Keeps the colors within a reasonable range so we dont get pitch black or 
 * bright white or neon colors.
 */

public class CubicCluster: MonoBehaviour
{

    private int block_count;
    private Vector3 origin_point;
    private float max_origin_offset;
    private GameObject block;
    private GameObject clusterParent;

    private List<GameObject> cubes;
    

    /*
     * Constructor for a single cubic cluster. This generates a random cluster of a random number of randomly sized cubes
     * with some variation for different sub-types of clusters. Initializes them at a given vector3 point in the world 
     * determined by the cubic world generator.
     * 
     */
    public void MakeCubicCluster(GameObject parent, GameObject worldBlock, int seed, Color top_color, Color body_color) { 

        Material[] materials = worldBlock.GetComponent<MeshRenderer>().sharedMaterials;
        block = worldBlock;
        materials[1].color = top_color;
        materials[0].color = body_color;
        clusterParent = parent;

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


            GameObject newblock = Instantiate(block, CalculatePoint(origin_point, max_origin_offset), Quaternion.Euler(-90, Random.Range(0, 180), 0));
            newblock.transform.localScale = CalculateScale();
            newblock.transform.parent = clusterParent.transform;

        }


    }



    /*
     * This method calculates the scale of the certain block. This is done in a separate function so we dont
     * get weird lookin blocks that are super thin but extremely tall. Basically just a more in depth scale randomized function.
     */

    private Vector3 CalculateScale() {

        float y_scale = Random.Range(0.5f, 2);
        float z_scale = 0;
        float x_scale = 0;

        if (y_scale >= 1)
        {
            x_scale = Random.Range(0.5f, 2);
            z_scale = Random.Range(0.5f, 2);
        }
        else {
            x_scale = Random.Range(2, 3);
            z_scale = Random.Range(2, 3);
        }



        return new Vector3(x_scale, y_scale, z_scale);
    }

    /*
     * This method creates a random point within the given deviation from the offset.
     * Works by finding the bottom left point, uses mathf.abs so it works with negative and positive directions.
     * Then it calculates a random value within that bottom point and 2 * deviation, and then divides it by two, and 
     * adds or subtracts it from the origin. This value is the new x, y, or z value of the new vector3. Once all are done,
     * the new Vector3 is returned.
     * 
     * Returns vector3 of random point within bounds.
     */
    private Vector3 CalculatePoint(Vector3 origin, float deviation)
    {
        //creates a bottom point so that the random point will be within
        float x_offset = Random.Range(-deviation, deviation);
        float y_offset = Random.Range(-deviation, deviation);
        float z_offset = Random.Range(-deviation, deviation);

        return new Vector3(origin.x + x_offset, origin.y + y_offset, origin.z + z_offset);
    }
}

	
