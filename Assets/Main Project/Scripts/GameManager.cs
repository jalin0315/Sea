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
        public float _Color_MaxMeter;
        private float _Color_Result;
        [SerializeField] private SpriteRenderer _Background;
        [SerializeField] private Color _Color_BG_Original;
        [SerializeField] private Color _Color_AmbientLights_00_Original;
        [SerializeField] private int[] _ZoneClassPoints;
        [SerializeField] private bool[] _ZonePoints;
        public int _MusicIndex;
        public bool _End;
        private Color _variable_color;

        private void Initialization()
        {
            _InGame = false;
            _Meter = 0.0f;
            _MaxMeter = 11000.0f;
            _ResurrectTotal = 1;
            MenuSystem._Instance._Text_ResurrectTotal.text = "x" + " " + _ResurrectTotal.ToString();
            _Color_Result = 1 / _Color_MaxMeter;
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
            9500,
            9900,
            10000,
            10800,
            10900,
            10950,
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
            if (!_InGame) return;
            Diving();
            ZoneTrigger();
        }

        private void Diving()
        {
            if (_Meter >= _MaxMeter) return;
            _Meter += TimeSystem._DeltaTime() * _Time;
            _Slider_Depth.value = _Meter;
            if (_Meter >= _Color_MaxMeter) return;
            _variable_color.r = _Color_BG_Original.r - (_Meter * _Color_BG_Original.r * _Color_Result);
            _variable_color.g = _Color_BG_Original.g - (_Meter * _Color_BG_Original.g * _Color_Result);
            _variable_color.b = _Color_BG_Original.b - (_Meter * _Color_BG_Original.b * _Color_Result);
            _variable_color.a = 1.0f;
            _Background.color = _variable_color;
            /*
            _variable_color.r = _Color_AmbientLights_00_Original.r - (_Meter * _Color_AmbientLights_00_Original.r * _Color_Result);
            _variable_color.g = _Color_AmbientLights_00_Original.g - (_Meter * _Color_AmbientLights_00_Original.g * _Color_Result);
            _variable_color.b = _Color_AmbientLights_00_Original.b - (_Meter * _Color_AmbientLights_00_Original.b * _Color_Result);
            _variable_color.a = 1.0f;
            */
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
                            EnemyManager._Instance.IEnumeratorSpawnNpc00(true);
                            EnemyManager._Instance.IEnumeratorSpawnNpcNPC(true);
                            SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                            SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                            // 生成海底地形
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_00);
                            break;
                        case 600:
                            EnemyManager._Instance.IEnumeratorSpawnNpc00(false);
                            EnemyManager._Instance.IEnumeratorSpawnNpc01(true);
                            break;
                        case 900:
                            // 生成海底地形
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_01);
                            break;
                        case 1000:
                            break;
                        case 1900:
                            // 生成海底地形
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_02);
                            break;
                        case 2000:
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            EnemyManager._Instance.IEnumeratorSpawnNpc01(false);
                            EnemyManager._Instance.IEnumeratorSpawnNpc02(true);
                            break;
                        case 2900:
                            // 生成海底地形
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_03);
                            break;
                        case 3000:
                            break;
                        case 4000:
                            switch (_MusicIndex)
                            {
                                case 0:
                                    AudioSystem._Instance.FadeMusic("ZoneOne00", 0.0f, 3.0f);
                                    break;
                                case 1:
                                    AudioSystem._Instance.FadeMusic("ZoneOne01", 0.0f, 3.0f);
                                    break;
                                case 2:
                                    AudioSystem._Instance.FadeMusic("ZoneOne02", 0.0f, 3.0f);
                                    break;
                            }
                            // 生態域分界
                            if (!Timeline._Instance._SkipEnable) Timeline._Instance._FadeIn.Play();
                            //_Object_PlayerLight.SetActive(true);
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            EnemyManager._Instance.IEnumeratorSpawnNpc02(false);
                            EnemyManager._Instance.IEnumeratorSpawnNpcNPC(true);
                            SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                            SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                            Database._Play_01 = 1;
                            Database._Vehicle_01 = 1;
                            break;
                        case 4900:
                            // 生成海底地形
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_04);
                            break;
                        case 5000:
                            break;
                        case 5900:
                            // 生成海底地形
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_05);
                            break;
                        case 6000:
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            break;
                        case 6500:
                            //_Object_PlayerLight.SetActive(true);
                            EnemyManager._Instance.IEnumeratorSpawnNpcJellyFish(true);
                            EnemyManager._Instance.IEnumeratorSpawnNpc03(false);
                            SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                            SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                            break;
                        case 6900:
                            switch (_MusicIndex)
                            {
                                case 0:
                                    AudioSystem._Instance.FadeMusic("ZoneTwo00", 0.0f, 5.0f);
                                    break;
                                case 1:
                                    AudioSystem._Instance.FadeMusic("ZoneTwo01", 0.0f, 5.0f);
                                    break;
                                case 2:
                                    AudioSystem._Instance.FadeMusic("ZoneTwo02", 0.0f, 5.0f);
                                    break;
                            }
                            EnemyManager._Instance.IEnumeratorSpawnNpcJellyFish(false);
                            // 生成海底地形
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_06);
                            break;
                        case 7000:
                            AudioSystem._Instance.PlayMusic("Boss00");
                            AudioSystem._Instance.FadeMusic("Boss00", 1.0f, 2.0f);
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
                            AudioSystem._Instance.FadeMusic("Boss00", 0.0f, 2.0f);
                            CameraControl._Instance.StatusChange(CameraControl.Status.Free);
                            EnemyManager._Instance.IEnumeratorSpawnNpcHuman(false);
                            break;
                        case 8000:
                            // 生態域分界
                            // Command
                            if (!Timeline._Instance._SkipEnable) Timeline._Instance._FadeIn.Play();
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            EnemyManager._Instance.IEnumeratorSpawnNpcNPC(true);
                            SuppliesManager._Instance.IEnumeratorCallSupplies(true);
                            SuppliesManager._Instance.IEnumeratorCallSuppliesAd(true);
                            Database._Play_02 = 1;
                            Database._Vehicle_02 = 1;
                            break;
                        case 8900:
                            // 生成海底地形
                            BackgroundManager._Instance.ReUse(BackgroundManager._Instance._Pool_Background_07);
                            break;
                        case 9500:
                            EnemyManager._Instance.IEnumeratorSpawnNpc04(false);
                            EnemyManager._Instance.IEnumeratorSpawnNpcClioneLimacina(true);
                            break;
                        case 9900:
                            switch (_MusicIndex)
                            {
                                case 0:
                                    AudioSystem._Instance.FadeMusic("ZoneThree00", 0.0f, 3.0f);
                                    break;
                                case 1:
                                    AudioSystem._Instance.FadeMusic("ZoneThree01", 0.0f, 3.0f);
                                    break;
                            }
                            EnemyManager._Instance.IEnumeratorSpawnNpcClioneLimacina(false);
                            break;
                        case 10000:
                            // Command
                            // Boss 出場
                            AudioSystem._Instance.PlayMusic("Boss01");
                            AudioSystem._Instance.FadeMusic("Boss01", 1.0f, 2.0f);
                            Player._Instance.VerifyHealth(_ZoneClassPoints[_i]);
                            CameraControl._Instance.StatusChange(CameraControl.Status.Lock);
                            EnemyManager._Instance.ReUseBoss();
                            break;
                        case 10800:
                            BossAI._Instance._Animator.SetTrigger("End");
                            break;
                        case 10900:
                            AudioSystem._Instance.FadeMusic("Boss01", 0.0f, 3.0f);
                            // 生態域分界
                            // Command
                            // Boss End
                            break;
                        case 10950:
                            _MusicIndex = Random.Range(0, 2);
                            switch (_MusicIndex)
                            {
                                case 0:
                                    AudioSystem._Instance.PlayMusic("End00");
                                    AudioSystem._Instance.FadeMusic("End00", 1.0f, 2.0f);
                                    break;
                                case 1:
                                    AudioSystem._Instance.PlayMusic("End01");
                                    AudioSystem._Instance.FadeMusic("End01", 1.0f, 2.0f);
                                    break;
                            }
                            break;
                        case 11000:
                            // Command
                            // Game End
                            MovementSystem._Instance._DynamicJoystick.Initialization();
                            MenuSystem._Instance.StateChange(MenuSystem.Status.Animation);
                            TimeSystem.TimeScale(1.0f);
                            _InGame = false;
                            Timeline._Instance._End.Play();
                            _End = true;
                            Database._Vehicle_03 = 1;
                            break;
                        default:
                            Logger.LogWarningFormat("階層錯誤！當前層級數 {0}", _ZoneClassPoints[_i]);
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
            MenuSystem._Instance.StateChange(MenuSystem.Status.InGameMenu);
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
            _variable_color.r = _Color_BG_Original.r - (_Meter * _Color_BG_Original.r * _Color_Result);
            _variable_color.g = _Color_BG_Original.g - (_Meter * _Color_BG_Original.g * _Color_Result);
            _variable_color.b = _Color_BG_Original.b - (_Meter * _Color_BG_Original.b * _Color_Result);
            _variable_color.a = 1.0f;
            _Background.color = _variable_color;
            /*
            _variable_color.r = _Color_AmbientLights_00_Original.r - (_Meter * _Color_AmbientLights_00_Original.r * _Color_Result);
            _variable_color.g = _Color_AmbientLights_00_Original.g - (_Meter * _Color_AmbientLights_00_Original.g * _Color_Result);
            _variable_color.b = _Color_AmbientLights_00_Original.b - (_Meter * _Color_AmbientLights_00_Original.b * _Color_Result);
            _variable_color.a = 1.0f;
            */
            // 生態域分界
            PlayMusic();
        }
        public void StopMusic()
        {
            if (_Meter >= 8000.0f)
            {
                switch (_MusicIndex)
                {
                    case 0:
                        AudioSystem._Instance.FadeMusic("ZoneThree00", 0.0f, 3.0f);
                        break;
                    case 1:
                        AudioSystem._Instance.FadeMusic("ZoneThree01", 0.0f, 3.0f);
                        break;
                }
                return;
            }
            if (_Meter >= 4000.0f)
            {
                switch (_MusicIndex)
                {
                    case 0:
                        AudioSystem._Instance.FadeMusic("ZoneTwo00", 0.0f, 5.0f);
                        break;
                    case 1:
                        AudioSystem._Instance.FadeMusic("ZoneTwo01", 0.0f, 5.0f);
                        break;
                    case 2:
                        AudioSystem._Instance.FadeMusic("ZoneTwo02", 0.0f, 5.0f);
                        break;
                }
                return;
            }
            if (_Meter >= 0.0f)
            {
                switch (_MusicIndex)
                {
                    case 0:
                        AudioSystem._Instance.FadeMusic("ZoneOne00", 0.0f, 3.0f);
                        break;
                    case 1:
                        AudioSystem._Instance.FadeMusic("ZoneOne01", 0.0f, 3.0f);
                        break;
                    case 2:
                        AudioSystem._Instance.FadeMusic("ZoneOne02", 0.0f, 3.0f);
                        break;
                }
                return;
            }
        }
        public void PlayMusic()
        {
            if (_Meter >= 10000.0f)
            {
                return;
            }
            if (_Meter >= 8000.0f)
            {
                _MusicIndex = Random.Range(0, 2);
                switch (_MusicIndex)
                {
                    case 0:
                        AudioSystem._Instance.PlayMusic("ZoneThree00");
                        AudioSystem._Instance.FadeMusic("ZoneThree00", 1.0f, 2.0f);
                        break;
                    case 1:
                        AudioSystem._Instance.PlayMusic("ZoneThree01");
                        AudioSystem._Instance.FadeMusic("ZoneThree01", 1.0f, 2.0f);
                        break;
                }
                return;
            }
            if (_Meter >= 7000.0f)
            {
                return;
            }
            if (_Meter >= 4000.0f)
            {
                _MusicIndex = Random.Range(0, 3);
                switch (_MusicIndex)
                {
                    case 0:
                        AudioSystem._Instance.PlayMusic("ZoneTwo00");
                        AudioSystem._Instance.FadeMusic("ZoneTwo00", 1.0f, 2.0f);
                        break;
                    case 1:
                        AudioSystem._Instance.PlayMusic("ZoneTwo01");
                        AudioSystem._Instance.FadeMusic("ZoneTwo01", 1.0f, 2.0f);
                        break;
                    case 2:
                        AudioSystem._Instance.PlayMusic("ZoneTwo02");
                        AudioSystem._Instance.FadeMusic("ZoneTwo02", 1.0f, 2.0f);
                        break;
                }
                return;
            }
            if (_Meter >= 0.0f)
            {
                _MusicIndex = Random.Range(0, 3);
                switch (_MusicIndex)
                {
                    case 0:
                        AudioSystem._Instance.PlayMusic("ZoneOne00");
                        AudioSystem._Instance.FadeMusic("ZoneOne00", 1.0f, 2.0f);
                        break;
                    case 1:
                        AudioSystem._Instance.PlayMusic("ZoneOne01");
                        AudioSystem._Instance.FadeMusic("ZoneOne01", 1.0f, 2.0f);
                        break;
                    case 2:
                        AudioSystem._Instance.PlayMusic("ZoneOne02");
                        AudioSystem._Instance.FadeMusic("ZoneOne02", 1.0f, 2.0f);
                        break;
                }
                return;
            }
        }
        public void PauseGame()
        {
            _InGame = false;
            MovementSystem._Instance._DynamicJoystick.Initialization();
            MovementSystem._Instance.Initialization();
        }
        public void GameState(bool _in_game)
        {
            _InGame = _in_game;
            if (_InGame)
            {
                if (Player._Instance._SlowMotionActivity) TimeSystem.TimeScale(Player._Instance._SlowMotionActivityTime);
                else TimeSystem.TimeScale(1.0f);
                return;
            }
            else
            {
                TimeSystem.TimeScale(0.0f);
                MovementSystem._Instance._DynamicJoystick.Initialization();
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
            Player._Instance.Initialization();
            MovementSystem._Instance.Initialization();
            CameraControl._Instance.Initialization();
            EnemyManager._Instance.IEnumeratorStopAllCoroutines();
            EnemyAI._Recycle = true;
            JellyFishEnemyAI._Recycle = true;
            ClioneLimacina._Recycle = true;
            HumanEnemyAI._Recycle = true;
            BossLegAI._Recycle = true;
            BossAI._Instance.RecycleThis();
            NPC._Recycle = true;
            SuppliesManager._Instance.IEnumeratorStopAllCoroutines();
            SuppliesControl._Recycle = true;
            BaitControl._RecoveryAll = true;
            BackgroundControl._RecoveryAll = true;
            if (_End) return;
            AudioSystem._Instance.StopAll();
            AudioSystem._Instance.PlayMusic("Home00");
            AudioSystem._Instance.FadeMusic("Home00", 1.0f, 2.0f);
            AudioSystem._Instance.PlayMusic("Home01");
            AudioSystem._Instance.FadeMusic("Home01", 1.0f, 2.0f);
        }
    }
}
