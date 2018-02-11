using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringPuzzle : MonoBehaviour {

    List<GameObject> leftRings = new List<GameObject>();
    List<GameObject> middleRings = new List<GameObject>();
    List<GameObject> rightRings = new List<GameObject>();

    //Saves positions for all rings becuase setting y values breaks unity
    List<Vector3> leftPos = new List<Vector3>();
    List<Vector3> middlePos = new List<Vector3>();
    List<Vector3> rightPos = new List<Vector3>();


    public GameObject currentRing;

    public Material defaultMat;

	// Use this for initialization
	void Start () {

        findDisks();
        fillVectors();


        Debug.Log(leftRings[0].transform.Find("Torus").GetComponent<Renderer>().material);
        defaultMat = new Material(leftRings[0].transform.Find("Torus").GetComponent<Renderer>().material);
    }
	
	// Update is called once per frame
	void Update () {

        if (currentRing != null)
        {
            currentRing.transform.Find("Torus").GetComponent<Renderer>().material.color = Color.green;
        }


	}




    //fills list of vectors of all rings, largest to smallest
    void fillVectors()
    {
        foreach(GameObject ring in leftRings)
        {
            leftPos.Add(ring.transform.position);
        }
        foreach (GameObject ring in middleRings)
        {
            middlePos.Add(ring.transform.position);
        }
        foreach (GameObject ring in rightRings)
        {
            rightPos.Add(ring.transform.position);
        }
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
    
    
    //Set position of newRing to findNewPos
    //Fix value system, allows for med to be placed above small for some reason.


    public void moveRing(GameObject pole)
    {
        GameObject newRing = findSameRing(pole);
        Debug.Log(currentRing);

        if (preventSamePole(pole) == true)
        {
            newRing.transform.position = findNewPos(pole);
            newRing.SetActive(true);
            currentRing.SetActive(false);
        }

        newRing.transform.Find("Torus").GetComponent<Renderer>().material = defaultMat;
        currentRing.transform.Find("Torus").GetComponent<Renderer>().material = defaultMat;
        currentRing = null;
    }


    bool preventSamePole(GameObject pole)
    {

        if (currentRing.transform.parent.name == pole.name)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //Finds the next position to place a ring.
    Vector3 findNewPos(GameObject pole)
    {
        int amountOfRings = countActive(pole);

        if(amountOfRings == 0)
        {
            return findVector(pole)[0];
        }
        else if(amountOfRings == 1)
        {
            return findVector(pole)[1];
        }
        else if(amountOfRings == 2)
        {
            return findVector(pole)[2];
        }
        else
        {
            //return 0;
        }
        return Vector3.down;
    }


    List<Vector3> findVector(GameObject pole)
    {
        if(pole.name == "Left Pole")
        {
            return leftPos;
        }
        else if (pole.name == "Middle Pole")
        {
            return middlePos;
        }
        else if (pole.name == "Right Pole")
        {
            return rightPos;
        }
        else
        {
            return null;
        }
    }

    int countActive(GameObject pole)
    {
        int activeAmount = 0;
        foreach(Transform child in pole.transform)
        {
            if(child.gameObject.activeSelf == true)
            {
                activeAmount = activeAmount + 1;
            }
        }
        return activeAmount;
    }


    public void setCurrentRing(GameObject pole)
    {
        currentRing = findSmallest(pole);

    }


    //Compares current ring to the smallest active ring on the selected pole, returns true if valid.
    public bool compareRings(GameObject smallestRing)
    {
        int currentValue = 0;
        int seletecValue = 0;

        //Finds value of current ring
        int counter = 0;
        foreach (GameObject ring in leftRings)
        {
            if(currentRing.name == leftRings[counter].name)
            {
                currentValue = counter;
            }
            counter = counter + 1;
        }


        counter = 0;
        foreach (GameObject ring in leftRings)
        {
            if (smallestRing.name == leftRings[counter].name)
            {
                seletecValue = counter;
            }
            counter = counter + 1;
        }

        if(currentValue == seletecValue)
        {
            return true;
        }
        else if(currentValue > seletecValue)
        {
            return true;
        }
        else { return false; }
    }

    //Finds smallest ring that is active to move, if none found defaults to largest ring
    public GameObject findSmallest(GameObject pole)
    {
        
        if (pole.name == "Left Pole")
        {
            for (int i = 2; i > 0; i--)
            {

                if(leftRings[i].activeSelf == true)
                {
                    return leftRings[i];
                }
            }
            return leftRings[0];
        }
        else if(pole.name == "Middle Pole")
        {
            for (int i = 2; i > 0; i--)
            {
                if (middleRings[i].activeSelf == true)
                {
                    Debug.Log(middleRings[i]);
                    return middleRings[i];
                }
            }
            return middleRings[0];
        }
        else if(pole.name == "Right Pole")
        {
            for (int i = 2; i > 0; i--)
            {
                if (rightRings[i].activeSelf == true)
                {
                    return rightRings[i];
                }
            }
            return rightRings[0];
        }
        else
        {
            Debug.Log("none found");
            return null;
        }
    }


    //Finds same ring as given to enable it and move it to smallestRing.
    GameObject findSameRing(GameObject pole)
    {
        foreach(Transform child in pole.transform)
        {
            if(currentRing.name == child.name)
            {
                return child.gameObject;
            }
        }
        return null;
    }

}
