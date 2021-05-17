using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{

    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider provider;
    private InputAction thumbstick;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActive;
        
        var cancel = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        thumbstick = actionAsset.FindActionMap("XRI LeftHand").FindAction("Move");
        thumbstick.Enable();


    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive){
            return;
        }

        // player has not released the thumbstick yet
        if (thumbstick.triggered){
            return;
        }

        // no valid location
        if(!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit)){
            Disable();
            return;
        }
        TeleportRequest request = new TeleportRequest(){
            destinationPosition = hit.point,
        };

        provider.QueueTeleportRequest(request);
        Disable();
    }



    private void OnTeleportActive(InputAction.CallbackContext context){
        rayInteractor.enabled = true;
        isActive = true;

    }

    private void OnTeleportCancel(InputAction.CallbackContext context){
        Disable();
    }

    private void Disable(){
        rayInteractor.enabled = false;
        isActive = false;
    }
}
