using UnityEngine;
using UnityEngine.UI;

public enum LightLevel{
    low = 1, 
    medium = 2,
    high = 3,
    intense = 4
}

public class GameState : MonoBehaviour
{

    public static LightLevel getLightLevel(){
        return (LightLevel)PlayerPrefs.GetInt("LightLevel", (int)LightLevel.low);
    }

    public static LightLevel setLightLevel(LightLevel light){
        if(light > getLightLevel()){
            PlayerPrefs.SetInt("LightLevel", (int)light);
        }

        return getLightLevel();
    }

}
