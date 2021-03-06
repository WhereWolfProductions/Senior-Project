﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class fadeScript : MonoBehaviour {


    public float fadeInSpeed = 0.01f;
    public float fadeOutSpeed = 0.01f;

	// Use this for initialization
	void Start () {
        
	}


	
	// Update is called once per frame
	void Update () {
        Canvas canvas = gameObject.GetComponent<Canvas>();

    }


    public IEnumerator fadeOut()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        Image canvasImage = canvas.transform.Find("Image").GetComponent<Image>();
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        canvas.GetComponent<Canvas>().planeDistance = 0.4f;
        canvasImage.color = new Color(0, 0, 0, 0);


        while (canvasImage.color.a < 1)
        {
            Color fadedColor = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, canvasImage.color.a + fadeOutSpeed);
            canvasImage.color = fadedColor;
            yield return new WaitForSeconds(0.015f);

        }

        canvasImage.color = new Color(0, 0, 0, 1);

    }

    public IEnumerator fadeIn()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        Image canvasImage = canvas.transform.Find("Image").GetComponent<Image>();
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        canvas.GetComponent<Canvas>().planeDistance = 0.4f;
        canvasImage.color = new Color(0, 0, 0, 1);


        while (canvasImage != null && canvasImage.color.a > 0)
        {
            Color fadedColor = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, canvasImage.color.a - fadeInSpeed);
            canvasImage.color = fadedColor;
            yield return new WaitForSeconds(0.015f);
        }

        if (canvasImage != null)
        {
            canvasImage.color = new Color(0, 0, 0, 0);
        }

    }

    public void stopFade()
    {
        StopAllCoroutines();
    }
	
    
    //Gives the current alpha of the fade screen.
    public float getFade()
    {
	    return canvas.transform.Find("Image").GetComponent<Image>().color.a;
    }

	
	
	
	
	
	
}
