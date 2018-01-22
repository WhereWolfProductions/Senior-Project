using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    Vector3 moveDirection;
    Rigidbody playerRB;
    Transform cam;

    float moveSpeed;

	// Use this for initialization
	void Start () {

        playerRB = GetComponent<Rigidbody>();
        moveSpeed = 10;
        cam = transform.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {



        //transform.rotation = Quaternion.Slerp(transform.rotation, cam.rotation, Time.deltaTime * 100000);

        //euler angles gives best result, but if you only set y, it fails to work

        Vector3 eulerRot = new Vector3(cam.localEulerAngles.x, cam.eulerAngles.y, cam.eulerAngles.z);

        transform.rotation = Quaternion.Euler(eulerRot);

    }



    private void FixedUpdate()
    {


    }
}
