using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

namespace CTJ
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _Instance;
        [SerializeField] private Slider _Slider_Depth;
        public static bool _InGame;
        [SerializeField] private Joystick _Joystick;
        public bool _Enable_Joystick;
        public bool _Enable_Vibrate;
        public float _Time;
        public static float _Meter;
        public static float _MaxMeter;
        public int _ResurrectTotal;
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
        private Color _variable_color;

        private void Initialization()
        {
            _InGame = false;
            _Meter = 0.0f;
            _MaxMeter = 11000.0f;
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
            6500,
            6900,
            7000,
            7100,
            7900,
            8000,
            8900,
            9000,
            10000,
            10800,
            10900,
            11000
            };
        }

        private void Awake()
        {
            _Instance = this;
            Initialization();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                // Ask if user wants to exit
                NativeUI.AlertPopup _alert = NativeUI.ShowTwoButtonAlert("離開應用程式", "是否離開應用程式？", "確定", "取消");
                if (_alert != null)
                {
                    _alert.OnComplete += delegate (int _button)
                    {
                        if (_button == 0) Application.Quit();
                    };
                }
            }
            if (_InGame)
            {
                Diving();
                ZoneTrigger();
            }
        }

        private void Diving()
        {
            if (_Meter >= _MaxMeter) return;
            _Meter += TimeSystem._DeltaTime() * _Time;
            _Slider_Depth.value = _Meter;
            if (_Meter >= _Light_MaxMeter) return;
            _variable_color.r = _Color_BG_Original.r - (_Meter * _Color_BG_Original.r * _Light_Result);
            _variable_color.g = _Color_BG_Original.g - (_Meter * _Color_BG_Original.g * _Light_Result);
            _variable_color.b = _Color_BG_Original.b - (_Meter * _Color_BG_Original.b * _Light_Result);
            _variable_color.a = 1.0f;
            _Light_BG.color = _variable_color;
            _variable_color.r = _Color_AmbientLights_00_Original.r - (_Meter * _Color_AmbientLights_00_Original.r * _Light_Result);
            _variable_color.g = _Color_AmbientLights_00_Original.g - (_Meter * _Color_AmbientLights_00_Original.g * _Light_Result);
            _variable_color.b = _Color_AmbientLights_00_Original.b - (_Meter * _Color_AmbientLights_00_Original.b * _Light_Result);
            _variable_color.a = 1.0f;
            for (int _i = 0; _i < _List_AmbientLights_00.Count; _i++) _List_AmbientLights_00[_i].color = _variable_color;
        }

        public void FixZoneTrigger()
        {
            _ZonePoints = new bool[_ZoneClassPoints.Length];
            for (int _i = 0; _i < _ZoneClassPoints.Length; _i++)
            {
                if (_Meter > _ZoneClassPoints[_i] && !_ZonePoints[_i]) _ZonePoints[_i] = true;
            }
        }
        private void ZoneTrigger()
        {
            for (int _i = 0; _i < _ZoneClassPoints.Length; _i++)
            {
                if (_Meter > _ZoneClassPoints[_i] && !_ZonePoints[_i])
                {
                    switch (_ZoneClassPoints[_i])
                    {
                        case 0:
                            int _index = Random.Range(0, 5);
                            switch (_index)
                            {
                                case 0:
                                    AudioSystem._Instance.PlayMusic("00");
                                    break;
                                case 1:
                                    AudioSystem._Instance.PlayMusic("01");
                                    break;
                                case 2:
                                    AudioSystem._Instance.PlayMusic("02");
                                    break;
                                case 3:
                                    AudioSystem._Instance.PlayMusic("03");
                                    break;
                                case 4:
                                    AudioSystem._Instance.PlayMusic("04");
                                    break;
                                default:
                                    break;
                            }
                            EnemyManager._Instance.IEnumeratorSpawnNpc00(true);
                            EnemyManager._Instance.IEnumeratorSpawnNpcNPC(true);
                            SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                            SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_00);
                            break;
                        case 600:
                            EnemyManager._Instance.IEnumeratorSpawnNpc00(false);
                            EnemyManager._Instance.IEnumeratorSpawnNpc01(true);
                            break;
                        case 900:
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_00);
                            break;
                        case 1000:
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            break;
                        case 1900:
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_01);
                            break;
                        case 2000:
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            EnemyManager._Instance.IEnumeratorSpawnNpc01(false);
                            EnemyManager._Instance.IEnumeratorSpawnNpc02(true);
                            break;
                        case 2900:
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_01);
                            break;
                        case 3000:
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            break;
                        case 4000:
                            // 生態域分界
                            if (!Timeline._Instance._SkipEnable) Timeline._Instance._FadeIn.Play();
                            _Object_PlayerLight.SetActive(true);
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            EnemyManager._Instance.IEnumeratorSpawnNpc02(false);
                            EnemyManager._Instance.IEnumeratorSpawnNpcNPC(true);
                            SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                            SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                            break;
                        case 4900:
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_02);
                            break;
                        case 5000:
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            break;
                        case 5900:
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_02);
                            break;
                        case 6000:
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            break;
                        case 6500:
                            if (!Timeline._Instance._SkipEnable) Timeline._Instance._FadeIn.Play();
                            _Object_PlayerLight.SetActive(true);
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            EnemyManager._Instance.IEnumeratorSpawnNpc03(false);
                            SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                            SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                            break;
                        case 6900:
                            EnemyManager._Instance.IEnumeratorSpawnNpcJellyFish(false);
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_02);
                            break;
                        case 7000:
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            CameraControl._Instance.StatusChange(CameraControl.Status.Lock);
                            EnemyManager._Instance.IEnumeratorSpawnNpcHuman(true);
                            break;
                        case 7100:
                            // Command
                            // Enable witch.
                            // 指向攻擊生物比例降低
                            // 定向攻擊生物比例不變
                            break;
                        case 7900:
                            CameraControl._Instance.StatusChange(CameraControl.Status.Free);
                            EnemyManager._Instance.IEnumeratorSpawnNpcHuman(false);
                            break;
                        case 8000:
                            // 生態域分界
                            // Command
                            // Disable witch.
                            if (!Timeline._Instance._SkipEnable) Timeline._Instance._FadeIn.Play();
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            EnemyManager._Instance.IEnumeratorSpawnNpcNPC(true);
                            SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                            SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                            break;
                        case 8900:
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_02);
                            break;
                        case 9000:
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            break;
                        case 10000:
                            // Command
                            // Boss
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            CameraControl._Instance.StatusChange(CameraControl.Status.Lock);
                            EnemyManager._Instance.IEnumeratorSpawnNpc04(false);
                            EnemyManager._Instance.ReUseBoss();
                            break;
                        case 10800:
                            BossAI._Instance._Animator.SetTrigger("End");
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
            if (_Meter >= 10900) return;
            if (_Meter >= 8000)
            {
                EnemyManager._Instance.IEnumeratorSpawnNpc04(true);
                return;
            }
            if (_Meter >= 6500)
            {
                EnemyManager._Instance.IEnumeratorSpawnNpcJellyFish(true);
                return;
            }
            if (_Meter >= 4000)
            {
                EnemyManager._Instance.IEnumeratorSpawnNpc03(true);
                return;
            }
        }
        public void Transition()
        {
            CameraControl._Instance.Initialization();
            EnemyAI._Recycle = true;
            JellyFishEnemyAI._Recycle = true;
            HumanEnemyAI._Recycle = true;
            _variable_color.r = _Color_BG_Original.r - (_Meter * _Color_BG_Original.r * _Light_Result);
            _variable_color.g = _Color_BG_Original.g - (_Meter * _Color_BG_Original.g * _Light_Result);
            _variable_color.b = _Color_BG_Original.b - (_Meter * _Color_BG_Original.b * _Light_Result);
            _variable_color.a = 1.0f;
            _Light_BG.color = _variable_color;
            _variable_color.r = _Color_AmbientLights_00_Original.r - (_Meter * _Color_AmbientLights_00_Original.r * _Light_Result);
            _variable_color.g = _Color_AmbientLights_00_Original.g - (_Meter * _Color_AmbientLights_00_Original.g * _Light_Result);
            _variable_color.b = _Color_AmbientLights_00_Original.b - (_Meter * _Color_AmbientLights_00_Original.b * _Light_Result);
            _variable_color.a = 1.0f;
            for (int _i = 0; _i < _List_AmbientLights_00.Count; _i++) _List_AmbientLights_00[_i].color = _variable_color;
            // 生態域分界
            if (_Meter >= 8000)
            {
                _Object_Background_DeepSea.SetActive(true);
                _Object_AmbientLight.SetActive(false);
                _Object_PlayerLight.SetActive(true);
                return;
            }
            if (_Meter >= 4000) return;
            if (_Meter >= 0)
            {
                LightRays2DControl._Instance.Initialization(true);
                return;
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
                TimeSystem.TimeScale(1.0f);
                return;
            }
            if (!_in_game)
            {
                _InGame = _in_game;
                TimeSystem.TimeScale(0.0f);
                MovementSystem._Instance._FloatingJoystick.Initialize();
                MovementSystem._Instance._DynamicJoystick.Initialize();
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
            Initialization();
            Player._Instance._Transform.position = Vector2.zero;
            Player._Instance.Initialization();
            Player._Instance._Animator.SetBool("Death", false);
            Player._Instance._EnableSkill = false;
            CameraControl._Instance.Initialization();
            EnemyManager._Instance.IEnumeratorStopAllCoroutines();
            EnemyAI._Recycle = true;
            JellyFishEnemyAI._Recycle = true;
            HumanEnemyAI._Recycle = true;
            BossLegAI._Recycle = true;
            BossAI._Instance.RecycleThis();
            NPC._Recycle = true;
            SuppliesManager._Instance.IEnumeratorStopAllCoroutines();
            SuppliesControl._Recycle = true;
            BaitControl._RecoveryAll = true;
            BackgroundControl._RecoveryAll = true;
            LightRays2DControl._Instance.Initialization(false);
            AudioSystem._Instance.StopAll();
        }

        private void OnApplicationQuit() => print("OnApplicationQuit()");
    }
}
