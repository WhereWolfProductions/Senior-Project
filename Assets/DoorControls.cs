using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : MonoBehaviour {
		
	bool locked;  //The state the door is in
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
		
		//If unlocked(green) and closed...
		if(locked == false && open == false)
		{
			doorAnimController.SetBool("opened", true);
		}
		//unlocked(green) and open
		else if(locked == false && open == true)
		{
			doorAnimController.SetBool("opened", false)
		}
		//locked(red) in any state
		else if (locked == true)
		{
			//play locked anim.
		}
		
		//run function with while loop to make button uninteractalbe until anim is done playing.
		
	}
	
	
	void autoClose()
	{
		//If distance between door and player gets big enough, the door will set itself to clsoed, thus closing.
		if(Vector3.Distance(transform.position, playerPos) > 4)
		{
			doorAnimController.SetBool("opened", false);
		}
	}
}
