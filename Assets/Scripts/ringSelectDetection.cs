using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringSelectDetection : MonoBehaviour {

    ringPuzzle puzzleMaster;

    private void Start()
    {
        puzzleMaster = transform.parent.parent.GetComponent<ringPuzzle>();
    }


    private void OnMouseDown()
    {

        foreach(Transform ring in transform)
        {
            if(ring != puzzleMaster.currentRing)
            {
                ring.Find("Torus").GetComponent<Renderer>().material = puzzleMaster.defaultMat;
            }
        }

        if (puzzleMaster.currentRing == null && puzzleMaster.findSmallest(gameObject).activeSelf == true)
        {
            puzzleMaster.setCurrentRing(this.gameObject);
        }
        else if(puzzleMaster.currentRing != null)
        {
            //If seleted ring is smaller than next ring, move it.
            if(puzzleMaster.compareRings(puzzleMaster.findSmallest(gameObject)) == true)
            {
                puzzleMaster.moveRing(gameObject);
            }
            else
            {
                puzzleMaster.currentRing = null;
            }
        }
    }



}
