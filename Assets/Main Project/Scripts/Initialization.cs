using EasyMobile;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class Initialization : MonoBehaviour
    {
        public static Initialization _Instance;

        private void Awake()
        {
            _Instance = this;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
            Load();
            Init();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                // Ask if user wants to exit.
                NativeUI.AlertPopup _alert;
                if (LeanLocalization.CurrentLanguage == "Traditional Chinese") _alert = NativeUI.ShowTwoButtonAlert("離開應用程式", "是否離開應用程式？", "確定", "取消");
                else _alert = NativeUI.ShowTwoButtonAlert("Quit", "Are you sure you want to quit ?", "Yes", "No");
                if (_alert != null) { _alert.OnComplete += delegate (int _button) { if (_button == 0) Application.Quit(); }; }
            }
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetInt("_Initialization", Database._Initialization);
            PlayerPrefs.SetInt("_Play_01", Database._Play_01);
            PlayerPrefs.SetInt("_Play_01_Unlock_00", Database._Play_01_Unlock_00);
            PlayerPrefs.SetInt("_Play_02", Database._Play_02);
            PlayerPrefs.SetInt("_Play_02_Unlock_00", Database._Play_02_Unlock_00);
            PlayerPrefs.SetInt("_Play_02_Unlock_01", Database._Play_02_Unlock_01);
            PlayerPrefs.SetInt("_Vehicle_Index", Database._Vehicle_Index);
            PlayerPrefs.SetInt("_Vehicle_01", Database._Vehicle_01);
            PlayerPrefs.SetInt("_Vehicle_02", Database._Vehicle_02);
            PlayerPrefs.SetInt("_Vehicle_03", Database._Vehicle_03);
            PlayerPrefs.SetInt("_Settings_Music", Database._Settings_Music);
            PlayerPrefs.SetInt("_Settings_SoundEffect", Database._Settings_SoundEffect);
            PlayerPrefs.SetInt("_Settings_Vibration", Database._Settings_Vibration);
        }

        private void Load()
        {
            Database._Initialization = PlayerPrefs.GetInt("_Initialization");
            Database._Play_01 = PlayerPrefs.GetInt("_Play_01");
            Database._Play_01_Unlock_00 = PlayerPrefs.GetInt("_Play_01_Unlock_00");
            Database._Play_02 = PlayerPrefs.GetInt("_Play_02");
            Database._Play_02_Unlock_00 = PlayerPrefs.GetInt("_Play_02_Unlock_00");
            Database._Play_02_Unlock_01 = PlayerPrefs.GetInt("_Play_02_Unlock_01");
            Database._Vehicle_Index = PlayerPrefs.GetInt("_Vehicle_Index");
            Database._Vehicle_01 = PlayerPrefs.GetInt("_Vehicle_01");
            Database._Vehicle_02 = PlayerPrefs.GetInt("_Vehicle_02");
            Database._Vehicle_03 = PlayerPrefs.GetInt("_Vehicle_03");
            Database._Settings_Music = PlayerPrefs.GetInt("_Settings_Music");
            Database._Settings_SoundEffect = PlayerPrefs.GetInt("_Settings_SoundEffect");
            Database._Settings_Vibration = PlayerPrefs.GetInt("_Settings_Vibration");
        }

        private void Init()
        {
            if (Database._Initialization == 0)
            {
                Database._Initialization = 1;
                Database._Play_01 = 0;
                Database._Play_01_Unlock_00 = 0;
                Database._Play_02 = 0;
                Database._Play_02_Unlock_00 = 0;
                Database._Play_02_Unlock_01 = 0;
                Database._Vehicle_Index = -1;
                Database._Vehicle_01 = 0;
                Database._Vehicle_02 = 0;
                Database._Vehicle_03 = 0;
                Database._Settings_Music = 1;
                Database._Settings_SoundEffect = 1;
                Database._Settings_Vibration = 1;
            }
        }
    }
}
