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

    private int worldTypeTracker = 1;

    public PlayAreaWorldGen(GameObject worldBlock, int seed, Color grassColor, Color rockColor) {

        this.worldBlock = worldBlock;
        this.seed = seed;
        this.grassColor = grassColor;
        this.rockColor = rockColor;

        clusterCount = Random.Range(300, 400);
        hugeClusters = Random.Range(150, 200);

        Vector3 worldTypeCollection = PickWorldType();



        //creates all clusters here
        for (int i = 0; i < 1; i++) {
            // GameObject clusterParent = new GameObject("Cluster" + i);
            // clusterParent.SetActive(false);

            Vector3 tempLoc = new Vector3(0, 0, 0);

            CubicCluster newCluster = tempCluster.AddComponent<CubicCluster>();
            newCluster.MakeCubicCluster(tempCluster, worldBlock, Random.Range(100, 999), grassColor, rockColor, tempLoc);
            //clusterParent.SetActive(true);

            // FlatCluster newFlatCluster = clusterParent.AddComponent<FlatCluster>();
            // newFlatCluster.MakeFlatCluster(clusterParent, worldBlock, Random.Range(100, 999), grassColor, rockColor, tempLoc);
            // clusterParent.SetActive(true);

            // TallCluster newCluster = clusterParent.AddComponent<TallCluster>();
            // newCluster.MakeTallCluster(clusterParent, worldBlock, Random.Range(100, 999), grassColor, rockColor, tempLoc);
            // clusterParent.SetActive(true);
        }
    }

    public void UpdateBlockTypeSelected(int type){
        switch(type){
            case 1:
                if(worldTypeTracker != 1){
                    foreach (Transform child in tempCluster.transform) {
                        GameObject.Destroy(child.gameObject);
                    }
                    CubicCluster newCluster = tempCluster.AddComponent<CubicCluster>();
                    newCluster.MakeCubicCluster(tempCluster, worldBlock, Random.Range(100, 999), grassColor, rockColor, new Vector3(0, 0, 0));
                    worldTypeTracker = 1;
                }
                break;
            case 2:
                if(worldTypeTracker != 2){
                    foreach (Transform child in tempCluster.transform) {
                        GameObject.Destroy(child.gameObject);
                    }
                    FlatCluster newFlatCluster = tempCluster.AddComponent<FlatCluster>();
                    newFlatCluster.MakeFlatCluster(tempCluster, worldBlock, Random.Range(100, 999), grassColor, rockColor, new Vector3(0, 0, 0));
                    worldTypeTracker = 2;
                }
                break;
            case 3:
                if(worldTypeTracker != 3){
                    foreach (Transform child in tempCluster.transform) {
                        GameObject.Destroy(child.gameObject);
                    }
                    TallCluster newTallCluster = tempCluster.AddComponent<TallCluster>();
                    newTallCluster.MakeTallCluster(tempCluster, worldBlock, Random.Range(100, 999), grassColor, rockColor, new Vector3(0, 0, 0));
                    worldTypeTracker = 3;
                }
                break;
        }
    }


    /*
     * This method is used to generate a random point at which the cluster of blocks will be placed.
     * Returns a vector3 coordinate of the cluster parent.
     */
    private Vector3 CalculatePoint() {
        //dimentions: 300x900x300 (x,y,z)

        float x_point = Random.Range(-340, 340);
        float y_point = Random.Range(10, 890);
        float z_point = Random.Range(-340, 340);


        return new Vector3(x_point, y_point, z_point);
    }

    /*
     * This method calculates the scale of a certain cluster. If the cluster is a large one, then it changes the randomization bounds.
     * 
     */
    private Vector3 CalculateScale(bool huge)
    {

        float y_scale;
        float x_scale;
        float z_scale;

        if (huge)
        {
            y_scale = Random.Range(17, 30);
            x_scale = Random.Range(17, 30);
            z_scale = Random.Range(17, 30);
        }
        else
        {
            y_scale = Random.Range(5, 15);
            x_scale = Random.Range(5, 15);
            z_scale = Random.Range(5, 15);
        }

        return new Vector3(x_scale, y_scale, z_scale);
    }

    /// <summary>
    /// This function picks the world type (flat, tall, or normal)
    /// and calculates the number of each clusters that should be created.
    /// ex a flat world type would have 80 flat clusters, 10 normal, and 10 tall
    /// With a Vector3, x = flatCount, y = tallCount, z = normalCount
    /// </summary>
    /// <returns>Vector3 containing count of each cluster type to build</returns>
    private Vector3 PickWorldType(){
        int res = Random.Range(0, 3);
        switch(res){
            case 1:
                return new Vector3(80, 10, 10);
            case 2: 
                return new Vector3(10, 80, 10);
            case 3:
                return new Vector3(10, 10, 80);
            default:
                return new Vector3 (0, 0, 0);
        }
    }
}
