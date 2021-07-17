using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessColor : MonoBehaviour
{


    public GameObject access;
    public bool finished  = false;


    void Start(){
        
        access.GetComponent<Renderer> ().material.color = Color.red;
    }

    void Update()
    {
        if (finished)
        {
            changeColor();
        }
    }

    public void setTrue()
    {
        finished = true;
    }

    public void changeColor()
    {
        access.GetComponent<Renderer> ().material.color = Color.green;

    }
    


    
}
