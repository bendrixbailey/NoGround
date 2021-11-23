using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class serves as a generator for the outside boundary islands and blocks, as well as clouds.
/// It generates each side individually, and then positions each side at 2000 blocks away from the center, and rotates them.
/// Given the parent object passed down to it, it creates each side and nests them inside the parent,
/// without returning anything.
/// </summary>

public class OuterWorldGen {

    // /**
    //  * This method creates one side of the 4 sided field of massive asthetic objects just outside the 
    //  * view field.
    //  */ 
    // private void GenerateOutsideObjectSide(Vector3 centerPoint, Color grassColor, Color rockColor, GameObject parent, GameObject worldBlock, bool rotate) {
    //     int clusterNumber = Random.Range(5, 10);
    //     int clusterSize = 20;
    //     GameObject outsideClusterParent = new GameObject("OutsideBlocks");

    //     for (int i = 0; i <= clusterNumber; i++) {
    //         GameObject clusterParent = new GameObject("Cluster" + i);


    //         CubicCluster newCluster = clusterParent.AddComponent<CubicCluster>();
    //         newCluster.MakeCubicCluster(clusterParent, worldBlock, Random.Range(100, 999), grassColor, rockColor);

    //         newCluster.transform.localScale = new Vector3(clusterSize, clusterSize, clusterSize);

    //         clusterParent.transform.SetParent(outsideClusterParent.transform);

           
    //     }
    //     outsideClusterParent.transform.localScale = new Vector3(15, 15, 30);
    //     outsideClusterParent.transform.position = centerPoint;

    //     if (rotate)
    //     {
    //         outsideClusterParent.transform.Rotate(new Vector3(0,90,0));
    //     }

    //     outsideClusterParent.transform.SetParent(parent.transform);

    // }
}
