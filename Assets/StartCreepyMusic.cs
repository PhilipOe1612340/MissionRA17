using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCreepyMusic : MonoBehaviour
{

    void Start()
    {
        backgroundMusic.Instance.switchToCreepy();
    }
}
