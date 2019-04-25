using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Vector3 newCamPos = transform.position - transform.forward * 2.0f + Vector3.up * 1.0f;

        float cameraOffset = 0.91f;
        chaseCam.position = chaseCam.position * cameraOffset + newCamPos*(1.0f-cameraOffset);
        chaseCam.LookAt(transform.position + transform.forward * 20.0f);

        transform.position += transform.forward * Time.deltaTime * speed * speedMultiplier;

        //speed decreases if you're tipped up above 90
        speed -= transform.forward.y * speedIncreaseMultiplier;

        transform.Rotate(Input.GetAxis("Vertical") * veriTurnSpeed, 0.0f, 0.0f);
        transform.Rotate(0.0f, Input.GetAxis("Horizontal") * horiTurnSpeed, 0.0f, Space.World);

        float tiltAmount = Input.GetAxis("Horizontal");
        float pitchAmount = Input.GetAxis("Vertical");





        if (tiltAmount > 0) {
            gliderModel.transform.localRotation = Quaternion.Lerp(gliderModel.transform.localRotation, Quaternion.Euler(gliderModel.transform.localRotation.x, gliderModel.transform.localRotation.y, -45), Time.deltaTime * 2f);
        }
        if (tiltAmount < 0){
            gliderModel.transform.localRotation = Quaternion.Lerp(gliderModel.transform.localRotation, Quaternion.Euler(gliderModel.transform.localRotation.x, gliderModel.transform.localRotation.y, 45f), Time.deltaTime * 2f);
        }
        if (tiltAmount == 0){
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

    private void OnTriggerEnter(Collider collider)
    { 
        if (collider.gameObject.tag == "Land") {
            Debug.Log("Land Hit!");
        }
    }
}
