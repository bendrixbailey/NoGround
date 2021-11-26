using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaWorldGen
{
    private int clusterCount;
    private int hugeClusters;
    private int massiveClusters;

    private GameObject tempCluster = new GameObject("Cluster");

    private GameObject PlayAreaParent = new GameObject("PlayArea");

    private GameObject worldBlock;
    private int seed;
    private Color grassColor;
    private Color rockColor;
    private WorldType.Type worldType;
    private Vector3 worldTypeCollection;    //x=cubes, y=flats, z=talls

    private int worldTypeTracker = 1;

    /// <summary>
    /// Constructor for the Play area world gen. 
    /// </summary>
    /// <param name="worldBlock"></param>
    /// <param name="seed"></param>
    /// <param name="grassColor"></param>
    /// <param name="rockColor"></param>
    public PlayAreaWorldGen(GameObject worldBlock, int seed, Color grassColor, Color rockColor, WorldType.Type worldType) {

        this.worldBlock = worldBlock;
        this.seed = seed;
        this.grassColor = grassColor;
        this.rockColor = rockColor;
        this.worldType = worldType;
        //Random.InitState(seed);

        //clusterCount = Random.Range(300, 400);
        hugeClusters = Random.Range(20, 40);

        this.worldTypeCollection = PickWorldType(this.worldType);
    }

    // public void UpdateBlockTypeSelected(int type){
    //     switch(type){
    //         case 1:
    //             if(worldTypeTracker != 1){
    //                 foreach (Transform child in tempCluster.transform) {
    //                     GameObject.Destroy(child.gameObject);
    //                 }
    //                 CubicCluster newCluster = tempCluster.AddComponent<CubicCluster>();
    //                 newCluster.MakeCubicCluster(tempCluster, worldBlock, Random.Range(100, 999), grassColor, rockColor, new Vector3(0, 0, 0));
    //                 worldTypeTracker = 1;
    //             }
    //             break;
    //         case 2:
    //             if(worldTypeTracker != 2){
    //                 foreach (Transform child in tempCluster.transform) {
    //                     GameObject.Destroy(child.gameObject);
    //                 }
    //                 FlatCluster newFlatCluster = tempCluster.AddComponent<FlatCluster>();
    //                 newFlatCluster.MakeFlatCluster(tempCluster, worldBlock, Random.Range(100, 999), grassColor, rockColor, new Vector3(0, 0, 0));
    //                 worldTypeTracker = 2;
    //             }
    //             break;
    //         case 3:
    //             if(worldTypeTracker != 3){
    //                 foreach (Transform child in tempCluster.transform) {
    //                     GameObject.Destroy(child.gameObject);
    //                 }
    //                 TallCluster newTallCluster = tempCluster.AddComponent<TallCluster>();
    //                 newTallCluster.MakeTallCluster(tempCluster, worldBlock, Random.Range(100, 999), grassColor, rockColor, new Vector3(0, 0, 0));
    //                 worldTypeTracker = 3;
    //             }
    //             break;
    //     }
    // }

    public void CreateWorld(){
        int cubes = Mathf.RoundToInt(this.worldTypeCollection.x);
        int flat = Mathf.RoundToInt(this.worldTypeCollection.y);
        int tall = Mathf.RoundToInt(this.worldTypeCollection.z);

        for(int i = 0; i < cubes; i++){
            GameObject newCluster = new GameObject("CubeCluster" + i);
            CubicCluster cubeCluster = newCluster.AddComponent<CubicCluster>();
            cubeCluster.MakeCubicCluster(newCluster, worldBlock, seed, grassColor, rockColor);
            newCluster.transform.position = CalculatePoint();
            newCluster.transform.SetParent(PlayAreaParent.transform);
            newCluster.transform.localScale = CalculateScale();
            
        }
        for(int j = 0; j < flat; j++){
            GameObject newCluster = new GameObject("FlatCluster" + j);
            FlatCluster flatCluster = newCluster.AddComponent<FlatCluster>();
            flatCluster.MakeFlatCluster(newCluster, worldBlock, seed, grassColor, rockColor);
            newCluster.transform.position = CalculatePoint();
            newCluster.transform.parent = PlayAreaParent.transform;
            newCluster.transform.localScale = CalculateScale();
        }
        for(int k = 0; k < tall; k++){
            GameObject newCluster = new GameObject("TallCluster" + k);
            TallCluster tallCluster = newCluster.AddComponent<TallCluster>();
            tallCluster.MakeTallCluster(newCluster, worldBlock, seed, grassColor, rockColor);
            newCluster.transform.position = CalculatePoint();
            newCluster.transform.parent = PlayAreaParent.transform;
            newCluster.transform.localScale = CalculateScale();
        }
    }


    /*
     * This method is used to generate a random point at which the cluster of blocks will be placed.
     * Returns a vector3 coordinate of the cluster parent.
     */
    private Vector3 CalculatePoint() {
        //dimentions: 300x900x300 (x,y,z)

        int x_point = Random.Range(-100, 100);
        int y_point = Random.Range(10, 300);
        int z_point = Random.Range(-100, 100);


        return new Vector3(x_point, y_point, z_point);
    }

    /*
     * This method calculates the scale of a certain cluster. If the cluster is a large one, then it changes the randomization bounds.
     * 
     */
    private Vector3 CalculateScale()
    {

        int scale;


        int big = Random.Range(0,2);

        if(this.hugeClusters > 0 && big == 1){
            scale = Random.Range(10, 20);
            this.hugeClusters --;
        }
        else
        {
            scale = Random.Range(3, 7);
        }

        return new Vector3(scale, scale, scale);
    }

    /// <summary>
    /// This function picks the world type (flat, tall, or normal)
    /// and calculates the number of each clusters that should be created.
    /// ex a flat world type would have 80 flat clusters, 10 normal, and 10 tall
    /// With a Vector3, x = flatCount, y = tallCount, z = normalCount
    /// </summary>
    /// <returns>Vector3 containing count of each cluster type to build</returns>
    private Vector3 PickWorldType(WorldType.Type type){
        switch(type){
            case WorldType.Type.Cube:
                return new Vector3(80, 10, 10);     //cubic world type
            case WorldType.Type.Flat: 
                return new Vector3(10, 80, 10);     //flat world type
            case WorldType.Type.Tall:
                return new Vector3(10, 10, 80);     //tall world type
            default:
                return new Vector3 (80, 10, 10);
        }
    }
}
