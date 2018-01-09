using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class buttonExtension : MonoBehaviour {


    //Goes onto canavs to add usefull functions for buttons to call.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    

}
