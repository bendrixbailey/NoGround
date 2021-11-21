using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This method creates an entire cubic world using CubicCluster.cs to generate the smaller pieces.
 * This method is called by WorldGenerator.cs if the cubic worldtype is selected.
 * This will generate about 30 different cubic clusters at wide scattered points around the map.
 * 
 * Map has a height of 250, width of 750. Total dimensions are always a 75x250x75 box.
 * 
 * Outside this box, there is a surrounding large box that contains the background structures. Cannot be reached by the player.
 * 
 * There can be a maximum of 50 cubic clusters and a minimum of 30. Each cubic cluster is also randomized with scale, 
 * with between 1 and 4 clusters over 5 times their size.
 */

public class CubicWorldType {

    private int clusterCount;
    private int hugeClusters;
    private int massiveClusters;

    public CubicWorldType(GameObject generatorParent, GameObject worldBlock, int seed, Color grassColor, Color rockColor) {

        clusterCount = Random.Range(300, 400);
        hugeClusters = Random.Range(150, 200);

        
        for (int i = 0; i <= 1; i++) {
            GameObject clusterParent = new GameObject("Cluster" + i);
            clusterParent.SetActive(false);

            CubicCluster newCluster = clusterParent.AddComponent<CubicCluster>();
            newCluster.MakeCubicCluster(clusterParent, worldBlock, Random.Range(100, 999), grassColor, rockColor);
            clusterParent.SetActive(true);

        // if ((hugeClusters > 0) && (Random.Range(0, 10) % 4 == 0))
        //     {
        //         clusterParent.transform.localScale = CalculateScale(true);
        //     }
        //     else {
        //         clusterParent.transform.localScale = CalculateScale(false);
        //     }
        //     clusterParent.transform.position = CalculatePoint();
        //     clusterParent.transform.parent = generatorParent.transform;
        }

        // GenerateOutsideObjectSide(new Vector3(2000, 0, 0), grassColor, rockColor, generatorParent, worldBlock, false);
        // GenerateOutsideObjectSide(new Vector3(-2000, 0, 0), grassColor, rockColor, generatorParent, worldBlock, false);
        // GenerateOutsideObjectSide(new Vector3(0, 0, 2000), grassColor, rockColor, generatorParent, worldBlock, true);
        // GenerateOutsideObjectSide(new Vector3(0, 0, -2000), grassColor, rockColor, generatorParent, worldBlock, true);

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

    /**
     * This method creates one side of the 4 sided field of massive asthetic objects just outside the 
     * view field.
     */ 
    private void GenerateOutsideObjectSide(Vector3 centerPoint, Color grassColor, Color rockColor, GameObject parent, GameObject worldBlock, bool rotate) {
        int clusterNumber = Random.Range(5, 10);
        int clusterSize = 20;
        GameObject outsideClusterParent = new GameObject("OutsideBlocks");

        for (int i = 0; i <= clusterNumber; i++) {
            GameObject clusterParent = new GameObject("Cluster" + i);


            CubicCluster newCluster = clusterParent.AddComponent<CubicCluster>();
            newCluster.MakeCubicCluster(clusterParent, worldBlock, Random.Range(100, 999), grassColor, rockColor);

            newCluster.transform.localScale = new Vector3(clusterSize, clusterSize, clusterSize);

            clusterParent.transform.SetParent(outsideClusterParent.transform);

           
        }
        outsideClusterParent.transform.localScale = new Vector3(15, 15, 30);
        outsideClusterParent.transform.position = centerPoint;

        if (rotate)
        {
            outsideClusterParent.transform.Rotate(new Vector3(0,90,0));
        }

        outsideClusterParent.transform.SetParent(parent.transform);

    }
}
