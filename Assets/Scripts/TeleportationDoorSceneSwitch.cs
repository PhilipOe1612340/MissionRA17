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
        playAudio();
        if(active){
            SceneLoader.Instance.LoadNewScene(target);
        }
    }

    public void setState(bool state){
        active = state;
        playAudio();

        anchor.layer = state ? teleportLayer : 0;
    }

    private void playAudio(){
         if(active){
            audioData.PlayOneShot(teleport);
        } else {
            audioData.PlayOneShot(fail);
        }
    }
}
