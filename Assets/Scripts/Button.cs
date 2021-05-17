using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Button : XRGrabInteractable
{

    public Color hoverColor;
    public Color startColor;

    void Start(){
        GetComponent<MeshRenderer>().material.color = startColor;    
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args){
        // overwrite selection so button can not be moved.
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        GetComponent<MeshRenderer>().material.color = hoverColor;    
    }

    protected override void OnHoverExited(HoverExitEventArgs args){
        base.OnHoverExited(args);
        GetComponent<MeshRenderer>().material.color = startColor;    
    }

}
