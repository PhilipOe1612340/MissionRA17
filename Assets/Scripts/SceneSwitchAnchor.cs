using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class SceneSwitchAnchor : MonoBehaviour
{
   public string scene;


   public void switchScene(){
       SceneManager.LoadScene("Scenes/"+ scene);
   }
}
