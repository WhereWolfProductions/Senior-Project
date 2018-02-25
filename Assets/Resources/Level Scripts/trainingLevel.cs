using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class trainingLevel : MonoBehaviour {

    GameObject player;
    textController textDriver;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        textDriver = player.transform.Find("uiText").GetComponent<textController>();

        player.transform.Find("FadeScreen").GetComponent<fadeScript>().setFade(255);
        StartCoroutine(player.transform.Find("FadeScreen").GetComponent<fadeScript>().fadeIn());

        StartCoroutine(levelMain());
        effectPlayer.effectPlayerData.loadClips("Ai Dialogue/Lvl 1/Training Level");
    }


    //main function controls sequence of function calls
    IEnumerator levelMain()
    {
        firstLong();
        yield return new WaitUntil(() => player.transform.position.z < -44);
        turnSection();
        yield return new WaitUntil(() => player.transform.position.x > -43);
        puzzle();
        GameObject puzzleObj = GameObject.Find("Stacking game");
        yield return new WaitUntil(() => puzzleObj.GetComponent<ringPuzzle>().done == true);
        puzzleObj.GetComponent<BoxCollider>().enabled = false; // removes invisable wall.
        jumpyBit();
    }

    //What directions are given during the first long part of the map.
    void firstLong()
    {
        textDriver.setText("Use W to move forward, A to move left, S to move back, and D to move right.");
        effectPlayer.effectPlayerData.sayClip("First Long", 100);
    }


    void turnSection()
    {
        textDriver.setText("Press Space to jump over the gap.");
        effectPlayer.effectPlayerData.sayClip("Turn", 100);
    }
  

    void puzzle()
    {
        effectPlayer.effectPlayerData.sayClip("puzzle", 100);
        textDriver.setText("Left click to interact with the world. Move the rings from the left most pole to the right most. A larger ring cannot go ontop of a smaller ring.");
    }

    void jumpyBit()
    {
        effectPlayer.effectPlayerData.sayClip("jumpy", 100);
        textDriver.setText("Get up to the green button");
    }

}

