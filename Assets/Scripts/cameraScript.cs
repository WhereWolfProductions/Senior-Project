using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    float sensitivity = 50;
    float yRotate = 0;

    PostProcessingBehaviour behaviour;
    PostProcessingProfile newProfile;

    Vector3 lastGoodRot;   //The last good rotation before hitting a surface that may rotate the player

    public bool canMove = true;
    public bool diving;
    GameObject cam;

	// Use this for initialization
	void Start () {

        cam = Camera.main.gameObject;

        behaviour = cam.GetComponent<PostProcessingBehaviour>();

        //Sets postProfile to a copy of the default profile.
        newProfile = Instantiate(behaviour.profile);

        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        if (canMove == true) { cameraMove(); }
        Cursor.lockState = CursorLockMode.Locked;

        transform.position = new Vector3(GameObject.FindWithTag("Player").transform.position.x, transform.parent.position.y + 1.639f, GameObject.FindWithTag("Player").transform.position.z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }





    //Camera Movement
    void cameraMove()
    {

        float horAim = Input.GetAxis("Mouse X");

        Vector3 newRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + (sensitivity * Time.deltaTime) * (horAim),
            transform.localEulerAngles.z);
        newRotation.y = 0;


        //Rotates player on Y-axis according to mouse camera rotation
        Vector3 playerRot = new Vector3(0, transform.parent.localEulerAngles.y + (sensitivity * Time.deltaTime) * (horAim), 0);
        transform.parent.localEulerAngles = playerRot;

        transform.localEulerAngles = newRotation;

        yRotate += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        yRotate = Mathf.Clamp(yRotate, -90, 90);


        Camera.main.transform.localEulerAngles = new Vector3(-yRotate, Camera.main.transform.localEulerAngles.y,
            Camera.main.transform.localEulerAngles.z);

    }

    IEnumerator incFov(float start, float target, float time)
    {

        float diff = target - 0;
        float changePerSec = diff / time;
        float rate = changePerSec / 100;
        float waitInterval = 1 / 100;

        while (cam != null && cam.GetComponent<Camera>().fieldOfView < target)
        {
            cam.GetComponent<Camera>().fieldOfView = cam.GetComponent<Camera>().fieldOfView + rate;
            yield return new WaitForSeconds(waitInterval);

        }

    }

    
    //Changes the profile copy to make weird effects when diving, also makes sound and fov change
    public IEnumerator diveEffect()
    {
        diving = true;
        StartCoroutine(incFov(90, 175, 9));
        AudioSource warp = effectPlayer.effectPlayerData.playEffect("warp", 70);


        while(warp.isPlaying == true)
        {
            ChromaticAberrationModel.Settings newchrome = newProfile.chromaticAberration.settings;
            float chromeValue = -0.2f * (Mathf.Cos(Time.time * 20) * 10) + 3f;
            newchrome.intensity = chromeValue;

            newProfile.chromaticAberration.enabled = true;
            newProfile.chromaticAberration.settings = newchrome;
            cam.GetComponent<PostProcessingBehaviour>().profile = newProfile;
            yield return new WaitForSeconds(1/1000);
        }

        diving = false;
    }

    public IEnumerator glitch()
    {

        for (int i = 0; i < 20; i++)
        {
            ChromaticAberrationModel.Settings newchrome = newProfile.chromaticAberration.settings;
            newProfile.chromaticAberration.enabled = true;

            float chromeValue = -0.2f * (Mathf.Cos(Time.time * 20) * 10) + 3f;
            newchrome.intensity = chromeValue;
            newProfile.chromaticAberration.settings = newchrome;
            cam.GetComponent<PostProcessingBehaviour>().profile = newProfile;
            yield return new WaitForSeconds(1 / 1000);

        }

        cam.GetComponent<PostProcessingBehaviour>().profile = Resources.Load("Post Processing") as PostProcessingProfile;
    }
}
