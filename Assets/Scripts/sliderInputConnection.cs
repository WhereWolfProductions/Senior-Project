using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class sliderInputConnection : MonoBehaviour {


	// Use this for initialization
	void Awake () {

    }

    private void OnEnable()
    {
        setDefaultSound();
    }





    // Update is called once per frame
    void Update () {

    }

  


    //sets slider to the value of the field
    public void setSlider(GameObject targetSlider)
    {
        targetSlider.GetComponent<Slider>().value =
        float.Parse(this.GetComponent<InputField>().text) / 100;
    }

    //sets field to the value of the slider
    public void setField(GameObject targetField)
    {

        targetField.GetComponent<InputField>().text =
            (this.GetComponent<Slider>().value * 100).ToString();

    }



    public void sliderSetMusicVol()
    {
        float sliderValue = gameObject.GetComponent<Slider>().value;
        musicManager.musicManagerData.setVolume(sliderValue * 100);
    }

    public void sliderSetEffectVolume()
    {
        float sliderValue = gameObject.GetComponent<Slider>().value;
        effectPlayer.effectPlayerData.effectVol = sliderValue;
    }



    bool checkSlider()
    {
        if (gameObject.GetComponent<Slider>() != null)
        {
            return true;
        }
        else if (gameObject.GetComponent<InputField>() != null)
        {
            return false;
        }
        else { return false; }
    }


    void setDefaultSound()
    {


        switch(gameObject.transform.parent.gameObject.name)
        {

            case "Music Volume":
                if(checkSlider() == true)
                {
                    Debug.Log(musicManager.musicManagerData.musicVol);
                    gameObject.GetComponent<Slider>().value = musicManager.musicManagerData.musicVol / 100;
                }
                else { gameObject.GetComponent<InputField>().text = (musicManager.musicManagerData.musicVol).ToString();}
                break;

            case "SFX":
                if(checkSlider() == true)
                {
                    gameObject.GetComponent<Slider>().value = effectPlayer.effectPlayerData.effectVol;
                }
                else { gameObject.GetComponent<InputField>().text = (effectPlayer.effectPlayerData.effectVol * 100).ToString(); }
                break;
        }

    }

    float stringToFloat(string input)
    {
        float result;
        
        if (float.TryParse(input, out result) == true)
        {
            return float.Parse(input);
        }
        else { return 0; }
    }

}
