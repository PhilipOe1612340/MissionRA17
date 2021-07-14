using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SecondCamera : MonoBehaviour
{

    private void OnPreRender()
    {
        RenderSettings.fog = false;
    }

    private void OnPostRender()
    {
        RenderSettings.fog = true;
    }
}