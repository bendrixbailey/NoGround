using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cluster : MonoBehaviour
{
    /*
     * This method calculates the scale of the certain block. This is done in a separate function so we dont
     * get weird lookin blocks that are super thin but extremely tall. Basically just a more in depth scale randomized function.
     */

    private Vector3 CalculateScale(float minScale, int maxScale) {

        float y_scale = Random.Range(0.2f, 2);
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


    /// <summary>
    /// This adds a few small cubes to the surface of the large cube passed in.
    /// This is done before any stretching, so the details will stretch along with it. Every single block
    /// at this point is still at 1x1x1 scale, so the randompointonmesh function can work. Directly
    /// adds children to the parent, so no return is necessary
    /// </summary>
    /// <param name="parentBlock"> This is the parent block the little detail blocks will be put on</param>
    /// <param name="block">detail block prefab to create from</param>
    /// <returns></returns>
    private void AddSurfaceDetail(GameObject parentBlock, GameObject block, float maxSize, float minSize){
        maxSize = 0.3f;
        minSize = 0.05f;

        //cache these values so multiple calls to GetRandomPointOnMesh doesnt kill the generation
        Mesh mesh = parentBlock.GetComponent<MeshCollider>.sharedMesh;
        float[] sizes = GetTriSizes(mesh.triangles, mesh.vertices);
        float[] cumulativeSizes = new float[sizes.Length];
        float total = 0;

        for (int i = 0; i < sizes.Length; i++)
        {
            total += sizes[i];
            cumulativeSizes[i] = total;
        }

        int numDetailBlocks = Random.Range(0, 8);

        for(int i = 0; i < numDetailBlocks; i ++){
            //create new clone of block on surface somewhere, set its rotation, and set parent
            Instantiate(block, GenerateRandomPointOnMesh(sizes, cumulativeSizes, total), Quaternion.Euler(-90, 0, 0), parentBlock.transform);
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
    private Vector3 GetRandomPointOnMesh(float[] sizes, float[] cumulativeSizes, float total)
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

