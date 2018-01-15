using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ballAnimator : MonoBehaviour {

    List<GameObject> balls;
    List<Image> ballImages;
    float fadeOutSpeed = 0.01f;

    public Coroutine ballCo;

    public int activeBall;

	// Use this for initialization
	void Start () {

        balls = new List<GameObject>();
        ballImages = new List<Image>();
        activeBall = 0;

        //The minus one accounts for the added text to the download prefab.
		for(int i = 0; i < transform.childCount - 1; i++)
        {

            balls.Add(transform.GetChild(i).gameObject);

        }

        foreach(GameObject ball in balls)
        {
            ballImages.Add(ball.GetComponent<Image>());
        }

        ballCo = StartCoroutine(toggleBalls());
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void stopAnim()
    {
        StopCoroutine(ballCo);
    }

    IEnumerator toggleBalls()
    {

        if (balls[0].activeSelf == true) { yield return new WaitForSeconds(2.5f); }
        if (activeBall + 1 == balls.Count)
        {
            activeBall = 0;
            foreach (GameObject ball in balls)
            {
                ball.SetActive(false);
            }
        }
        else if (balls[activeBall].activeSelf == false)
        {
            balls[activeBall].SetActive(true);
            activeBall = 0;
        }
        else if (balls[activeBall].activeSelf == true)
        {
            
            if (balls[activeBall + 1].activeSelf == false)
            {
                activeBall = activeBall + 1;
                balls[activeBall].SetActive(true);
            }

        }


        ballCo = StartCoroutine(toggleBalls());
    }


    

    //Fades an image by a given time.
    IEnumerator fadeOutAnim(float fadeTime)
    {
        Color intialColor = balls[0].GetComponent<Image>().color;
        Color targetColor = new Color(intialColor.r, intialColor.g, intialColor.b, 0);

        float diff = Mathf.Abs(targetColor.a - intialColor.a);
        float changePerSec = diff / fadeTime;
        float rate = changePerSec / 100;
        float waitInterval = 1 / 100;

        while (balls[0].GetComponent<Image>().color.a > 0.001f)
        {
            foreach (Image ballImage in ballImages)
            {
                if (ballImage.color.a > 0.001f)
                {
                    Color fadedColor = new Color(ballImage.color.r, ballImage.color.g, ballImage.color.b, ballImage.color.a - rate);
                    ballImage.color = fadedColor;
                }

            }
            yield return new WaitForSeconds(waitInterval);
        }



    }

    public void fadeAnim()
    {
        StartCoroutine(fadeOutAnim(2));
        StartCoroutine(fadeText(gameObject.transform.Find("Download Text").gameObject.GetComponent<Text>(), 2));
    }

    

    public bool checkFaded()
    {
        bool allFaded = false;

        foreach(Image ballImage in ballImages)
        {
            if(ballImage.color.a > 0.001f)
            {
                allFaded = false;
            }
            else { allFaded = true; }
        }

        if(allFaded == true)
        {
            return true;
        }
        else { return false; }
    }




    //Text Fading


    IEnumerator fadeText(Text targetText, float fadeTime)
    {
        Color intialColor = targetText.color;
        Color targetColor = new Color(intialColor.r, intialColor.g, intialColor.b, 0);

        float diff = Mathf.Abs(targetColor.a - intialColor.a);
        float changePerSec = diff / fadeTime;
        float rate = changePerSec / 100;
        float waitInterval = 1 / 100;

        while (targetText.color.a > 0.001)
        {
            Color currentTextColor = targetText.color;
            Color newColor = new Color(currentTextColor.r, currentTextColor.g, currentTextColor.b, currentTextColor.a - rate);
            yield return new WaitForSeconds(waitInterval);

            targetText.color = newColor;
        }



    }


}
