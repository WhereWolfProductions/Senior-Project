using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    float sensitivity = 50;
    float yRotate = 0;

    public bool canMove = true;

	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;

	}
	
	// Update is called once per frame
	void Update () {

        if (canMove == true) { cameraMove(); }
	}





    //Camera Movement
    void cameraMove()
    {
        float horAim = Input.GetAxis("Mouse X");

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + (sensitivity * Time.deltaTime) * (horAim),
            transform.localEulerAngles.z);

        yRotate += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        yRotate = Mathf.Clamp(yRotate, -90, 90);


        Camera.main.transform.localEulerAngles = new Vector3(-yRotate, Camera.main.transform.localEulerAngles.y,
            Camera.main.transform.localEulerAngles.z);
    }

}
