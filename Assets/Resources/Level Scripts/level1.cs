using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class level1 : Monobehavior {

    //This level occures after the training level is done.


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

    GameObject player;

    public int dataFound;

	// Use this for initialization
	void Start () {


        StartCoroutine(checkRealStart());
        dataFound = 0;
	}


    IEnumerator checkRealStart()
    {

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Office Better");
        player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(-7.86f, 2.53f, -7.49f);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);

        player.GetComponent<playerController>().UI.GetComponent<playerUI>().setDate("01 / 05 / 2184");
        player.GetComponent<playerController>().UI.GetComponent<playerUI>().setAssignment("Enter your office to start your work day.");

        yield return new WaitUntil(() => player.GetComponent<playerController>().seated == true);

        StartCoroutine(player.transform.Find("Main Camera").GetComponent<cameraScript>().diveEffect());
        yield return new WaitUntil(() => player.transform.Find("Main Camera").GetComponent<cameraScript>().diving == false);

        SceneManager.LoadScene("Level 1");

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Level 1");

        GameObject fadeScreen = Instantiate(Resources.Load("FadeScreen") as GameObject);
        StartCoroutine(fadeScreen.GetComponent<fadeScript>().fadeIn());

        StartCoroutine(begineLevel());

        player = GameObject.FindWithTag("Player");

        yield return new WaitUntil(() => fadeScreen.GetComponent<fadeScript>().getFade() < 1);

        effectPlayer.effectPlayerData.sayClip("Assignment", 100);
    }
	
	// Update is called once per frame
	void Update () {
        
	}


    IEnumerator begineLevel()
    {
        Debug.Log("start");
        //Dialogue that says stuff. Talks about empoyer Avion wanting you to recover a corruputed file, which was created by the rogue AI, this is how you get infected.

        GameObject.FindWithTag("Player").GetComponent<playerController>().UI.GetComponent<playerUI>().setAssignment("Collect all the data orbs to fix the corrupted Avion file.");

        yield return new WaitUntil(() => dataFound == 1);

        StartCoroutine(GameObject.FindWithTag("Player").transform.Find("Main Camera").GetComponent<cameraScript>().glitch());

        yield return new WaitUntil(() => dataFound == 2);

        StartCoroutine(GameObject.FindWithTag("Player").transform.Find("Main Camera").GetComponent<cameraScript>().glitch());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(GameObject.FindWithTag("Player").transform.Find("Main Camera").GetComponent<cameraScript>().glitch());

        player.GetComponent<playerController>().UI.GetComponent<playerUI>().setAssignment("Exit the deep.");

        yield return new WaitForSeconds(2);

        AudioSource secondOrb = effectPlayer.effectPlayerData.sayClip("second orb", 100);
        yield return new WaitUntil(() => secondOrb.isPlaying == false);

        StartCoroutine(GameObject.FindWithTag("Player").transform.Find("Main Camera").GetComponent<cameraScript>().diveEffect());
    }

}
