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
        
        gameController.gameControllerManager.runCoroutine(introduction);
        mainCamera = Camera.main.gameObject;

    }



    GameObject getFade()
    {
        return Camera.main.transform.Find("FadeScreen(Clone)").gameObject;
    }



    //Controls the changing of scene, fading in and out, download bar, audio played during download
    IEnumerator introduction()
    {

        Cursor.lockState = CursorLockMode.Locked;
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
        GameObject fadeScreen = Instantiate(Resources.Load("FadeScreen"), Camera.main.transform) as GameObject;
        GameObject downLoadBar = Instantiate(Resources.Load("Download Bar"), fadeScreen.transform) as GameObject;

        fadeScreen.transform.Find("Image").GetComponent<Image>().color = Color.black;
        AudioSource usedSource = effectPlayer.effectPlayerData.sayClip("Intro Dialogue");
        yield return new WaitUntil(() => usedSource.isPlaying == false); //changes to when done with dialogue
        yield return new WaitUntil(() => downLoadBar.GetComponent<ballAnimator>().activeBall == 0);
        downLoadBar.GetComponent<ballAnimator>().fadeAnim();

        yield return new WaitUntil(() => downLoadBar.GetComponent<ballAnimator>().checkFaded() == true);
        downLoadBar.GetComponent<ballAnimator>().stopAnim();
        Destroy(downLoadBar);

        gameController.gameControllerManager.runCoroutine(getFade().GetComponent<fadeScript>().fadeIn);
        yield return new WaitForSeconds(2);
        mainCamera.GetComponent<cameraScript>().canMove = true;


        yield return new WaitUntil(() => fadeScreen.transform.Find("Image").GetComponent<Image>().color.a == 0);

        

    }



}

