using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class level1 : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {

        StartCoroutine(checkRealStart());
	}


    IEnumerator checkRealStart()
    {

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Office Better");
        player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(-7.86f, 2.53f, -7.49f);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
