using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public Transform light;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RedButtonPressed(){
        ButtonPressed(Color.red);
    }
    public void BlueButtonPressed(){
        ButtonPressed(Color.blue);
    }
    public void GreenButtonPressed(){
        ButtonPressed(Color.green);
    }
    public void YellowButtonPressed(){
        ButtonPressed(Color.yellow);
    }

    private void ButtonPressed(Color color){
        light.GetComponent<MeshRenderer>().material.color = color;
    }
}
