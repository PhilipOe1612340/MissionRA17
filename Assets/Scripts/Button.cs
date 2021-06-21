using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Button : XRBaseInteractable
{

    public Color hoverColor;
    public Color startColor;

    private Transform top;
    private Transform stem;
    private Material[] materials;

    void Start(){
        top = transform.GetChild(1);
        stem = transform.GetChild(2);
        materials = new Material[] {top.GetComponent<MeshRenderer>().material, stem.GetComponent<MeshRenderer>().material};
        setColor(startColor);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args){
        base.OnSelectEntered(args);
        top.transform.position -= transform.forward * 0.01f;
    }

    protected override void OnSelectExited(SelectExitEventArgs args){
        base.OnSelectExited(args);
        top.transform.position += transform.forward * 0.01f;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        setColor(hoverColor);
    }

    protected override void OnHoverExited(HoverExitEventArgs args){
        base.OnHoverExited(args);
        setColor(startColor);
    }

    private void setColor(Color color){
        foreach (var material in materials)
        {
            material.color = color;
        }
    }

}
