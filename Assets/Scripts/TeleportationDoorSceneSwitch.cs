using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationDoorSceneSwitch : MonoBehaviour
{
    public bool active;
    public string target;

    public AudioClip fail;
    public LightLevel setLightLevelTo;

    private AudioSource audioData;
    private GameObject anchor;

    private int teleportLayer = 6;

    void Awake(){
        anchor = transform.GetChild(0).gameObject;
    }

    void Start(){
        audioData = GetComponent<AudioSource>();
        anchor.layer = active ? teleportLayer : 0;
    }

    public void SwitchToTargetScene(){
        if(active) {
            SceneLoader.Instance.LoadNewScene(target);
        } else {
            audioData.PlayOneShot(fail);
        }
        GameState.setLightLevel(setLightLevelTo);
    }

    public void setState(bool state){
        active = state;
        anchor.layer = state ? teleportLayer : 0;
    }

}
