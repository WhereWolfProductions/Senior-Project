using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logInButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void logIn()
    {
        Debug.Log("Button");
        GameObject fadeObj = Instantiate(Resources.Load("FadeScreen"), Camera.main.transform) as GameObject;
        StartCoroutine(fadeObj.GetComponent<fadeScript>().fadeOut());
    }
}
