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
	}
	
	// Update is called once per frame
	void Update () {

	float horInput = Input.GetAxis("Horizontal");
	float vertInput = Input.GetAxis("Vertical");
	
	if(horInput != 0 && vertInput != 0)
	{
		//Might try not including transform.position, may not need the if.
		moveDirection = new Vector3(transform.position.x + (horInput * moveSpeed), transform.position.y, transform.position.z + (vertInput * moveSpeed));
	}

    }

	

    private void FixedUpdate()
    {
	
	    playerRB.AddForce(moveDirection);

    }
}
