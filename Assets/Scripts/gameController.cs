using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class gameController : MonoBehaviour {

    public static gameController gameControllerManager;

    Component currentLevel;


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


    public void startGame()
    {
        musicManager.musicManagerData.stopMusic();
        changeScene("Office Better");
    }





    //Takes a level class and adds it to gameController, deleting the last level
    //Prevents multiple levels being active at one time.

    //Example of use: setLevel(typeof(level1));
    void setLevel(System.Type levelClass)
    {

        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        else
        {
            currentLevel = gameObject.AddComponent(levelClass);
        }

    }


    void clearExtra()
    {
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        if(scripts.Length >  2)
        {
            foreach(Component script in gameObject.GetComponents<MonoBehaviour>())
            {
                Debug.Log(script.GetType());
                if (script.GetType() != gameObject.GetComponent<gameController>().GetType())
                {
                    Destroy(script);
                }
            }
        }
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
