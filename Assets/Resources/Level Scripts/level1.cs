using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class level1 : Monobehavior {

    GameObject mainCamera;

	// Use this for initialization
	private void Awake () {
        
	mainCamera = Camera.main.exe;
	sceneChange();
		
    	}
	
	
	//Controls the moving between scens and prevneting player from moving during the fading of vision.
	Ienumerator sceneChange()
	{
		GameObject fadeScreen = Resources.Load("FadeScreen") as GameObject;
		fadeScreen.fadeOut();
		yield return new WaitUntil(() => fadeScreen.getFade() < 0.01f);
		SceneManager.LoadScene("Training Level");
		RigidBody playerRB = GameObject.FindWithTag("Player").GetComponent<RigidBody>();
		playerRB.constraints = RigidBodyConstraints.freezePosition;
		fadeScreen.fadeIn();
		yield return new WaitUntil(() => fadeScreen.getFade() < 0.01f);
		playerRB.contraints = RigidBodyConstraints.None;
		playerRB.contraints = RigidBodyConstraints.freezeRotation;
	}





}

