using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairController : MonoBehaviour {

    GameObject player;

    bool seated;

    MeshCollider chairCollider;
    BoxCollider footCollider;

	// Use this for initialization
	void Start () {

        chairCollider = GetComponent<MeshCollider>();
        footCollider = GetComponent<BoxCollider>();

        player = GameObject.FindWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {

        player.GetComponent<playerController>().seated = seated;

        if(seated == true)
        {
            chairCollider.enabled = false;
            footCollider.enabled = false;
        }
        else
        {
            chairCollider.enabled = true;
            footCollider.enabled = true;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
        }

	}

    private void OnMouseDown()
    {
        seated = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        player.transform.position = new Vector3 (transform.position.x, transform.position.y + 3, transform.position.z);
        player.GetComponent<playerController>().seated = true;
    }


    

}
