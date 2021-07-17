using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoanaVoice : MonoBehaviour
{
    private AudioSource Audio;
    public AudioClip joanaStart;
    public AudioClip joanaEnd;
    private int state;
    private int level;
    //public static JoanaVoice instance;
    
    void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        level = (int)GameState.getLightLevel();
        state = GetInt("Visited");

        if (state - 2*level<= 0)
            PlayJoana();
    }

    public void PlayJoana()
    {
        state = GetInt("Visited");
        if (state % 2 == 0)
        {
            Audio.PlayOneShot(joanaStart);
        }
        else
        {
            Audio.PlayOneShot(joanaStart);
        }
        SetInt("VisitedCapsule", state + 1);
    }

    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int GetInt(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }
}
