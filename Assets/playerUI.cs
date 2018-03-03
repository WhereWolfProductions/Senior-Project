using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerUI : MonoBehaviour {

    Text date;
    Text assignments;


	// Use this for initialization
	void Start () {

        date = transform.Find("Right Corner").Find("Date").Find("Text").GetComponent<Text>();
        assignments = transform.Find("Left Corner").Find("Assignments").Find("Text").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update () {

	}


    //essentially an animation.
    public IEnumerator setActive()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        gameObject.SetActive(true);

    }

    

    //    mm/dd//yyyy
    public void setDate(string newDate)
    {
        if(date == null)
        {
            date = transform.Find("Right Corner").Find("Date").Find("Text").GetComponent<Text>();
        }
        date.text = newDate;
    }


    public void setAssignment(string newAssignment)
    {
        if(assignments == null)
        {
            assignments = transform.Find("Left Corner").Find("Assignments").Find("Text").GetComponent<Text>();
        }
        assignments.text = "\n" + "- " + newAssignment;
    }

    

}
