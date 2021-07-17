using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusic : Singleton<backgroundMusic>
{
    public AudioClip easy;
    public AudioClip creepy;
    public AudioSource player;
    private bool isCreepy = false;

    public void Awake(){
        player.clip = easy;
        player.PlayDelayed(5);
    }

   public void switchToCreepy(){
        if(!isCreepy){
            isCreepy = true;
            player.Stop();
            player.clip = creepy;
            player.PlayDelayed(5);
        }
   }
}
