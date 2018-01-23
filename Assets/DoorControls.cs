using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : MonoBehaviour {
	
	public bool locked;  //Used to set some doors to be unopenable, this is used with doors to nowhere.
	bool open;
	
	Animator doorAnimController;
	
	Vector3 playerPos;
	
	// Use this for initialization
	void Start () {
		
		//Assures that if I forget to set door state, it will be defaultly unopenable.
		if(locked == null)
		{
			locked = false;
		}
		
		doorAnimController = gameObject.GetComponent<Animator>();
		playerPos = GameObject.FindWithTag("Player").transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void clickedButton()
	{
		//If unlocked and closed...
		if(locked == false && open == false)
		{
			doorAnimController.SetBool("opened", true);
		}
	}
	
	
	void autoClose()
	{
		if(Vector3.Distance(transform.position, playerPos) > 4)
		{
			doorAnimController.SetBool("opened", false);
		}
		
	}
}
