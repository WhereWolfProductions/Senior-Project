using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringPuzzle : MonoBehaviour {

    List<GameObject> leftRings = new List<GameObject>();
    List<GameObject> middleRings = new List<GameObject>();
    List<GameObject> rightRings = new List<GameObject>();

	// Use this for initialization
	void Start () {

        findDisks();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //Finds and sorts the disks to make programming/managing the puzzle easier.
    void findDisks()
    {
        foreach (Transform child in transform.Find("Disks").Find("Left Pole"))
        {
            leftRings.Add(child.gameObject);
        }
        foreach (Transform child in transform.Find("Disks").Find("Middle Pole"))
        {
            middleRings.Add(child.gameObject);
        }
        foreach (Transform child in transform.Find("Disks").Find("Right Pole"))
        {
            rightRings.Add(child.gameObject);
        }

    }

}
