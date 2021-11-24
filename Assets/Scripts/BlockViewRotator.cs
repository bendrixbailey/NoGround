using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a very simple script for rotating a game object
/// on a specified axis. Uses public booleans to change the rotation axis, 
/// can be updated mid game. Used to rotate the viewing camera
/// to view the prototype clusters.
/// </summary>
public class BlockViewRotator : MonoBehaviour
{

    //Rotational Speed
    public float speed = 0f;
   
    //Forward Direction
    public bool ForwardX = false;
    public bool ForwardY = false;
    public bool ForwardZ = false;
   
    //Reverse Direction
    public bool ReverseX = false;
    public bool ReverseY = false;
    public bool ReverseZ = false;
   
    void Update ()
    {
        //Forward Direction
        if(ForwardX == true)
        {
            transform.Rotate(Time.deltaTime * speed, 0, 0, Space.Self);
        }
        if(ForwardY == true)
        {
            transform.Rotate(0, Time.deltaTime * speed,  0, Space.Self);
        }
        if(ForwardZ == true)
        {
            transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
        }
        //Reverse Direction
        if(ReverseX == true)
        {
            transform.Rotate(-Time.deltaTime * speed, 0, 0, Space.Self);
        }
        if(ReverseY == true)
        {
            transform.Rotate(0, -Time.deltaTime * speed,  0, Space.Self);
        }
        if(ReverseZ == true)
        {
            transform.Rotate(0, 0, -Time.deltaTime * speed, Space.Self);
        }
       
    }
}

