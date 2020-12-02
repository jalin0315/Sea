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
    [SerializeField] private Slider _Slider_Depth;
    [HideInInspector] public bool _InGame;
    [SerializeField] private Joystick _Joystick;
    public bool _EnableJoystick;
    public bool _Enable_Vibrate;
    public float _Time;
    public float _Meter;
    public float _MaxMeter;
    [HideInInspector] public int _Result;
    [HideInInspector] public int _ResurrectTotal;
    public float _Light_MaxMeter;
    private float _Light_Result;
    [SerializeField] private Light _Light_BG;
    [SerializeField] private Color _Color_BG_Original;
    [SerializeField] private List<Light> _List_AmbientLights_00;
    [SerializeField] private Color _Color_AmbientLights_00_Original;
    [SerializeField] private GameObject _Object_Background_DeepSea;
    [SerializeField] private GameObject _Object_AmbientLight;
    [SerializeField] private GameObject _Object_PlayerLight;
    [SerializeField] private int[] _ZoneClassPoints;
    [SerializeField] private bool[] _ZonePoints;

    private void Awake() => _Instance = this;

    private void InitializeStart()
    {
        _InGame = false;
        _Result = Convert.ToInt32(_Meter);
        _ResurrectTotal = 1;
        MenuSystem._Instance._Text_ResurrectTotal.text = "x" + " " + _ResurrectTotal.ToString();
        _Light_Result = 1 / _Light_MaxMeter;
        _Object_Background_DeepSea.SetActive(false);
        _Object_AmbientLight.SetActive(true);
        _Object_PlayerLight.SetActive(false);
        _ZoneClassPoints = new int[]
        {
            0,
            600,
            900,
            1000,
            1900,
            2000,
            2900,
            3000,
            4000,
            4900,
            5000,
            5900,
            6000,
            6900,
            7000,
            7100,
            8000,
            8900,
            9000,
            10000,
            10900,
            11000
        };
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
        //_Text_Depth.text = _Result.ToString("###,###") + " " + "Metres";
        _Slider_Depth.value = _Result;
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

    public void FixZoneTrigger()
    {
        _ZonePoints = new bool[_ZoneClassPoints.Length];
        for (int _i = 0; _i < _ZoneClassPoints.Length; _i++)
        {
            if (_Result > _ZoneClassPoints[_i] && !_ZonePoints[_i])
                _ZonePoints[_i] = true;
        }
    }
    private void ZoneTrigger()
    {
        for (int _i = 0; _i < _ZoneClassPoints.Length; _i++)
        {
            if (_Result > _ZoneClassPoints[_i] && !_ZonePoints[_i])
            {
                switch (_ZoneClassPoints[_i])
                {
                    case 0:
                        EnemyManager._Instance.IEnumeratorSpawnNpcBackground(true);
                        EnemyManager._Instance.IEnumeratorSpawnNpc00(true);
                        SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                        SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                        BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
                        break;
                    case 600:
                        EnemyManager._Instance.IEnumeratorSpawnNpc00(false);
                        EnemyManager._Instance.IEnumeratorSpawnNpc01(true);
                        break;
                    case 900:
                        BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
                        break;
                    case 1000:
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        break;
                    case 1900:
                        BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
                        break;
                    case 2000:
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        EnemyManager._Instance.IEnumeratorSpawnNpc01(false);
                        EnemyManager._Instance.IEnumeratorSpawnNpc02(true);
                        break;
                    case 2900:
                        BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
                        break;
                    case 3000:
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        break;
                    case 4000:
                        // 生態域分界
                        if (!Timeline._Instance._SkipEnable) Timeline._Instance._FadeIn.Play();
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        EnemyManager._Instance.IEnumeratorSpawnNpcBackground(true);
                        EnemyManager._Instance.IEnumeratorSpawnNpc02(false);
                        SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                        SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                        break;
                    case 4900:
                        BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
                        break;
                    case 5000:
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        break;
                    case 5900:
                        BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
                        break;
                    case 6000:
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        _Object_PlayerLight.SetActive(true);
                        break;
                    case 6900:
                        BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
                        break;
                    case 7000:
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        break;
                    case 7100:
                        // Command
                        // Enable witch.
                        // 指向攻擊生物比例降低
                        // 定向攻擊生物比例不變
                        break;
                    case 8000:
                        // 生態域分界
                        // Command
                        // Disable witch.
                        if (!Timeline._Instance._SkipEnable) Timeline._Instance._FadeIn.Play();
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        EnemyManager._Instance.IEnumeratorSpawnNpcBackground(true);
                        EnemyManager._Instance.IEnumeratorSpawnNpc03(false);
                        SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                        SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                        break;
                    case 8900:
                        BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
                        break;
                    case 9000:
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        break;
                    case 10000:
                        // Command
                        // Boss
                        Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                        EnemyManager._Instance.IEnumeratorSpawnNpc04(false);
                        break;
                    case 10900:
                        // 生態域分界
                        // Command
                        // Boss End
                        if (!Timeline._Instance._SkipEnable) Timeline._Instance._FadeIn.Play();
                        break;
                    case 11000:
                        // Command
                        // Game End
                        if (!Timeline._Instance._SkipEnable) Timeline._Instance._FadeIn.Play();
                        break;
                    default:
                        Debug.LogWarningFormat("階層錯誤！當前層級數 {0}", _ZoneClassPoints[_i]);
                        break;
                }
                _ZonePoints[_i] = true;
            }
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
        if (_Result >= 10900) return;
        if (_Result >= 8000)
        {
            EnemyManager._Instance.IEnumeratorSpawnNpc04(true);
            return;
        }
        if (_Result >= 4000)
        {
            EnemyManager._Instance.IEnumeratorSpawnNpc03(true);
            return;
        }
    }
    public void Transition()
    {
        CameraControl._Instance._Camera.orthographicSize = _Result * (3.5f / _MaxMeter) + 6.5f;
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
        EnemyAI._RecoveryAll = true;
        // 生態域分界
        if (_Result >= 8000)
        {
            _Object_Background_DeepSea.SetActive(true);
            _Object_AmbientLight.SetActive(false);
            _Object_PlayerLight.SetActive(true);
        }
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
    public void ResurrectControl(int _variable)
    {
        _ResurrectTotal += _variable;
        MenuSystem._Instance._Text_ResurrectTotal.text = "x" + " " + _ResurrectTotal.ToString();
    }
    public void ReGameLogic()
    {
        _Meter = 0.0f;
        _Result = 0;
        InitializeStart();
        Player._Instance._Transform_Player.position = Vector2.zero;
        Player._Instance.InitializeStart();
        Player._Instance._Animator.SetBool("Death", false);
        Player._Instance._EnableSkill = false;
        CameraControl._Instance._Transform_Camera.position = new Vector3(Vector2.zero.x, Vector2.zero.y, CameraControl._Instance._Transform_Camera.position.z);
        EnemyManager._Instance.IEnumeratorStopAllCoroutines();
        EnemyAI._RecoveryAll = true;
        SuppliesManager._Instance.IEnumeratorStopAllCoroutines();
        SuppliesControl._RecoveryAll = true;
        BaitControl._RecoveryAll = true;
        //LightRays2DControl._Instance.InitializeStart();
        BackgroundControl._RecoveryAll = true;
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
    private void ZoneTrigger_Old()
    {
        /*
        if (_Result > 11000)
        {
            int _index = 21;
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
            int _index = 20;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Timeline._Instance._FadeIn.Play();
            // Command
            // Boss End
            _ZonePoints[_index] = true;
        }
        if (_Result > 10000)
        {
            int _index = 19;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            // Command
            // Boss
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 9000)
        {
            int _index = 18;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 8900)
        {
            int _index = 17;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        // 生態域分界
        if (_Result > 8000)
        {
            int _index = 16;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            // Command
            // Disable witch.
            Timeline._Instance._FadeIn.Play();
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            EnemyManager._Instance.IEnumeratorSpawnNpc03(false);
            _ZonePoints[_index] = true;
        }
        if (_Result > 7100)
        {
            int _index = 15;
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
            int _index = 14;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 6900)
        {
            int _index = 13;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 6000)
        {
            int _index = 12;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 5900)
        {
            int _index = 11;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 5000)
        {
            int _index = 10;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 4900)
        {
            int _index = 9;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        // 生態域分界
        if (_Result > 4000)
        {
            int _index = 8;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Timeline._Instance._FadeIn.Play();
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            EnemyManager._Instance.IEnumeratorSpawnNpc02(false);
            _ZonePoints[_index] = true;
        }
        if (_Result > 3000)
        {
            int _index = 7;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            _ZonePoints[_index] = true;
        }
        if (_Result > 2900)
        {
            int _index = 6;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 2000)
        {
            int _index = 5;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            EnemyManager._Instance.IEnumeratorSpawnNpc01(false);
            EnemyManager._Instance.IEnumeratorSpawnNpc02(true);
            _ZonePoints[_index] = true;
        }
        if (_Result > 1900)
        {
            int _index = 4;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        if (_Result > 1000)
        {
            int _index = 3;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            Player._Instance._Slider_MaxHealth.value -= Player._Instance._MaxHealthLess;
            StartCoroutine(SuppliesManager._Instance._CallSupplies_Singleton);
            _ZonePoints[_index] = true;
        }
        if (_Result > 900)
        {
            int _index = 2;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }

        if (_Result > 600)
        {
            int _index = 1;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            EnemyManager._Instance.IEnumeratorSpawnNpc00(false);
            EnemyManager._Instance.IEnumeratorSpawnNpc01(true);
            _ZonePoints[_index] = true;
        }
        if (_Result > 0)
        {
            int _index = 0;
            if (_ZonePoints.Count - 1 < _index) _ZonePoints.Add(false);
            if (_ZonePoints[_index]) return;
            EnemyManager._Instance.IEnumeratorSpawnNpcBackground(true);
            EnemyManager._Instance.IEnumeratorSpawnNpc00(true);
            StartCoroutine(SuppliesManager._Instance._CallSuppliesAd_Singleton);
            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Background_00_Pool);
            _ZonePoints[_index] = true;
        }
        */
    }
}
