using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class musicManager : MonoBehaviour {

    public static musicManager musicManagerData;

    public AudioSource audioSource;

    //Variables that are used for the volumes
    public float musicVol = 100; //default volume is 1.


    public IEnumerator introSong;

    //Songs
    AudioClip Acord;
    AudioClip Bach;

    void loadSongs()
    {
        Acord = Resources.Load("Music/Acord") as AudioClip;
        Bach = Resources.Load("Music/Bach") as AudioClip;
    }

    // Use this for initialization
    void Awake () {

        

        if (!musicManagerData)
        {
            musicManagerData = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        audioSource = gameObject.GetComponent<AudioSource>();
        musicVol = Mathf.Clamp(musicVol, 0, 100);

        loadSongs();
        StartCoroutine(checkTitle());
        
    }
	
	// Update is called once per frame
	void Update () {


    }


    //@@@@@@@@@@@@@@@@@@@@@@@
    //USEFULL FUNCTIONS

    //A public function buttons can use, or the game controller when entering a new scene.
    public void playSong(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.Play();
    }

    public void stopMusic()
    {
        audioSource.Stop();
    }

    public float getVolume()
    {
        return musicVol;
    }


    public void setVolume(float value)
    {
        musicVol = value;
    }

    //@@@@@@@@@@@@@@@@@@@@@@@


    IEnumerator checkTitle()
    {
        
        if(SceneManager.GetActiveScene().name == "Title")
        {
            introSong = songIntro();
            StartCoroutine(introSong);
            audioSource.clip = Bach;
            audioSource.Play();
            yield return new WaitForSeconds(Bach.length);
            StartCoroutine(checkTitle());
        }
    }



    IEnumerator songTransition()
    {

        while(audioSource.volume > 0)
        {
            audioSource.volume = audioSource.volume - 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        while(audioSource.volume < musicVol)
        {
            audioSource.volume = audioSource.volume + 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        //ensures accuracy of volume.
        audioSource.volume = musicVol;

    }



    //A soft introduction to a song
    IEnumerator songIntro()
    {
        float localVol = musicVol - (musicVol * .8f);
        float targetVolume = Mathf.Clamp(localVol, 0, musicVol);

        float rate = (targetVolume * 11f) / 100;

        while (audioSource.volume < targetVolume)
        {
            localVol = Mathf.Clamp(localVol + rate, 0, musicVol);
            audioSource.volume = Mathf.Clamp(localVol, 0, musicVol) / 100;
            yield return new WaitForSeconds(0.05f);
        }
        
    }





    //totally fades out music, to 0.
    public IEnumerator fadeMusic(AudioSource target, float fadeTime)
    {
        float diff = target.volume - 0;
        float changePerSec = diff / fadeTime;
        float rate = changePerSec / 100;
        float waitInterval = 1 / 100;

        while (target.volume > 0.01f)
        {
            target.volume = target.volume - rate;

            yield return new WaitForSeconds(waitInterval);
        }
        target.volume = 0;

        target.clip = null;
        yield return null;
    }


    public void stopMusicRoutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }



}
