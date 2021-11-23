using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This class is a test class for generation of various world blocks.
 * By default it clusters random size and scale blocks together within reason. (About 3x original block size)
 * All these random blocks are around a center point, so this will be part of a larger worldgen structure.
 * 
 * Seed is by default randomly generated, but can be user inputted to get repeatable generation.
 * Uses the Random.InitState(seed) with unity's random function to create pseudo random generation
 * that can be recreated by entering the same number.
 * 
 * Seed is a randomly generated 6 digit number.
 * 
 * 
 * TODO: implement a more flat type of worldgen, implement a pillar type of worldgen, implement an arch type of worldgen,
 * implement a hoop type of worldgen. 
 * 
 * 
 * Author: Bendrix Bailey
 */

public class WorldGenerator : MonoBehaviour
{
    [Header("Game Generation")]
    [SerializeField] private int seed = 0; 
    [SerializeField] private GameObject worldBlock;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject flower;

    [Header("Clouds")]
    [SerializeField] private GameObject[] bigClouds;
    [SerializeField] private GameObject[] mediumClouds;
    [SerializeField] private GameObject[] smallClouds;
    [SerializeField] private GameObject[] wispyClouds;


    private Color cloudColor;
    private Color grassColor;
    private Color rockColor;


    /*
     * This method is what generates the world. World is small enough so it can be generated all at once.
     * Uses seed based randomness to create repeatable world generation while keeping it random    
     * 
     */
    void Start()
    {
        if (seed == 0) {
            seed = Random.Range(100000, 999999);
        }

        Random.InitState(seed);

        grassColor = Random.ColorHSV(0.19f, 0.44f, 0.3f, 1f, 0.2f, 0.4f);
        rockColor = Random.ColorHSV(0.5f, 1f, 0.3f, 1f, 0.3f, 0.5f);


        //Attempted implementation of view distance fog, unity fog is crappy,
        //need to write own fog scripts
        RenderSettings.fog = true;
        RenderSettings.fogColor = rockColor;

        GenerateClouds();

        //if (seed % 2 == 0) {
        //Generates a new world type with a cubic layout.
        //More worldtypes will be implemented here, and it will be randomly chosen based off the seeed
        new PlayAreaWorldGen(worldBlock, seed, grassColor, rockColor);
        //var unused = new CubicWorldType(new GameObject("Landscape"), worldBlock, seed, grass_color, rock_color);
        //}

    }

    /*
     * This method is used to generate a random type of clouds for the world. All random functions use the seed
     * that the world was created with.
     */ 

    private void GenerateClouds() {
        GameObject clouds = new GameObject("Clouds");
        clouds.transform.parent = gameObject.transform;
        int width = 1000;
        int height = 100;



        Vector3 pos = new Vector3(Random.Range(-(width / 2), (width / 2)), Random.Range(1000, 1000 + height), Random.Range(-(width / 2), (width / 2)));

        int cloudType = Random.Range(1, 4);
        if (cloudType == 1)
        {
            for (int i = 0; i <= 30; i++) {
                GameObject cloud = Instantiate(bigClouds[Random.Range(0, bigClouds.Length)], pos, Quaternion.identity);
                cloud.transform.parent = clouds.transform;
            }
        }
        else if (cloudType == 2)
        {
            for (int i = 0; i <= 60; i++)
            {
                GameObject cloud = Instantiate(mediumClouds[Random.Range(0, mediumClouds.Length)], pos, Quaternion.identity);
                cloud.transform.parent = clouds.transform;
            }
        }
        else if (cloudType == 3)
        {
            for (int i = 0; i <= 30; i++)
            {
                GameObject cloud = Instantiate(smallClouds[Random.Range(0, smallClouds.Length)], pos, Quaternion.identity);
                cloud.transform.parent = clouds.transform;
            }
        }
        else
        {
            for (int i = 0; i <= 30; i++)
            {
                GameObject cloud = Instantiate(wispyClouds[Random.Range(0, wispyClouds.Length)], pos, Quaternion.identity);
                cloud.transform.parent = clouds.transform;
            }
        }


    }
}
