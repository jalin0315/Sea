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
            Application.targetFrameRate = -1;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Load();
            Init();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetInt("_Initialization", Database.Instance._Initialization);
            PlayerPrefs.SetInt("_Play_01", Database.Instance._Play_01);
            PlayerPrefs.SetInt("_Play_01_Unlock_00", Database.Instance._Play_01_Unlock_00);
            PlayerPrefs.SetInt("_Play_02", Database.Instance._Play_02);
            PlayerPrefs.SetInt("_Play_02_Unlock_00", Database.Instance._Play_02_Unlock_00);
            PlayerPrefs.SetInt("_Play_02_Unlock_01", Database.Instance._Play_02_Unlock_01);
            PlayerPrefs.SetInt("_Vehicle_Index", Database.Instance._Vehicle_Index);
            PlayerPrefs.SetInt("_Vehicle_01", Database.Instance._Vehicle_01);
            PlayerPrefs.SetInt("_Vehicle_02", Database.Instance._Vehicle_02);
            PlayerPrefs.SetInt("_Vehicle_03", Database.Instance._Vehicle_03);
            PlayerPrefs.SetInt("_Settings_Music", Database.Instance._Settings_Music);
            PlayerPrefs.SetInt("_Settings_SoundEffect", Database.Instance._Settings_SoundEffect);
            PlayerPrefs.SetInt("_Settings_Vibration", Database.Instance._Settings_Vibration);
        }

        private void Load()
        {
            Database.Instance._Initialization = PlayerPrefs.GetInt("_Initialization");
            Database.Instance._Play_01 = PlayerPrefs.GetInt("_Play_01");
            Database.Instance._Play_01_Unlock_00 = PlayerPrefs.GetInt("_Play_01_Unlock_00");
            Database.Instance._Play_02 = PlayerPrefs.GetInt("_Play_02");
            Database.Instance._Play_02_Unlock_00 = PlayerPrefs.GetInt("_Play_02_Unlock_00");
            Database.Instance._Play_02_Unlock_01 = PlayerPrefs.GetInt("_Play_02_Unlock_01");
            Database.Instance._Vehicle_Index = PlayerPrefs.GetInt("_Vehicle_Index");
            Database.Instance._Vehicle_01 = PlayerPrefs.GetInt("_Vehicle_01");
            Database.Instance._Vehicle_02 = PlayerPrefs.GetInt("_Vehicle_02");
            Database.Instance._Vehicle_03 = PlayerPrefs.GetInt("_Vehicle_03");
            Database.Instance._Settings_Music = PlayerPrefs.GetInt("_Settings_Music");
            Database.Instance._Settings_SoundEffect = PlayerPrefs.GetInt("_Settings_SoundEffect");
            Database.Instance._Settings_Vibration = PlayerPrefs.GetInt("_Settings_Vibration");
        }

        private void Init()
        {
            if (Database.Instance._Initialization == 0)
            {
                Database.Instance._Initialization = 1;
                Database.Instance._Play_01 = 0;
                Database.Instance._Play_01_Unlock_00 = 0;
                Database.Instance._Play_02 = 0;
                Database.Instance._Play_02_Unlock_00 = 0;
                Database.Instance._Play_02_Unlock_01 = 0;
                Database.Instance._Vehicle_Index = -1;
                Database.Instance._Vehicle_01 = 0;
                Database.Instance._Vehicle_02 = 0;
                Database.Instance._Vehicle_03 = 0;
                Database.Instance._Settings_Music = 1;
                Database.Instance._Settings_SoundEffect = 1;
                Database.Instance._Settings_Vibration = 1;
            }
        }
    }
}
