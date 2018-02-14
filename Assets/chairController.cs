using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairController : MonoBehaviour {

    public bool seated;

    MeshCollider chairCollider;
    BoxCollider footCollider;

	// Use this for initialization
	void Start () {

        chairCollider = GetComponent<MeshCollider>();
        footCollider = GetComponent<BoxCollider>();

	}
	
	// Update is called once per frame
	void Update () {
		
        if(seated == true)
        {
            chairCollider.enabled = false;
            footCollider.enabled = false;
        }
        else
        {
            chairCollider.enabled = true;
            footCollider.enabled = true;
        }

	}
}
