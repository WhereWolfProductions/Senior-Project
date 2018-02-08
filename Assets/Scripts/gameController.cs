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

        Cursor.SetCursor(Resources.Load("Images/cursor") as Texture2D, Vector2.zero, CursorMode.Auto);


    }

    // Update is called once per frame
    void Update () {
        
    }


    public void loadLevelScript(Monobehavior levelScript)
    {
	//Data type   name          script
	Monobehavior Level = new levelScript();
	GameObject.AddComponent<Level>();
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


    public void spawnFab(string fabName, GameObject parent)
    {
        Instantiate(Resources.Load("fabName"), parent.transform);
    }


}
