using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
