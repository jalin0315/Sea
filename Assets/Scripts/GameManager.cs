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
    public bool _Enable_Vibrate;
    public float _Time;
    [HideInInspector] public float _Meter;
    public float _MaxMeter;
    public int _Result;
    public float _Light_MaxMeter;
    private float _Light_Result;
    [SerializeField] private Light _Light_BG;
    [SerializeField] private Color _Color_BG_Original;
    [SerializeField] private List<Light> _List_AmbientLights_00;
    [SerializeField] private Color _Color_AmbientLights_00_Original;
    [SerializeField] private GameObject _Object_Background_DeepSea;
    [SerializeField] private GameObject _Object_AmbientLight;
    [SerializeField] private GameObject _Object_PlayerLight;

    private void Awake() => _Instance = this;

    private void InitializeStart()
    {
        _InGame = false;
        _Light_Result = 1 / _Light_MaxMeter;
        _Object_Background_DeepSea.SetActive(false);
        _Object_AmbientLight.SetActive(true);
        _Object_PlayerLight.SetActive(false);
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
        if (_Result > _Light_MaxMeter) return;
        float _bg_r = _Color_BG_Original.r - (_Result * _Color_BG_Original.r * _Light_Result);
        float _bg_g = _Color_BG_Original.g - (_Result * _Color_BG_Original.g * _Light_Result);
        float _bg_b = _Color_BG_Original.b - (_Result * _Color_BG_Original.b * _Light_Result);
        _Light_BG.color = new Color(_bg_r, _bg_g, _bg_b, 1.0f);
        float _lm_00_r = _Color_AmbientLights_00_Original.r - (_Result * _Color_AmbientLights_00_Original.r * _Light_Result);
        float _lm_00_g = _Color_AmbientLights_00_Original.g - (_Result * _Color_AmbientLights_00_Original.g * _Light_Result);
        float _lm_00_b = _Color_AmbientLights_00_Original.b - (_Result * _Color_AmbientLights_00_Original.b * _Light_Result);
        for (int _i = 0; _i < _List_AmbientLights_00.Count; _i++)
        {
            _List_AmbientLights_00[_i].color = new Color(_lm_00_r, _lm_00_g, _lm_00_b, 1.0f);
        }
    }

    private List<bool> _ZonePoints = new List<bool>();
    private void ZoneTrigger()
    {
        if (_Result > 11000)
        {
            int _index = 20;
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
            int _index = 19;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Timeline._Instance._FadeIn.Play();
            // Command
            // Boss End
            _ZonePoints[_index] = true;
        }
        if (_Result > 10000)
        {
            int _index = 18;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            // Command
            // Boss
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 9000)
        {
            int _index = 17;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }

        if (_Result > 8900)
        {
            int _index = 16;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        // 生態域分界
        if (_Result > 8000)
        {
            int _index = 15;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            // Command
            // Disable witch.
            Timeline._Instance._FadeIn.Play();
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 7100)
        {
            int _index = 14;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            // Command
            // Enable witch.
            // 指向攻擊生物比例降低
            // 定向攻擊生物比例不變
            _ZonePoints[_index] = true;
        }
        if (_Result > 7000)
        {
            int _index = 13;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 6900)
        {
            int _index = 12;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 6000)
        {
            int _index = 11;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 5900)
        {
            int _index = 10;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 5000)
        {
            int _index = 9;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 4900)
        {
            int _index = 8;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        // 生態域分界
        if (_Result > 4000)
        {
            int _index = 7;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Timeline._Instance._FadeIn.Play();
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 3000)
        {
            int _index = 6;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 2900)
        {
            int _index = 5;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 2000)
        {
            int _index = 4;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 1900)
        {
            int _index = 3;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundControl._RecoveryAll = false;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 1000)
        {
            int _index = 2;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            StartCoroutine(SuppliesManager._Instance.CallSupplies(120.0f));
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
            StartCoroutine(EnemyManager._Instance.SpawnNpc(EnemyManager._Instance._Fish_Pool, true, EnemyManager.EnemyType.Null, true, 0.5f, 1, 0.0f));
            SuppliesControl._RecoveryAll = false;
            StartCoroutine(SuppliesManager._Instance.CallSuppliesAd(60.0f)); // 60.0f
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
    private List<bool> _UpdateZonePoints = new List<bool>();
    public void UpdateZone()
    {
        if (_Result > 8000)
        {
            int _index = 1;
            if (_UpdateZonePoints.Count - 1 < _index) _UpdateZonePoints.Add(false);
            if (_UpdateZonePoints[_index]) return;
            EnemyManager._Instance._BreakSpawnNpcLoop = false;
            StartCoroutine(EnemyManager._Instance.SpawnNpc(EnemyManager._Instance._Fish_Pool, true, EnemyManager.EnemyType.Null, true, 0.5f, 1, 0.0f));
            _UpdateZonePoints[_index] = true;
        }
        if (_Result > 4000)
        {
            int _index = 0;
            if (_UpdateZonePoints.Count - 1 < _index) _UpdateZonePoints.Add(false);
            if (_UpdateZonePoints[_index]) return;
            EnemyManager._Instance._BreakSpawnNpcLoop = false;
            StartCoroutine(EnemyManager._Instance.SpawnNpc(EnemyManager._Instance._Fish_Pool, true, EnemyManager.EnemyType.Null, true, 0.5f, 1, 0.0f));
            _UpdateZonePoints[_index] = true;
        }
    }
    private List<bool> _TransitionPoints = new List<bool>();
    public void Transition()
    {
        EnemyManager._Instance._BreakSpawnNpcLoop = true;
        EnemyAI._RecoveryAll = true;
        // 生態域分界
        if (_Result > 8000)
        {
            int _index = 0;
            if (_TransitionPoints.Count - 1 < _index) _TransitionPoints.Add(false);
            if (_TransitionPoints[_index]) return;
            _Object_Background_DeepSea.SetActive(true);
            _Object_AmbientLight.SetActive(false);
            _Object_PlayerLight.SetActive(true);
            _TransitionPoints[_index] = true;
        }
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
    public void GameState(bool _in_game)
    {
        if (_in_game)
        {
            _InGame = _in_game;
            Time.timeScale = 1.0f;
            return;
        }
        if (!_in_game)
        {
            _InGame = _in_game;
            Time.timeScale = 0.0f;
            MovementSystem._Instance._FloatingJoystick.Initialize();
            return;
        }
    }
    public void ReGameLogic()
    {
        _Meter = 0.0f;
        _Result = 0;
        InitializeStart();
        for (int _i = 0; _i < _ZonePoints.Count; _i++) _ZonePoints[_i] = false;
        for (int _i = 0; _i < _UpdateZonePoints.Count; _i++) _UpdateZonePoints[_i] = false;
        for (int _i = 0; _i < _TransitionPoints.Count; _i++) _TransitionPoints[_i] = false;
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
