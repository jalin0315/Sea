﻿using UnityEngine;
using System.Collections;

namespace CTJ
{
    public static class Vibration
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    public static AndroidJavaClass vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
    public static int defaultAmplitude = vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");
    public static AndroidJavaClass androidVersion = new AndroidJavaClass("android.os.Build$VERSION");
    public static int apiLevel = androidVersion.GetStatic<int>("SDK_INT");
#else
        public static AndroidJavaClass unityPlayer;
        public static AndroidJavaObject vibrator;
        public static AndroidJavaObject currentActivity;
        public static AndroidJavaClass vibrationEffectClass;
        public static int defaultAmplitude;
#endif

        public static void Vibrate(int _status)
        {
            if (!GameManager._Instance._Enable_Vibrate) return;
            switch (_status)
            {
                case 0:
                    CreateOneShot(1000, defaultAmplitude);
                    break;
                case 1:
                    CreateOneShot(25, defaultAmplitude);
                    break;
                case 2:
                    CreateOneShot(100, defaultAmplitude);
                    break;
                default:
                    Logger.LogErrorFormat("Vibration Status Error! {0}", _status);
                    break;
            }
        }

        public static void CreateOneShot(long milliseconds, int amplitude)
        {
            CreateVibrationEffect("createOneShot", new object[] { milliseconds, amplitude });
        }

        public static void CreateWaveform(long[] timings, int repeat)
        {
            CreateVibrationEffect("createWaveform", new object[] { timings, repeat });
        }

        public static void CreateWaveform(long[] timings, int[] amplitudes, int repeat)
        {
            CreateVibrationEffect("createWaveform", new object[] { timings, amplitudes, repeat });
        }

        public static void CreateVibrationEffect(string function, params object[] args)
        {
            if (isAndroid() && HasAmplituideControl())
            {
                AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>(function, args);
                vibrator.Call("vibrate", vibrationEffect);
            }
            else Handheld.Vibrate();
        }

        public static bool HasVibrator()
        {
            return vibrator.Call<bool>("hasVibrator");
        }

        public static bool HasAmplituideControl()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (apiLevel >= 26) return vibrator.Call<bool>("hasAmplitudeControl"); // API 26+ specific
        else return false; // no amplitude control below API level 26
#else
            return false;
#endif
        }

        public static void Cancel()
        {
            if (isAndroid()) vibrator.Call("cancel");
        }

        private static bool isAndroid()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
            return false;
#endif
        }
    }
}
