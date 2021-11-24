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

        //cache these values so multiple calls to GetRandomPointOnMesh doesnt kill the generation
        Mesh mesh = parentBlock.GetComponent<MeshFilter>().sharedMesh;
        float[] sizes = GetTriSizes(mesh.triangles, mesh.vertices);
        float[] cumulativeSizes = new float[sizes.Length];
        float total = 0;


        for (int i = 0; i < sizes.Length; i++)
        {
            total += sizes[i];
            cumulativeSizes[i] = total;
        }


        int numDetailBlocks = Random.Range(5, 15);

        for(int i = 0; i < numDetailBlocks; i ++){
            //create new clone of block on surface somewhere, set its rotation, and set parent
            GameObject tempBlock = Instantiate(block, parentBlock.transform.position, Quaternion.Euler(-90, 0, 0), parentBlock.transform);
            //randomize all scales within the given scale modifiers
            float blocksize = Random.Range(minSize, maxSize);
            //make dimensions not square, but slightly vary them within the randomly specified range
            tempBlock.transform.localScale = new Vector3(Random.Range(blocksize - (blocksize * 0.25f), blocksize + (blocksize * 0.25f)), 
                                                         Random.Range(blocksize - (blocksize * 0.25f), blocksize + (blocksize * 0.25f)), 
                                                         Random.Range(blocksize - (blocksize * 0.25f), blocksize + (blocksize * 0.25f)));

            tempBlock.transform.localPosition = GetRandomPointOnMesh(sizes, cumulativeSizes, total, mesh);
        }
    }

    /// <summary>
    /// This function was borrowed from https://gist.github.com/v21/5378391. It generates a random point 
    /// on a mesh. This is used to create the location of all of the small detail blocks. Originally it calculated
    /// sizes, cumulativeSizes, and total each call, but because every time this is called, its the same mesh, then
    /// I can store them outside and just pass them in, allowing the code to only calculate it once per call.
    /// </summary>
    /// <param name="sizes">cached sizes array</param>
    /// <param name="cumulativeSizes">cached cumulative sizes array</param>
    /// <param name="total">cached total count</param>
    /// <returns></returns>
    private Vector3 GetRandomPointOnMesh(float[] sizes, float[] cumulativeSizes, float total, Mesh mesh)
    {
        //if you're repeatedly doing this on a single mesh, you'll likely want to cache cumulativeSizes and total

        //so everything above this point wants to be factored out

        float randomsample = Random.value* total;

        int triIndex = -1;
        
        for (int i = 0; i < sizes.Length; i++)
        {
            if (randomsample <= cumulativeSizes[i])
            {
                triIndex = i;
                break;
            }
        }

        if (triIndex == -1) Debug.LogError("triIndex should never be -1");

        Vector3 a = mesh.vertices[mesh.triangles[triIndex * 3]];
        Vector3 b = mesh.vertices[mesh.triangles[triIndex * 3 + 1]];
        Vector3 c = mesh.vertices[mesh.triangles[triIndex * 3 + 2]];

        //generate random barycentric coordinates

        float r = Random.value;
        float s = Random.value;

        if(r + s >=1)
        {
            r = 1 - r;
            s = 1 - s;
        }
        //and then turn them back to a Vector3
        Vector3 pointOnMesh = a + r*(b - a) + s*(c - a);
        return pointOnMesh;

    }

    private float[] GetTriSizes(int[] tris, Vector3[] verts)
    {
        int triCount = tris.Length / 3;
        float[] sizes = new float[triCount];
        for (int i = 0; i < triCount; i++)
        {
            sizes[i] = .5f*Vector3.Cross(verts[tris[i*3 + 1]] - verts[tris[i*3]], verts[tris[i*3 + 2]] - verts[tris[i*3]]).magnitude;
        }
        return sizes;
    }
}

