using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapDeskControl : MonoBehaviour
{
    public GameObject lever;
    public Transform leverLight;
    private Material leverLightMaterial;
    private Material buttonMaterial;
    public HingeJoint leverHinge;
    bool deskActive;
    //public Light lit;
    bool buttonsActive;
    bool button1 = false;
    bool button2 = false;
    bool button3 = false;
    //bool state = false;
    public GameObject[] buttons;
    public GameObject finalButton;
    //public GameObject button2;
    //public GameObject button3;

    public AudioClip deskActivation;
    public AudioClip clamp;
    public AudioClip clamp2;
    public AudioClip clamp3;
    public AudioClip engineSound; 
    
    private AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        leverLightMaterial = leverLight.GetComponent<MeshRenderer>().material;
        buttonsActive = false;
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deskActive)
        {
           
            if (leverHinge.angle <= 10)
            {               
                //deactivate ALL buttons and reset lever
                deactivateEverything();


            }
            
            if (button1 && button2 && button3)
            {
                activatefinalButton();

            }
        }
        else
        {
            if (leverHinge.angle >= 55 && !buttonsActive)
            {
                deskActive = true;
                SetColor(leverLightMaterial, Color.green);
                
                audioData.PlayOneShot(deskActivation);
                activateButtons();
            }

        }
        
    }
    void deactivateEverything()
    {
        buttonsActive = false;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonMaterial = buttons[i].transform.GetComponent<MeshRenderer>().material;
            SetColor(buttonMaterial, Color.black);

        }
        button1 = false;
        button2 = false;
        button3 = false;

        //final button
        buttonMaterial = finalButton.transform.GetComponent<MeshRenderer>().material;
        SetColor(buttonMaterial, Color.black);

        SetColor(leverLightMaterial, Color.white);
        deskActive = false;
    }

    void activateButtons()
    {
        buttonsActive = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonMaterial = buttons[i].transform.GetComponent<MeshRenderer>().material;
            SetColor(buttonMaterial, Color.white); // oder rot?
        }
    }

    void activatefinalButton()
    {
        buttonMaterial = finalButton.transform.GetComponent<MeshRenderer>().material;
        SetColor(buttonMaterial, Color.white);
    }

    public void FirstButtonPressed()
    {
        button1 = ToggleState(button1,buttons[0],clamp);
    }
    public void SecondButtonPressed()
    { 
        button2 = ToggleState(button2,buttons[1],clamp2);
    }
    public void ThirdButtonPressed()
    {
        button3 = ToggleState(button3,buttons[2],clamp3);
    }


    private bool ToggleState(bool state, GameObject button, AudioClip clip)
    {
        if (buttonsActive)
        {
            
            buttonMaterial = button.transform.GetComponent<MeshRenderer>().material;
            if (state)
            {
                SetColor(buttonMaterial, Color.red);
                state = false;
            }
            else
            {
                SetColor(buttonMaterial, Color.green);
                
                state = true;
                audioData.PlayOneShot(clip);
            }
   
        }
        return state;
    }

    public void DeskStartup()
    {
        // when Desk becomes interactable
        SetColor(leverLightMaterial, Color.white);
    }

    public void finalButtonpressed()
    {
        
        if (button1 && button2 && button3)
        {
            buttonMaterial = finalButton.transform.GetComponent<MeshRenderer>().material;
            SetColor(buttonMaterial, Color.green);

            //Countdown?
            audioData.clip = engineSound;
            audioData.loop = true;
            audioData.Play();

            // final -> some kinda Endscreen?

        }
    }
    private void SetColor(Material mat, Color color)
    {
        mat.color = color;
        mat.SetVector("_EmissionColor", color * 1.1f);

    }
}
