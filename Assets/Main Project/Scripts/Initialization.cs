using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    public static Initialization _Instance;

    private void Awake()
    {
        _Instance = this;
        Application.targetFrameRate = -1;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
