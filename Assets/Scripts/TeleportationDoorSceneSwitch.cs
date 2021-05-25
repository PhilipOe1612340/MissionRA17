using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationDoorSceneSwitch : MonoBehaviour
{
    public bool active;
    public string target;

    public AudioClip fail;
    public AudioClip teleport;

    private AudioSource audioData;
    // private TeleportationAnchor anchor;

    void Start(){
        audioData = GetComponent<AudioSource>();
        // anchor = this.gameObject.transform.GetChild(0).GetComponent<TeleportationAnchor>();
    }

    public void SwitchToTargetScene(){
        playAudio();
        if(active){
            SceneLoader.Instance.LoadNewScene(target);
        }
    }

    public void setState(bool state){
        active = state;
        playAudio();
    }

    private void playAudio(){
         if(active){
            audioData.PlayOneShot(teleport);
        } else {
            audioData.PlayOneShot(fail);
        }
    }
}
