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




    }

	
        void setRotation()
	{
		//Possible way to make two objects have same y rotation.
		transfrom.localEulerAngles = new Vectro3(transform.localEulerAngles.x, transform.localEulerAngles.y  - ( (transform.localEulerAngles.y - transform.localEulerAngles.y) + cam.localEulerAngles.y), transform.lcoalEulerAngles.z)
	}

    private void FixedUpdate()
    {


    }
}
