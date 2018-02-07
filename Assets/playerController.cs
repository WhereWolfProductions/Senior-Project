using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    Rigidbody playerRB;
    CapsuleCollider playerCollider;
    Transform cam;

    Vector3 nextPos;

    public float moveSpeed;
    float jumpPower = 15;
    float slopeClossnes;     //The distance

    bool grounded;

	// Use this for initialization
	void Start () {

        playerRB = GetComponent<Rigidbody>();
        slopeClossnes = 7;
        playerCollider = playerRB.gameObject.GetComponent<Collider>() as CapsuleCollider;
        
        Vector3 nextPos = (transform.position) + playerRB.velocity * Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {




    }

	

    private void FixedUpdate()
    {
        playerMove();
        stickToSlopes();
        checkGround();
        preventStickWalls();
        nextPos = (transform.position) + playerRB.velocity * Time.deltaTime;
    }


    void playerMove()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");

        preventSliding();

        if (horInput != 0 || vertInput != 0)
        {
            Vector3 vertDir = (transform.forward * vertInput);
            Vector3 horDir = (transform.right * horInput);

            Vector3 totalDir = (vertDir + horDir);
            totalDir = new Vector3(totalDir.x, playerRB.velocity.y, totalDir.z);
            totalDir = totalDir * moveSpeed * Time.deltaTime;
            totalDir.y = playerRB.velocity.y;

            playerRB.velocity = totalDir;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jump();
        }
    }


    void preventSliding()
    {

        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            Vector3 localVel = transform.InverseTransformDirection(playerRB.velocity);
            localVel.x = 0;
            playerRB.velocity = transform.TransformDirection(localVel);
        }
        if(Input.GetAxisRaw("Vertical") == 0)
        {
            Vector3 localVel = transform.InverseTransformDirection(playerRB.velocity);
            localVel.z = 0;
            playerRB.velocity = transform.TransformDirection(localVel);
        }

    }

    

    void stickToSlopes()
    {
        RaycastHit hitInfo = new RaycastHit();


        Vector3 adjustedPos = new Vector3(transform.position.x, transform.position.y - playerCollider.height /2, transform.position.z);
        playerRB.useGravity = true;
        //If ray hits something...
        if (Physics.Raycast(new Ray(adjustedPos, Vector3.down), out hitInfo, slopeClossnes))
        {
            if(hitInfo.collider.tag == "slope")
            {
                Debug.Log("hit");
                playerRB.position = new Vector3(playerRB.position.x, (hitInfo.point.y + ((playerCollider.height) / 2) + 0.1f), playerRB.position.z);
                playerRB.useGravity = false;

                //Prevents hopping behavior while standing still on slopes.
                if(checkMovement() == false)
                {
                    playerRB.constraints = RigidbodyConstraints.FreezePositionY;
                }
                else
                {
                    playerRB.constraints = RigidbodyConstraints.None;
                    playerRB.constraints = RigidbodyConstraints.FreezeRotation;
                }
            }
            else { playerRB.useGravity = true; }
        }
        else
        {
            playerRB.useGravity = true;
        }
        playerRB.useGravity = true;
    }

	
	
    void preventStickWalls()
    {




        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(nextPos.normalized);
            Debug.Log(transform.position.normalized + "current");
        }


	    //If the players position has not changed in the direction they intend to travel
	    if(approxVectors(transform.position, nextPos, 0.05f) == false)
	    {
		    Debug.Log("Stuck on wall");
		    /*
		    
		    //set hor velocity to 0 or .....
		
		    @@@@@@@@@@
		    
		    //Adjust y velocity (gravity) to make the player fall, y overpowers total of hor velocity by amount of gravity.
		    
		    //New y velocity that adjusts gravity to overtake left/right velocity.
		    float newYVelo = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z).magnitude - Physics.gravity;
		    
		    
		    playerRB.velocity = new Vector3(playerRB.velocity.x, newYVelo, playerRB.velocity.z)
		
		    */
	    }
	    else{  Debug.Log("Free"); }

        

    }
	
	
    //Returns true if two vecotrs are approxmetly the same on x,y,z..
	bool approxVectors(Vector3 vector1, Vector3 vector2, float epsilon)
    {
        if(Mathf.Abs(vector2.x - vector1.x) < epsilon)
        {
            if(Mathf.Abs(vector2.y - vector1.y) < epsilon)
            {
                if(Mathf.Abs(vector2.z - vector1.z) < epsilon)
                {
                    return true;
                }
            }
        }

        return false;
    }
	
	

    //Rays castes downward looking for a collider, if one is found, you are grounded, if not, grounded is false.
    void checkGround()
    {
        float rayRange = 0.9f;
        RaycastHit hitInfo = new RaycastHit();
        Vector3 rayPos = new Vector3(transform.position.x, (transform.position.y - playerCollider.height / 2) -1, transform.position.z);

        //If ray hits collider...
        if (Physics.Raycast(new Ray(rayPos, Vector3.up), out hitInfo, rayRange))
        {
            grounded = true;
        }
        else { grounded = false; }

    }


    //Returns true if player is pressing movement controls.
    bool checkMovement()
    {
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            return false;
        }
        else { return true; }
    }



    void jump()
    {
        if(grounded == true)
        {
            playerRB.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }


}
