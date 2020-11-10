using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;
    [SerializeField] private Text _Text_Depth;
    [HideInInspector] public bool _InGame;
    [SerializeField] private Joystick _Joystick;
    public bool _EnableJoystick;
    public float _Time;
    [HideInInspector] public float _Meter;
    public float _MaxMeter;
    public float _BG_MaxMeter;
    public int _Result;
    [SerializeField] private Light _BG_SpotLight;
    [SerializeField] private Color _Original_BG_SL_Color;
    private float _BG_M_Max_Result;

    private void Awake() => _Instance = this;

    public void InitializeStart()
    {
        _InGame = false;
        _BG_M_Max_Result = 1 / _BG_MaxMeter;
    }
    private void Start() => InitializeStart();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (_InGame)
        {
            Diving();
            ZoneTrigger();
        }
    }

    private void Diving()
    {
        if (_Result > _MaxMeter) return;
        _Text_Depth.text = _Result.ToString("###,###") + " " + "Metres";
        _Meter += _Time * Time.deltaTime;
        _Result = Convert.ToInt32(_Meter);
        if (_Result > _BG_MaxMeter) return;
        float _r = _Original_BG_SL_Color.r - (_Result * _Original_BG_SL_Color.r * _BG_M_Max_Result);
        float _g = _Original_BG_SL_Color.g - (_Result * _Original_BG_SL_Color.g * _BG_M_Max_Result);
        float _b = _Original_BG_SL_Color.b - (_Result * _Original_BG_SL_Color.b * _BG_M_Max_Result);
        _BG_SpotLight.color = new Color(_r, _g, _b, 1.0f);
    }

    [HideInInspector] public List<bool> _ZonePoints = new List<bool>();
    private void ZoneTrigger()
    {
        if (_Result > 11000)
        {
            int _index = 14;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Timeline._Instance._FadeIn.Play();
            // Command
            // Game End
            _ZonePoints[_index] = true;
        }
        // 生態域分界
        if (_Result > 10900)
        {
            int _index = 13;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Timeline._Instance._FadeIn.Play();
            // Command
            // Boss End
            _ZonePoints[_index] = true;
        }
        if (_Result > 10000)
        {
            int _index = 12;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            // Command
            // Boss
            _ZonePoints[_index] = true;
        }
        if (_Result > 8900)
        {
            int _index = 11;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        // 生態域分界
        if (_Result > 8000)
        {
            int _index = 10;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Timeline._Instance._FadeIn.Play();
            // Command
            // Disable witch.
            _ZonePoints[_index] = true;
        }
        if (_Result > 7100)
        {
            int _index = 9;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            // Command
            // Enable witch.
            // 指向攻擊生物比例降低
            // 定向攻擊生物比例不變
            _ZonePoints[_index] = true;
        }
        if (_Result > 6900)
        {
            int _index = 8;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 5900)
        {
            int _index = 7;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 4900)
        {
            int _index = 6;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        // 生態域分界
        if (_Result > 4000)
        {
            int _index = 5;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Timeline._Instance._FadeIn.Play();
            _ZonePoints[_index] = true;
        }
        if (_Result > 2900)
        {
            int _index = 4;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 2000)
        {
            int _index = 3;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            // Command
            // 海洋補給品
            // 每 120 秒隨機抽樣
            // 30 % Red
            // 50 % Yellow
            // 20 % Pass
            SuppliesControl._RecoveryAll = false;
            StartCoroutine(SuppliesManager._Instance.CallSupplies(120.0f));
            _ZonePoints[_index] = true;
        }
        if (_Result > 1900)
        {
            int _index = 2;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 900)
        {
            int _index = 1;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 0)
        {
            int _index = 0;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            EnemyManager._Instance._BreakSpawnNpcLoop = false;
            StartCoroutine(EnemyManager._Instance.SpawnNpc(EnemyManager._Instance._Fish_00_Pool, true, EnemyManager.EnemyType.Null, true, 0.25f, 1, 0.0f));
            StartCoroutine(EnemyManager._Instance.SpawnNpc(EnemyManager._Instance._Fish_01_Pool, true, EnemyManager.EnemyType.Null, true, 0.25f, 1, 0.0f));
            StartCoroutine(EnemyManager._Instance.SpawnNpc(EnemyManager._Instance._Fish_02_Pool, true, EnemyManager.EnemyType.Null, true, 0.25f, 1, 0.0f));
            StartCoroutine(EnemyManager._Instance.SpawnNpc(EnemyManager._Instance._Fish_03_Pool, true, EnemyManager.EnemyType.Null, true, 0.25f, 1, 0.0f));
            StartCoroutine(EnemyManager._Instance.SpawnNpc(EnemyManager._Instance._Fish_04_Pool, true, EnemyManager.EnemyType.Null, true, 0.25f, 1, 0.0f));
            //StartCoroutine(SuppliesManager._Instance.CallSupplies(1.0f));
            _ZonePoints[_index] = true;
        }
    }

    public void IdlePlay()
    {
        Timeline._Instance._Idle.Play();
        MenuSystem._Instance.StateChange(MenuSystem.Status.MainMenu);
    }
    public void GameStartInitialize()
    {
        MenuSystem._Instance.StateChange(MenuSystem.Status.InGame);
        _InGame = true;
    }
    public void OnMainMenu()
    {
        Timeline._Instance._Idle.Play();
        MenuSystem._Instance.StateChange(MenuSystem.Status.MainMenu);
    }
    public void UpdateZone()
    {
        // Command
    }
    public void Transition()
    {
        //EnemyManager._Instance._BreakSpawnNpcLoop = true;
        EnemyAI._RecoveryAll = true;
    }
    public void ResumeGame()
    {
        _InGame = true;
        EnemyAI._RecoveryAll = false;
    }
    public void PauseGame()
    {
        _InGame = false;
        _Joystick.background.gameObject.SetActive(false);
    }
    public void ReGameLogic()
    {
        _Meter = 0.0f;
        _Result = 0;
        for (int _i = 0; _i < _ZonePoints.Count; _i++) _ZonePoints[_i] = false;
        BackgroundControl._RecoveryAll = true;
        Player._Instance._Transform_Player.position = Vector2.zero;
        Player._Instance.InitializeStart();
        Player._Instance._Animator.SetBool("Death", false);
        Player._Instance._EnableSkill = false;
        CameraControl._Instance._Transform_Camera.position = new Vector3(Vector2.zero.x, Vector2.zero.y, CameraControl._Instance._Transform_Camera.position.z);
        BaitControl._RecoveryAll = true;
        SuppliesControl._RecoveryAll = true;
        EnemyManager._Instance._BreakSpawnNpcLoop = true;
        EnemyAI._RecoveryAll = true;
        LightRays2DControl._Instance.InitializeStart();
    }

    private void OnApplicationQuit() => print("OnApplicationQuit()");

    // --------------------

    float energy;    //meter->米.公尺  energy->能量(放技能用
    int time, hp;    //time->每秒+幾米
    GameObject player;
    bool isStart, isSkill_Time, isSkill_Light, isSkill_Camera;
    string fileName, LoadData;
    List<int> save_GameObjectID = new List<int>();
    List<int> load_GameObjectID = new List<int>();
    public void Skill()    //每個技能都要有energy消耗
    {
        if (isSkill_Time)    //時間控制
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (isSkill_Light)    //燈光範圍增加
        {

        }

        if (isSkill_Camera)    //周圍生物拍照收錄圖鑑
        {

        }

        if (energy < 100)    //能量回復
        {
            energy += 1 * Time.deltaTime;
        }
        else
        {
            energy = 100;
        }
    }
    public void Save()
    {
        save_GameObjectID.Clear();

        Data data = new Data
        {
            gameObject_ID = save_GameObjectID
        };

        string jsonInfo = JsonUtility.ToJson(data, true);

        File.WriteAllText(Application.persistentDataPath + "/" + fileName, jsonInfo);

        Debug.LogWarning("寫入完成");
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            LoadData = File.ReadAllText(Application.persistentDataPath + "/" + fileName);

            Data ID = JsonUtility.FromJson<Data>(LoadData);

            load_GameObjectID = ID.gameObject_ID;

            Debug.LogWarning("讀取完成");
        }
    }
}
