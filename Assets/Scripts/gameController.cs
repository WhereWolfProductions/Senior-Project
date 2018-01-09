using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class gameController : MonoBehaviour {

    public static gameController gameControllerManager;
    
    


	// Use this for initialization
	void Awake () {

        if (!gameControllerManager)
        {
            gameControllerManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }


    //Begins the transition to level 1.
    public void pressedStart()
    {

        GameObject temp = Instantiate(Resources.Load("FadeScreen") as GameObject, Camera.main.transform);
        ScriptableObject.CreateInstance<level1>();


    }









    public void runCoroutine(System.Func<IEnumerator> routine)
    {
        StartCoroutine(routine());
    }

    public void stoproutine(Coroutine routine)
    {
        StopCoroutine(routine);
    }


    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }



}
