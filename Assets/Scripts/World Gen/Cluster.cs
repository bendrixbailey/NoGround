using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cluster : MonoBehaviour
{
    
    /// <summary>
    /// Calculates a random vector3 scale based on min and max scale inputted.
    /// TODO add checks to make sure if its thin, then it doesnt get super squished all the ways
    /// </summary>
    /// <param name="minScale"></param>
    /// <param name="maxScale"></param>
    /// <returns></returns>
    protected Vector3 CalculateScale(float minScale, int maxScale, WorldType.Type type) {



        float y_scale = 0f;
        float z_scale = 0f;
        float x_scale = 0f;

        if(type == WorldType.Type.Cube){                      //if difference is less than 2, its cubic world type
            y_scale = Random.Range(minScale, maxScale);     //all will be close in range, might be more tall/flat but
            z_scale = Random.Range(minScale, maxScale);     //will be close regardless
            x_scale = Random.Range(minScale, maxScale);
        }


        if(type == WorldType.Type.Flat){                                //if min is small, we know we want flat world type
            z_scale = Random.Range(minScale, minScale * 3);
            y_scale = Random.Range(maxScale/3, maxScale);   //recalc z and x to be atleast 3* as large
            x_scale = Random.Range(maxScale/3, maxScale);
        }

        if(type == WorldType.Type.Tall){     //if max is greater than 4* min scale its tall world type
            // if(maxScale/4 < minScale){
            //     maxScale = Mathf.RoundToInt(4* minScale) + 1;
            // }
            z_scale = Random.Range(maxScale - 1, maxScale);   //make sure Y is always greater than either z or x
            y_scale = Random.Range(minScale, minScale + 0.5f);   //recalc z and x to at most be 1/3 the size 
            x_scale = Random.Range(minScale, minScale + 0.5f);
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
    protected Vector3 CalculatePoint(Vector3 origin, float deviation)
    {
        //creates a bottom point so that the random point will be within
        float x_offset = Random.Range(-deviation, deviation);
        float y_offset = Random.Range(-deviation, deviation);
        float z_offset = Random.Range(-deviation, deviation);

        return new Vector3(origin.x + x_offset, origin.y + y_offset, origin.z + z_offset);
    }


    /// <summary>
    /// This adds a few small cubes to the surface of the large cube passed in.
    /// This is done before any stretching, so the details will stretch along with it. Every single block
    /// at this point is still at 1x1x1 scale, so the randompointonmesh function can work. Directly
    /// adds children to the parent, so no return is necessary
    /// </summary>
    /// <param name="parentBlock"> This is the parent block the little detail blocks will be put on</param>
    /// <param name="block">detail block prefab to create from</param>
    /// <returns></returns>
    protected void AddSurfaceDetail(GameObject parentBlock, GameObject block, float maxSize, float minSize){
        maxSize = 0.5f;
        minSize = 0.1f;


        int numDetailBlocks = Random.Range(5, 10);

        for(int i = 0; i < numDetailBlocks; i ++){
            //create new clone of block on surface somewhere, set its rotation, and set parent
            GameObject tempBlock = Instantiate(block, parentBlock.transform.position, Quaternion.Euler(-90, 0, 0), parentBlock.transform);
            //randomize all scales within the given scale modifiers
            float blocksize = Random.Range(minSize, maxSize);
            //make dimensions not square, but slightly vary them within the randomly specified range
            tempBlock.transform.localScale = new Vector3(Random.Range(blocksize - (blocksize * 0.25f), blocksize + (blocksize * 0.25f)), 
                                                         Random.Range(blocksize - (blocksize * 0.25f), blocksize + (blocksize * 0.25f)), 
                                                         Random.Range(blocksize - (blocksize * 0.25f), blocksize + (blocksize * 0.25f)));

            //tempBlock.transform.localPosition = GetRandomPointOnMesh(sizes, cumulativeSizes, total, mesh);
            tempBlock.transform.localPosition = GetRandomPointOnCube(1.15f, blocksize);
        }
    }

    private Vector3 GetRandomPointOnCube(float parentWidth, float blockSize){
        float width = parentWidth - (blockSize * 0.2f);
        float hw = parentWidth * 0.5f;
        int side = Random.Range(0, 6);
        switch(side){
            case 0:
                return new Vector3(-width, Random.Range(-hw, hw), Random.Range(-hw, hw));
            case 1:
                return new Vector3(width, Random.Range(-hw, hw), Random.Range(-hw, hw));
            case 2:
                return new Vector3(Random.Range(-hw, hw), -width, Random.Range(-hw, hw));
            case 3:
                return new Vector3(Random.Range(-hw, hw), width, Random.Range(-hw, hw));
            case 4:
                return new Vector3(Random.Range(-hw, hw), Random.Range(-hw, hw), -(width - 0.2f));
            case 5:
                return new Vector3(Random.Range(-hw, hw), Random.Range(-hw, hw), (width - 0.2f));
            default:
                return new Vector3(0, 0, 0);
        }
    }
}