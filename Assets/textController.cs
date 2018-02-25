using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class textController : MonoBehaviour {

    Text uiText;

	// Use this for initialization
	void Start () {

        uiText = transform.Find("Text").GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setText(string newText)
    {
        uiText.text = newText;
    }
}
