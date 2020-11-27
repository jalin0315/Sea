using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Initialization : MonoBehaviour
{
    public static Initialization _Instance;

    private void Awake()
    {
        _Instance = this;
        //Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
