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


    IEnumerator startRoutine()
    {
        //Every thing up to fading out the camera after spawning into office
        GameObject fadeScreen = Instantiate(Resources.Load("FadeScreen")) as GameObject;
        StartCoroutine(fadeScreen.GetComponent<fadeScript>().fadeOut());
        musicManager.musicManagerData.stopMusic();

        yield return new WaitUntil(() => fadeScreen.GetComponent<fadeScript>().getFade() > 0.9f);
        changeScene("Office Better");
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Office Better");
        GameObject.FindWithTag("Player").GetComponent<playerController>().UI.SetActive(false);
        GameObject chair = GameObject.Find("Chair");
        chair.GetComponent<chairController>().seated = true;

        GameObject.FindWithTag("Player").transform.position = new Vector3(-21.572f, 3, 20);
        GameObject.FindWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        GameObject newFadeScreen = Instantiate(Resources.Load("FadeScreen"), GameObject.FindWithTag("Player").transform) as GameObject;
        StartCoroutine(newFadeScreen.GetComponent<fadeScript>().fadeIn());

        //Audio and transition to training level.
        
        AudioSource dialogueSource = effectPlayer.effectPlayerData.sayClip("Part 1", 100);
        yield return new WaitUntil(() => dialogueSource.time > 13.5);
        dialogueSource.Pause();
        yield return new WaitForSeconds(1.2f);
        dialogueSource.UnPause();
        StartCoroutine(GameObject.FindWithTag("Player").GetComponent<playerController>().UI.GetComponent<playerUI>().setActive());
        GameObject.FindWithTag("UI").GetComponent<playerUI>().setAssignment("Recalibrate neural implant.");
        yield return new WaitUntil(() => dialogueSource.isPlaying == false);

        //implant initation
        
        dialogueSource = effectPlayer.effectPlayerData.sayClip("Part 2", 100);
        yield return new WaitUntil(() => dialogueSource.isPlaying == false);
        GameObject calibrate = Instantiate(Resources.Load("Calibrating Canvas"), transform) as GameObject;
        for(int i = 0; i < 4; i++)
        {
            AudioSource used = effectPlayer.effectPlayerData.playEffect("beeping", 30);
            yield return new WaitUntil(() => used.isPlaying == false);
        }

        Destroy(calibrate);
        
        dialogueSource = effectPlayer.effectPlayerData.sayClip("Part 3", 100);
        yield return new WaitUntil(() => dialogueSource.isPlaying == false);
        StartCoroutine(GameObject.FindWithTag("Player").transform.Find("Main Camera").GetComponent<cameraScript>().diveEffect());
        yield return new WaitUntil(() => GameObject.FindWithTag("Player").transform.Find("Main Camera").GetComponent<cameraScript>().diving == false);
        SceneManager.LoadScene("Tutorial Level");
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Tutorial Level");
        setLevel(typeof(trainingLevel));

    }


    public void startGame()
    {
        StartCoroutine(startRoutine());
    }




    //Takes a level class and adds it to gameController, deleting the last level
    //Prevents multiple levels being active at one time.

    //Example of use: setLevel(typeof(level1));
    public void setLevel(System.Type levelClass)
    {

        //If current level is being used, removes level and resets it to new one
        if (currentLevel != null)
        {
            Destroy(currentLevel);
            currentLevel = gameObject.AddComponent(levelClass);
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
