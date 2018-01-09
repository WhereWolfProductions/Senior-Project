using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class computerController : MonoBehaviour {


    bool clickable = false;

    float floatSpeed = .6f;
    Vector3 floatDirection = new Vector3(0, .15f, 0);

    Coroutine spinCubeRoutine;
    Coroutine floatCubeRoutine;
    Coroutine humSound;

	// Use this for initialization
	void Start () {

        clickable = false;
        StartCoroutine(preventEarlyClick());
        spinCubeRoutine = StartCoroutine(spinCube());
        floatCubeRoutine = StartCoroutine(floatCube());
        humSound = StartCoroutine(humEffect());
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator preventEarlyClick()
    {
        yield return new WaitUntil(() => Camera.main.GetComponent<cameraScript>().canMove == true);
        clickable = true;
        
    }


    IEnumerator spinCube()
    {
        Vector3 roation = gameObject.transform.localEulerAngles;
        gameObject.transform.localEulerAngles = new Vector3(roation.x, roation.y + 3, roation.z);
        yield return new WaitForSeconds(0.03f);
        spinCubeRoutine  = StartCoroutine(spinCube());
    }


    

    IEnumerator floatCube()
    {
        while(gameObject.transform.position.y < floatDirection.y)
        {
            transform.Translate(floatDirection * Time.deltaTime * floatSpeed, Space.World);
            yield return null;
        }

        yield return new WaitUntil(() => gameObject.transform.position.y >= floatDirection.y);

        while (gameObject.transform.position.y > floatDirection.y * -1)
        {
            transform.Translate(floatDirection * Time.deltaTime * floatSpeed * -1, Space.World);
            yield return null;
        }
        floatCubeRoutine = StartCoroutine(floatCube());
    }


    IEnumerator humEffect()
    {
        AudioSource used =  effectPlayer.effectPlayerData.playEffect("Eletronic Hum", 0.1f);
        yield return new WaitUntil(() => used.isPlaying == false);
        humSound = StartCoroutine(humEffect());
    }



    private void OnMouseDown()
    {

        if (clickable == true)
        {
            StopCoroutine(spinCubeRoutine);
            StopCoroutine(floatCubeRoutine);
            StopCoroutine(humSound);
            effectPlayer.effectPlayerData.stopEffect("Eletronic Hum", 1f);

            StartCoroutine(resetPos());
            StartCoroutine(resetRotation());
            StartCoroutine(expandScreen());


            clickable = false;
        }


    }


    //resets position of cube to 0,0,-3
    IEnumerator resetPos()
    {

        Vector3 targetPos = new Vector3(0, 0, 1.5f);
        while (transform.position != targetPos)
        {
            Vector3 newPos = new Vector3(targetPos.x - 0, targetPos.y - (transform.position.y), targetPos.z - transform.position.z);
            transform.Translate(newPos * Time.deltaTime * (floatSpeed + 1.5f), Space.World);
            if(transform.position.y > 0f && transform.position.y < 0.0005f) //when close enough, sets position to be on target to avoid endless loop
            {
                transform.position = targetPos;
            }
            yield return null;
        }
    }


    //resets the roation of the cube to 0,0,0
    IEnumerator resetRotation()
    {
        Vector3 temp = new Vector3(-90, 0, 0);
        Quaternion rotationPos = Quaternion.Euler(temp);
        
        while (transform.localEulerAngles != rotationPos.eulerAngles)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationPos, Time.deltaTime * 3);
            if (transform.rotation.eulerAngles.y > 0f && transform.rotation.eulerAngles.y < 0.0005f) //when close enough, sets position to be on target to avoid endless loop
            {
                transform.rotation = rotationPos;
            }
            yield return null;
        }
    }


    //Expands the cube so that a UI screen can be placed over it.
    IEnumerator expandScreen()
    {
        Vector3 targetSize = new Vector3(6, 10, 8);

        while (transform.localScale != targetSize)
        {
            Vector3 size = transform.localScale;
            //Finds difference between current size and target size to find change amount needed.
            Vector3 newSize = new Vector3(targetSize.x - size.x, targetSize.y - size.y, targetSize.z - size.z);

            //sets current scale to half of newsize for growing in size effect.
            transform.localScale += newSize * .03f;
            if (checkVectors(size, targetSize, 1.0005f))
            {
                transform.localScale = targetSize;
            }
            
            yield return null;
        }

        StartCoroutine(fadeEmission(transform.Find("Cube").GetComponent<Renderer>().material));
        yield return new WaitUntil(() => transform.Find("Cube").GetComponent<Renderer>().material.GetColor("_EmissionColor").a > 0.3f);
        spawnCanvases();
        
    }



    IEnumerator fadeEmission(Material emissionTarget)
    {

        while (emissionTarget.GetColor("_EmissionColor").a > 0.3f)
        {
            Color currentEmission = emissionTarget.GetColor("_EmissionColor");
            emissionTarget.SetColor("_EmissionColor", currentEmission * 0.95f);
            yield return new WaitForSeconds(0.05f);
        }

    }




    //If two vectors are close enough, returns true.
    bool checkVectors(Vector3 original, Vector3 compared, float accuracyPercentage)
    {
        float diffX = original.x - compared.x;
        float diffY = original.y - compared.y;
        float diffZ = original.z - compared.z;
        bool closeX, closeY, closeZ;
        closeX = false;
        closeY = false;
        closeZ = false;

        if(Mathf.Abs(diffX) < Mathf.Abs(original.x - (original.x * accuracyPercentage)) )
        {
            closeX = true;
        }
        if(Mathf.Abs(diffY) < original.y * accuracyPercentage)
        {
            closeY = true;
        }
        if(Mathf.Abs(diffZ) < original.z * accuracyPercentage)
        {
            closeZ = true;
        }

        if(closeX == true && closeY == true && closeZ == true)
        {
            return true;
        }
        else { return false; }
        
    }


    //@@@@@@@@@@@@@@@@@@@@@@@@@
    //@@@@@ Canvas Driver @@@@@
    //@@@@@@@@@@@@@@@@@@@@@@@@@

    //Manages the canvases that are displayed on the computer, and the images on those canvases.

    //spawns a canvas on each part of the compter.

        //Save canvas paths, spawn image as component of Image empty

    GameObject pcCanvases;
    Sprite[] imageList;

    void spawnCanvases()
    {
        pcCanvases = Instantiate(Resources.Load("pcCanvases") as GameObject, gameObject.transform.Find("Cube"));
        loadImages();
        Image addedImage = addImage(pcCanvases.transform.Find("Middle Canvas").gameObject, "Peace and prosperity symbol");
    }


    //loads all images at once to prevent pauses from resource.load
    void loadImages()
    {
        imageList = Resources.LoadAll<Sprite>("Images");
        Debug.Log(imageList[0]);
    }

    //Searches imageList for an image name and returns it if found
    Sprite findImage(string imageToFind)
    {
        foreach(Sprite image in imageList)
        {
            if(image.name == imageToFind)
            {
                return image;
            }
        }

        return null;
    }


    //Adds an image to a canvas, returns the image component for customization if needed.
    Image addImage(GameObject targetCanvas, string imageName)
    {
        Image spawnedImage = targetCanvas.transform.Find("Panel").Find("Image").gameObject.AddComponent<Image>();
        spawnedImage.sprite = findImage(imageName);
        return spawnedImage;
    }


}




