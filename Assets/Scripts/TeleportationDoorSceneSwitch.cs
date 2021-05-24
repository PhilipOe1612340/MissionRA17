using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationDoorSceneSwitch : MonoBehaviour
{

    public string target;

    public void SwitchToTargetScene(){
    SceneLoader.Instance.LoadNewScene(target);
    }
}
