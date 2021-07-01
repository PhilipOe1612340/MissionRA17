using UnityEngine;
using System;


[Serializable]
public struct AudioEventDict {
    public string key;
    public AudioClip clip;
}


public class AudioFeedbackEvents : MonoBehaviour
{
    public AudioEventDict[] eventList;
    private AudioSource audioData;

    // Start is called before the first frame update
    void Awake()
    {
       audioData = GetComponent<AudioSource>();        
    }

    public void triggerEvent(string eventKey){
        AudioClip clip = getClip(eventKey);
        audioData.PlayOneShot(clip);
    }

    private AudioClip getClip(string name){
        foreach (AudioEventDict item in eventList)
        {
            if(item.key == name){
                return item.clip;
            }
        }

        return eventList[0].clip;
    }
}
