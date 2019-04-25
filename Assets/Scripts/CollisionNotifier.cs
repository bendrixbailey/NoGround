using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is used to notify the GliderController which type of block the 
 * glider has actually hit. Because the GliderController script is on a parent,
 * it cannot actually see if the glidercollider has hit any blocks. This is where
 * this script is used. Uses a boolean that simply relays whether the land has 
 * been hit.
 * 
 * 
 * Also used on score collision spheres. Basically anything where a collider
 * would need to be notified about colliding with the land
 * 
 * author: Bendrix Bailey
 */ 
public class CollisionNotifier : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Land") {
            triggered = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Land") {
            triggered = false;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Land") {
            triggered = true;
        }
    }

    public bool getTriggered() {
        return triggered;
    }
}
