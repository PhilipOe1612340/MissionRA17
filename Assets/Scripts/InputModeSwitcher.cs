using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportationEnabler : MonoBehaviour
{

    public GameObject baseController;
    public GameObject teleportationController;

    public InputActionReference teleportationActivationRef;

    public UnityEvent onTeleportActive;
    public UnityEvent onTeleportCancel;
    public UnityEvent onSceneSwitch;

    void Start()
    {
        teleportationActivationRef.action.performed += TeleportActivate;
        teleportationActivationRef.action.canceled += TeleportCancel;
    }

    private void TeleportActivate(InputAction.CallbackContext obj){
        onTeleportActive.Invoke();
    }

    private void TeleportCancel(InputAction.CallbackContext obj){
        Invoke("DeactivateTeleporter", .1f);
    }

    void DeactivateTeleporter(){
        onTeleportCancel.Invoke();
    }
}
