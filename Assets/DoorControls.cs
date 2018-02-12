using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : MonoBehaviour {
		
	public bool unlocked;  //The state the door is in
    public GameObject door;
	
	Animator doorAnimController;
	
	Transform playerPos;
	
	// Use this for initialization
	void Start () {
		
		//Assures that if I forget to set door state, it will be defaultly unopenable.
		if(unlocked == null)
		{
			unlocked = false;
		}
		
		doorAnimController = door.GetComponent<Animator>();
		playerPos = GameObject.FindWithTag("Player").transform;
        canvasReflectState();
	}
	
	// Update is called once per frame
	void Update () {


    
	}

    private void OnMouseDown()
    {
        Debug.Log("ji");
        if (inRange() == true)
        {
            clickedButton();
            canvasReflectState();
        }
    }



    void clickedButton()
	{
		
		//If unlocked(green) and closed...
		if(unlocked == true && doorAnimController.GetBool("opened") == false)
		{
			doorAnimController.SetBool("opened", true);
		}
		//unlocked(green) and open
		else if(unlocked == true && doorAnimController.GetBool("opened") == true)
		{
            doorAnimController.SetBool("opened", false);
		}
		//locked(red) in any state
		else if (unlocked == false)
		{
			//play locked anim.
		}
		
		//run function with while loop to make button uninteractalbe until anim is done playing.
		
	}
	
	
	void autoClose()
	{
		//If distance between door and player gets big enough, the door will set itself to clsoed, thus closing.
		if(Vector3.Distance(transform.position, playerPos.position) > 4)
		{
			doorAnimController.SetBool("opened", false);
		}
	}
	
	
	//Sets what picture the button should show, locked or unlocked.
	void setButtons()
	{
		if(unlocked == true)
		{
			
		}
		else if (unlocked == false)
		{
			
		}
	}


    //prevents player from pressing buttons from an infinate distance
    bool inRange()
    {
        Debug.Log(Vector3.Distance(transform.position, playerPos.position));
        if (Vector3.Distance(transform.position, playerPos.position) > 3)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    
    //The screen of the button will say locked if door is locked, unlocked if unlocked.
    void canvasReflectState()
    {
        if(unlocked == true)
        {
            transform.Find("Locked Canvas").gameObject.SetActive(false);
            transform.Find("Unlocked Canvas").gameObject.SetActive(true);
        }
        else if(unlocked == false)
        {
            transform.Find("Locked Canvas").gameObject.SetActive(true);
            transform.Find("Unlocked Canvas").gameObject.SetActive(false);
        }
    }


	
	//Developer functions, non-player controlled.
	void unlockDoor()
	{
		unlocked = true;
		setButtons();
	}
	
	void lockDoor()
	{
		unlocked = false;
		setButtons();
	}
	
	void openDoor(GameObject target)
	{
		target.GetComponent<Animator>().SetBool("opened", true);
	}
	
	
}
