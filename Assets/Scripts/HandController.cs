using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] InputActionReference controllerActionGrip;
    [SerializeField] InputActionReference controllerActionTrigger;

    private Animator handAnimator;


    void Awake(){
        controllerActionGrip.action.performed += GripPress;
        controllerActionTrigger.action.performed += TriggerPress;
        controllerActionGrip.action.canceled += GripPress;
        controllerActionTrigger.action.canceled += TriggerPress;

        handAnimator = GetComponent<Animator>();
    }

    private void GripPress(InputAction.CallbackContext obj){
        handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }

    private void TriggerPress(InputAction.CallbackContext obj){
        handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
    }

}
