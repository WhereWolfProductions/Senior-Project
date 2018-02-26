using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class tutorialButton : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.FindWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnMouseDown()
    {
        player.transform.Find("uiText").gameObject.SetActive(false);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.transform.Find("FadeScreen").GetComponent<fadeScript>().fadeOut();
        StartCoroutine(waitForFade(player.transform.Find("FadeScreen").GetComponent<fadeScript>()));

    }

    IEnumerator waitForFade(fadeScript fadeController)
    {
        StartCoroutine(fadeController.fadeOut());
        yield return new WaitUntil(() => fadeController.getFade() > 0.9f);

        //load new level script...
        gameController.gameControllerManager.setLevel(typeof(level1));
        SceneManager.LoadScene("Office Better");

       
    }

}
