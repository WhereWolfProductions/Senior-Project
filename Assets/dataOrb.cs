using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataOrb : MonoBehaviour {

    GameObject player;

    bool pressed;

	// Use this for initialization
	void Start () {

        player = GameObject.FindWithTag("Player");
        pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (pressed == false)
        {
            gameController.gameControllerManager.GetComponent<level1>().dataFound += 1;
            Destroy(gameObject);
        }
        pressed = true;
        effectPlayer.effectPlayerData.playEffect("ding", 50);
        player.transform.Find("Main Camera").GetComponent<cameraScript>().glitch();
    }
}
