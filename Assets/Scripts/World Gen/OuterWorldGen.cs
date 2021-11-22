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

public class OuterWorldGen {

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
