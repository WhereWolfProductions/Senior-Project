using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectPlayer : MonoBehaviour {

    public static effectPlayer effectPlayerData;

    AudioSource audioSource;

    //Variables that are used for the volumes
    public float effectVol = 100;



    List<AudioClip> clips = new List<AudioClip>();
    AudioClip[] SFXList;

    AudioSource[] sources;

    //Effect Clips
    AudioClip flick;


    

    public void loadClips(string path)
    {
        foreach(AudioClip clip in Resources.LoadAll<AudioClip>(path))
        {
            clips.Add(clip);
        }
    }


	// Use this for initialization
	void Awake () {

        if (!effectPlayerData)
        {
            effectPlayerData = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);



        sources = gameObject.GetComponents<AudioSource>();


        effectVol = gameObject.GetComponent<AudioSource>().volume;

        loadClips("Ai Dialogue/Lvl 1/Intro Dialogue");
        loadClips("Ai Dialogue/first level");
        SFXList = Resources.LoadAll<AudioClip>("SFX");

        effectVol = 100;
	}
	
	// Update is called once per frame
	void Update () {

        foreach(AudioSource audioSource in sources)
        {
            if(audioSource.isPlaying == false)
            {
                audioSource.clip = null;
            }
        }

	}



    //Method used to play any dialogue Clip, at a certain volume that cant exceed the set effect volume.
    public AudioSource sayClip(string clipName, float volume)
    {

        AudioSource used = findFreeSource();

        foreach (AudioClip clip in clips)
        {
            if(clip.name == clipName)
            {

                if (volume <= effectVol)
                {
                    used.volume = volume;
                }
                else { used.volume = effectVol; }

                used.clip = clip;
                used.Play();
                return used;
            }

            
        }

        return null;
    }


    //Play effect, allows for volume, but will not exceed set effectVol
    public AudioSource playEffect(string effectName, float volume)
    {
        AudioSource used = findFreeSource();

        foreach (AudioClip effect in SFXList)
        {
            if (used != null && effect.name == effectName)
            {
                if(used != null && volume <= effectVol)
                {
                    used.volume = volume/100;
                }
                else {

                    if (used != null)
                    {
                        used.volume = effectVol;
                    }
                }

                used.clip = effect;
                used.Play();
                return used;
            }

        }

        return null;

    }


    public void flickSwitch()
    {
        audioSource.PlayOneShot(flick, effectVol);
    }


    //Returns a free audio source to play an effect in.
    AudioSource findFreeSource()
    {
        foreach(AudioSource source in sources)
        {
            if(source.isPlaying == false)
            {
                return source;
            }
        }

        return null;
    }


    public void stopEffect(string name, float fadeTime)
    {

        foreach(AudioSource source in sources)
        {
            
            if(source.clip != null && source.clip.name == name)
            {
                StartCoroutine(fadeEffect(source, fadeTime));
            }
        }
    }



    IEnumerator fadeEffect(AudioSource target, float fadeTime)
    {
        float diff = target.volume - 0;
        float changePerSec = diff / fadeTime;
        float rate = changePerSec / 100;
        float waitInterval = 1 / 100;

        while(target.volume > 0.0001f)
        {
            target.volume = target.volume - rate;
            yield return new WaitForSeconds(waitInterval);
        }
        yield return null;
    }


    //checks if the same effect is being played
    public bool preventRepeat(string clipName)
    {
        foreach(AudioSource audioSource in sources)
        {
            if(audioSource.clip != null && audioSource.clip.name == clipName)
            {
                return true;
            }
            else { return false; }
        }

        return false;
    }

}
