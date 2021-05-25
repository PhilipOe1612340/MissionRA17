using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavComputerStart : MonoBehaviour
{

    public GameObject computer;
    public AudioSource bootAudio;
    private ParticleSystem particles; 

    void Start(){
        particles = computer.GetComponent<ParticleSystem>();
        computer.SetActive(false);
    }

    public void startComputer(){
        computer.SetActive(true);
        particles.Play();
        bootAudio.Play();
    }
}
