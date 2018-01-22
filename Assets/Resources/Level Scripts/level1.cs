using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class level1 : ScriptableObject {

    GameObject mainCamera;

    Coroutine fadeMusicCo;
	// Use this for initialization
	private void Awake () {
        
        //gameController.gameControllerManager.runCoroutine(introduction);
        mainCamera = Camera.main.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }



    GameObject getFade()
    {
        return Camera.main.transform.Find("FadeScreen(Clone)").gameObject;
    }


    /*
    //Controls the changing of scene, fading in and out, download bar, audio played during download
    IEnumerator introduction()
    {

        
        musicManager.musicManagerData.stopMusicRoutine(musicManager.musicManagerData.introSong);
        musicManager.musicManagerData.fadeMusicFromScriptObj(musicManager.musicManagerData.audioSource, 2);
        gameController.gameControllerManager.runCoroutine(getFade().GetComponent<fadeScript>().fadeOut);
        yield return new WaitUntil(() => Camera.main.transform.Find("FadeScreen(Clone)").Find("Image").GetComponent<Image>().color.a > 1);

        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => musicManager.musicManagerData.gameObject.GetComponent<AudioSource>().volume == 0);
        gameController.gameControllerManager.changeScene("Office");
        


        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Office");
        mainCamera = Camera.main.gameObject;
        mainCamera.GetComponent<cameraScript>().canMove = false;
        GameObject.FindWithTag("Computer").GetComponent<computerController>().clickable = false;
        GameObject fadeScreen = Instantiate(Resources.Load("FadeScreen"), Camera.main.transform) as GameObject;
        GameObject downLoadBar = Instantiate(Resources.Load("Download Bar"), fadeScreen.transform) as GameObject;

        fadeScreen.transform.Find("Image").GetComponent<Image>().color = Color.black;
        
        
        AudioSource clip1 = effectPlayer.effectPlayerData.sayClip("1", 100);
        yield return new WaitUntil(() => clip1.isPlaying == false);
        AudioSource clip2 = effectPlayer.effectPlayerData.sayClip("2", 100);
        yield return new WaitUntil(() => clip2.isPlaying == false);

        yield return new WaitForSeconds(4);
        yield return new WaitUntil(() => downLoadBar.GetComponent<ballAnimator>().activeBall == 0);
        downLoadBar.GetComponent<ballAnimator>().fadeAnim();

        yield return new WaitUntil(() => downLoadBar.GetComponent<ballAnimator>().checkFaded() == true);
        downLoadBar.GetComponent<ballAnimator>().stopAnim();
        Destroy(downLoadBar);

        Cursor.lockState = CursorLockMode.Confined;
        gameController.gameControllerManager.runCoroutine(getFade().GetComponent<fadeScript>().fadeIn);
        yield return new WaitForSeconds(5);
        mainCamera.GetComponent<cameraScript>().canMove = true;
        GameObject.FindWithTag("Computer").GetComponent<computerController>().clickable = true;


        yield return new WaitUntil(() => fadeScreen.transform.Find("Image").GetComponent<Image>().color.a == 0 
        || GameObject.FindWithTag("Computer").GetComponent<computerController>().open == true);
        fadeScreen.GetComponent<fadeScript>().stopFade();
        Destroy(fadeScreen);

        //After download screen is gone...

        gameController.gameControllerManager.runCoroutine(historyLesson);
        
    }
    */
    IEnumerator historyLesson()
    {
        GameObject computer;
        computer = GameObject.FindWithTag("Computer");
        computerController computerController;
        computerController = computer.GetComponent<computerController>();

    

        yield return new WaitUntil(() => computer.GetComponent<computerController>().open == true);
        yield return new WaitForSeconds(6); //waits for screens to fully expand, cant check coroutine done :/


        AudioSource localClip = effectPlayer.effectPlayerData.sayClip("4", 100);

        //waits until mention of nukes
        GameObject pcCanvas = GameObject.FindWithTag("PC Canvas");

        
        yield return new WaitUntil(() => localClip.time >= 15);
        computerController.addImage(pcCanvas.transform.Find("Left Canvas").gameObject, "mushroom cloud");
        computerController.addImage(pcCanvas.transform.Find("Right Canvas").gameObject, "mushroom cloud");

        //waits until previous dialogue is done before doing next, pause added.
        yield return new WaitUntil(() => localClip.isPlaying == false);

        localClip = effectPlayer.effectPlayerData.sayClip("5", 100);
        yield return new WaitForSeconds(1.5f);
        computerController.removeImage(pcCanvas.transform.Find("Left Canvas").gameObject);
        computerController.removeImage(pcCanvas.transform.Find("Right Canvas").gameObject);

        yield return new WaitUntil(() => localClip.isPlaying == false);

        localClip = effectPlayer.effectPlayerData.sayClip("6", 100);

        yield return new WaitUntil(() => localClip.time >= 10);
        
        computerController.addImage(pcCanvas.transform.Find("Left Canvas").gameObject, "engineering");
        computerController.addImage(pcCanvas.transform.Find("Right Canvas").gameObject, "handshake");

        yield return new WaitUntil(() => localClip.isPlaying == false);
        yield return new WaitForSeconds(2);

        computerController.removeImage(pcCanvas.transform.Find("Left Canvas").gameObject);
        computerController.removeImage(pcCanvas.transform.Find("Right Canvas").gameObject);

        localClip = effectPlayer.effectPlayerData.sayClip("7", 100);

        yield return new WaitUntil(() => localClip.isPlaying == false);

        localClip = effectPlayer.effectPlayerData.sayClip("8", 100);

        yield return new WaitUntil(() => localClip.isPlaying == false);
        
        computerController.removeImage(pcCanvas.transform.Find("Middle Canvas").gameObject);
        GameObject fadeObj = Instantiate(Resources.Load("Log in screen"), pcCanvas.transform.Find("Middle Canvas").Find("Panel")) as GameObject;

    }

    void logIn()
    {
        
    }

}

