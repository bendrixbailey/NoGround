using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class acts as the main movement controller for the glider object. This also
 * handles the movement of the camera that follows the glider. The camera is not a child of the
 * glider parent, as its distance from the actual model changes and floats with speed
 * and direction of the glider itself.
 * 
 * The glider has no rigidbody, so all movement is done by moving the transform.
 * 
 * This class also controls the tilting when the player turns
 * 
 * TODO implement a way for the glider to have a max downward tilt, so the thing never
 * reverses.
 * 
 * author: Bendrix Bailey
 * 
 */ 
public class GliderController : MonoBehaviour {

    [Header("Movmement Attributes")]
    [SerializeField] float startSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float horiTurnSpeed = 1;
    [SerializeField] float veriTurnSpeed = 2;
    [SerializeField] float dragAmount = 0.01f;

    [SerializeField] float speedMultiplier;
    [SerializeField] float speedIncreaseMultiplier;

    [Header("Objects")]
    [SerializeField] Transform chaseCam;
    [SerializeField] GameObject gliderModel;
    [SerializeField] CameraShake cameraShake;
    float speed;


	// Use this for initialization
	void Start () {
        speed = startSpeed;
        cameraShake.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        //the camera position is updated every frame to a certain position behind the glider
        Vector3 newCamPos = transform.position - transform.forward * 2.0f + Vector3.up * 1.0f;


        //camera offset is calculated based off of speed and location of the glider
        float cameraOffset = 0.91f;
        chaseCam.position = chaseCam.position * cameraOffset + newCamPos*(1.0f-cameraOffset);
        chaseCam.LookAt(transform.position + transform.forward * 20.0f);

        //increases the speed of the glider based off of the angle down this glider is.
        transform.position += transform.forward * Time.deltaTime * speed * speedMultiplier;

        //speed decreases if you're tipped up above 90
        speed -= transform.forward.y * speedIncreaseMultiplier;


        //handles rotation of the glider based off of the input from vertical and horizontal
        //axes;
        transform.Rotate(Input.GetAxis("Vertical") * veriTurnSpeed, 0.0f, 0.0f);
        transform.Rotate(0.0f, Input.GetAxis("Horizontal") * horiTurnSpeed, 0.0f, Space.World);

        float tiltAmount = Input.GetAxis("Horizontal");
        float pitchAmount = Input.GetAxis("Vertical");




        //handles tilt logic to tilt the model of the glider for visual appearance
        if (tiltAmount > 0) {
            gliderModel.transform.localRotation = Quaternion.Lerp(gliderModel.transform.localRotation, Quaternion.Euler(gliderModel.transform.localRotation.x, gliderModel.transform.localRotation.y, -45), Time.deltaTime * 2f);
        }
        if (tiltAmount < 0){
            gliderModel.transform.localRotation = Quaternion.Lerp(gliderModel.transform.localRotation, Quaternion.Euler(gliderModel.transform.localRotation.x, gliderModel.transform.localRotation.y, 45f), Time.deltaTime * 2f);
        }
#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
        if (tiltAmount == 0)
        {
#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator
            gliderModel.transform.localRotation = Quaternion.Lerp(gliderModel.transform.localRotation, Quaternion.Euler(gliderModel.transform.localRotation.x, gliderModel.transform.localRotation.y, 0.0f), Time.deltaTime * 2f);
        }

        //Speed management
        if (speed > maxSpeed){
            speed = maxSpeed;
            cameraShake.enabled = true;
        }
        else {
            cameraShake.enabled = false;
        }

        if (speed <= 0){
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(100f, transform.rotation.y, 0.0f), Time.deltaTime * 1f);
        }
        else {
            //speed -= dragAmount;
        }


	}

    /*
     * This method is called when the glider hits the land objects. Collision
     * logic not implemented, as I need to figure out the consequences
     * of hitting land objects.
     * 
     */
         
    private void OnTriggerEnter(Collider coll)
    { 
        if (coll.gameObject.tag == "Land") {
            Debug.Log("Land Hit!");
        }
    }
}
