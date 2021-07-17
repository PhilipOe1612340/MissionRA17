using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour
{

    public GameObject[] objs;
    public Color lightColor;
    LightLevel level;

    void Start()
    {
        level = GameState.getLightLevel();
        StartCoroutine(loop());
    }

    public void setLightLevel(LightLevel l){
        level  = l;
    }

    private IEnumerator loop(){
        while(true){
            SetColor(lightColor);
            yield return new WaitForSeconds(10 / (int)level);
            SetColor(Color.black);
            yield return new WaitForSeconds(10 / (int)level);
        }
    }

    public void SetColor(Color color){
        foreach (var obj in objs)
        {
            Material lightMaterial = obj.GetComponent<MeshRenderer>().material;
            lightMaterial.color = color;
            lightMaterial.SetVector("_EmissionColor", color * 1.1f);
        }
    }
}
