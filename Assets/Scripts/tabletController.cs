using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class tabletController : MonoBehaviour
{

    Renderer rend;
    Transform Canvas;
    Transform Panel;
    Color baseColor;

    List<Transform> Buttons;

	// Use this for initialization
	void Start () {
        Buttons = new List<Transform>();
        Canvas = transform.Find("Canvas");

        setPanel("Main Menu");
        Panel.gameObject.SetActive(false);
        setButtons();



        baseColor = new Color(0.6470588f, 0.9853955f, 1f) * 0.1f;

        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
		
	}





    void changeButtonAlpha(string sign)
    {

        if (sign == "+")
        {
            Graphic buttonGraphic;
            Color txtBase;
            foreach (Transform button in Buttons)
            {
                txtBase = button.Find("Text").GetComponent<Text>().color;
                button.Find("Text").GetComponent<Text>().color = new Color(txtBase.r, txtBase.g, txtBase.b, 1);
                buttonGraphic = button.Find("Text").GetComponent<Text>();
                buttonGraphic.CrossFadeAlpha(1, 0.01f, false);
            }
            
        }

        else if (sign == "-")
        {
            Graphic buttonGraphic;
            foreach (Transform button in Buttons)
            {
                buttonGraphic = button.Find("Text").GetComponent<Text>();
                buttonGraphic.CrossFadeAlpha(-1, 0.01f, false);
            }
        }
    
    }

    private void OnMouseEnter()
    {
        rend.material.SetColor("_EmissionColor", new Color(0.6470588f, 0.9853955f, 1f) * .25f);
        Panel.gameObject.SetActive(true);

    }

    private void OnMouseExit()
    {
        rend.material.SetColor("_EmissionColor", baseColor);
        Panel.gameObject.SetActive(false);
    }

    //Sets and or updates the current panel.
    void setPanel(string panelName)
    {
        Panel = Canvas.Find(panelName);
    }


    //Sets and resets list of buttons
    void setButtons()
    {

        Buttons.Clear();

        for (int i = 0; i != Panel.childCount; i++)
        {
            Buttons.Add(Panel.GetChild(i));
        }
    }


    List<GameObject> findPanels()
    {
        List<GameObject> Panels = new List<GameObject>();
        for (int i = 0; i != Canvas.childCount; i++)
        {
            Panels.Add(Canvas.GetChild(i).gameObject);
        }
        return Panels;
    }




    //Disables all other panels except the specified one given to the function.
    public void disableOthers(string panelName)
    {
        foreach(GameObject panel in findPanels())
        {
            if(panel.name != panelName)
            {
                panel.SetActive(false);
            }
        }
    }


    //Enables other panels
    void enableOthers()
    {
        foreach (GameObject panel in findPanels())
        {
            if (panel != Panel.gameObject)
            {
                panel.SetActive(true);
            }
        }
        setButtons();
    }


    public void enablePanel(string panelName)
    {
        Canvas.Find(panelName).gameObject.SetActive(true);
        setPanel(panelName);
        setButtons();
    }

    public void disablePanel(string panelName)
    {
        Canvas.Find(panelName).gameObject.SetActive(false);
    }



}
