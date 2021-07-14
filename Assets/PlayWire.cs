using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWire : MonoBehaviour
{
    [SerializeField]
    GameObject tool;
    [SerializeField]
    GameObject wire;
    OffsetGrab grab;

    public static bool activateCollision = false;

    void Update()
    {
        //check if collsion is already enabled
        if (activateCollision == true)
        {
            destroyTool();
            createTool();
        }
        
    }

    private void destroyTool()
    {
        //destroy tool
        Destroy(tool);
    }

    private void createTool()
    {
        //Instatntiate new tool for playing
        tool = Instantiate(tool, new Vector3(-14,0,4), Quaternion.identity); //adjust postion for game
        //enable grabbing for CubeClone
        grab = tool.GetComponent<OffsetGrab>();
        grab.enabled = true;
    }
    
    

    
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("collide");
        //check collision of GameObjects with Wire
        
            //enable collision
            activateCollision = true;
            
    }


    void OnTriggerEnter(Collider other)
    {
        //Controller <-> cube
         Debug.Log("craaaaaash");
         if (other.tag == "game")
        {
            Debug.Log("more crash");
            //enable collision
            activateCollision = true;
            
        }
    }

/*
    void OnTriggerStay(Collider other)
    {
        Debug.Log("booooom");
    }*/

   

}
