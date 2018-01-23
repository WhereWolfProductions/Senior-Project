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



    }

	

    private void FixedUpdate()
    {


    }
}
