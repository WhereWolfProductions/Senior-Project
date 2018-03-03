using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class trainingLevel : MonoBehaviour {

    GameObject player;
    playerUI UI;

    Vector3 startPos;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        startPos = player.transform.position;
        StartCoroutine(player.GetComponent<playerController>().UI.GetComponent<playerUI>().setActive());
        UI = player.GetComponent<playerController>().UI.GetComponent<playerUI>();

        GameObject fadeScreen = Instantiate(Resources.Load("FadeScreen"), player.transform) as GameObject;
        fadeScreen.GetComponent<fadeScript>().setFade(255);
        StartCoroutine(fadeScreen.GetComponent<fadeScript>().fadeIn());

        StartCoroutine(levelMain());
        effectPlayer.effectPlayerData.loadClips("Ai Dialogue/Lvl 1/Training Level");
        effectPlayer.effectPlayerData.sayClip("long", 100);

    }

    private void Update()
    {
        if(player.transform.position.y < -5)
        {
            player.transform.position = startPos;
        }
    }

    //main function controls sequence of function calls
    IEnumerator levelMain()
    {
        firstLong();
        yield return new WaitUntil(() => player.transform.position.z < -44);
        GameObject.Find("Marker").SetActive(false);
        turnSection();
        yield return new WaitUntil(() => player.transform.position.x > -43);
        puzzle();
        GameObject puzzleObj = GameObject.Find("Stacking game");
        yield return new WaitUntil(() => puzzleObj.GetComponent<ringPuzzle>().done == true);
        puzzleObj.GetComponent<BoxCollider>().enabled = false; // removes invisable wall.
        jumpyBit();
        GameObject.Find("Door Button").transform.Find("Canvas").Find("Button").GetComponent<Button>().onClick.AddListener(delegate { buttonPressed(); }); //allows for dynamic onClick event
    }

    //What directions are given during the first long part of the map.
    void firstLong()
    {
        UI.setAssignment("Use W to move forward, A to move left, S to move back, and D to move right." + "\n\n" + "- Move to the marker.");
        effectPlayer.effectPlayerData.sayClip("long", 100);
    }


    void turnSection()
    {
        UI.setAssignment("Press Space to jump over the gap.");
        effectPlayer.effectPlayerData.sayClip("Turn", 100);
    }
  

    void puzzle()
    {
        effectPlayer.effectPlayerData.sayClip("puzzle", 100);
        UI.setAssignment("Left click to interact with the world. Move the rings from the left most pole to the right most. A larger ring cannot go ontop of a smaller ring.");
    }

    void jumpyBit()
    {
        effectPlayer.effectPlayerData.sayClip("jumpy", 100);
        UI.setAssignment("Get up to the green button.");
    }



    //Once green button is pressed, go back to office.
    public void buttonPressed()
    {
        Debug.Log("Done");
    }
}

