using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    Rigidbody playerRB;
    Transform cam;

    float moveSpeed;

	// Use this for initialization
	void Start () {

        playerRB = GetComponent<Rigidbody>();
        moveSpeed = 7;
	}
	
	// Update is called once per frame
	void Update () {


	


    }

	

    private void FixedUpdate()
    {
        //Try addForce(dir, ForceMode.Impulse)
        // Also try ForceMode.Veloicty
        float horInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");

        

        Vector3 vertDir = (transform.forward * vertInput);
        Vector3 horDir = (transform.right * horInput);

        Vector3 totalDir = (vertDir + horDir);

        playerRB.MovePosition(transform.position + (totalDir * Time.deltaTime * moveSpeed));



    }
}
