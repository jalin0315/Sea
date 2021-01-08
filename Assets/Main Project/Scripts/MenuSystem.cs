using EasyMobile;
using GoogleMobileAdsMediationTestSuite.Api;
using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace CTJ
{
    public class MenuSystem : MonoBehaviour
    {
        public static MenuSystem _Instance;
        public enum Status
        {
            Animation,
            MainMenu,
            Index,
            Vehicle,
            Settings,
            Checkpoint,
            InGame,
            InGameMenu,
            DeathMenu
        }
        public Status _Status;
        [Space(20)]
        [SerializeField] private GameObject _Object_Play;
        [SerializeField] private GameObject _Object_Prompt;
        [SerializeField] private GameObject _Object_Checkpoint;
        [SerializeField] private GameObject _Object_Menu;
        [SerializeField] private GameObject _Object_MenuGroup;
        [SerializeField] private GameObject _Object_Return;
        [SerializeField] private GameObject _Object_Close;
        [SerializeField] private GameObject _Object_Index;
        [SerializeField] private GameObject _Object_Vehicle;
        [SerializeField] private GameObject _Object_Settings;
        [SerializeField] private GameObject _Object_InGameMenu;
        [SerializeField] private GameObject _Object_Fold;
        [SerializeField] private GameObject _Object_ReturnMainMenu;
        [SerializeField] private GameObject _Object_Depth;
        [SerializeField] private GameObject _Object_Health;
        [SerializeField] private GameObject _Object_ResurrectTotal;
        [SerializeField] private GameObject _Object_PropTime;
        [SerializeField] private GameObject _Object_Death;
        [Space(20)]
        [SerializeField] private Button _Button_Play;
        [SerializeField] private Button _Button_Checkpoint00;
        [SerializeField] private Button _Button_Checkpoint01;
        [SerializeField] private Button _Button_Checkpoint02;
        [SerializeField] private Button _Button_Index;
        [SerializeField] private Button _Button_Vehicle;
        [SerializeField] private Button[] _Array_Button_Vehicles;
        [SerializeField] private Button _Button_Settings;
        [SerializeField] private Button _Button_Settings_Music;
        [SerializeField] private Button _Button_Settings_SoundEffect;
        [SerializeField] private Button _Button_Settings_Vibration;
        [SerializeField] private Button _Button_Settings_Language;
        [SerializeField] private Button _Button_Return;
        [SerializeField] private Button _Button_Close;
        [SerializeField] private Button _Button_InGameMenu;
        [SerializeField] private Button _Button_Fold;
        [SerializeField] private Button _Button_ReturnMainMenu;
        [SerializeField] private Button _Button_Restart;
        [SerializeField] private Button _Button_Resurrect;
        [Space(20)]
        [SerializeField] private Image[] _Array_Image_Vehicles;
        [SerializeField] private Animator _Animator_Vehicles;
        [SerializeField] private int _Index_Vehicle = -1;
        [SerializeField] private bool[] _Array_Vehicles_Unlock;
        [SerializeField] private GameObject[] _Array_GameObject_Vehicles_Locked;
        [SerializeField] private GameObject[] _Array_GameObject_Vehicles_Unlock;
        [SerializeField] private SpriteRenderer _SpriteRenderer_Player;
        [SerializeField] private SpriteRenderer _SpriteRenderer_AnimationPlayer;
        [SerializeField] private SpriteRenderer _SpriteRenderer_AnimationPlayer_WaterReflect;
        [Space(20)]
        [SerializeField] private Image _Image_Music_Frame;
        [SerializeField] private Image _Image_SoundEffect_Frame;
        [SerializeField] private Image _Image_Vibration_Frame;
        [SerializeField] private Image _Image_Music;
        [SerializeField] private Image _Image_SoundEffect;
        [SerializeField] private Image _Image_Vibration;
        [SerializeField] private Image _Image_Language;
        [SerializeField] private Sprite _Sprite_UnMute;
        [SerializeField] private Sprite _Sprite_Mute;
        [SerializeField] private Sprite _Sprite_Vibration_Enable;
        [SerializeField] private Sprite _Sprite_Vibration_Disable;
        [SerializeField] private bool _Enable_Music;
        [SerializeField] private bool _Enable_SoundEffect;
        [SerializeField] private bool _Enable_Vibration;
        [SerializeField] private bool _IsEnglish;
        /*
        [SerializeField] private Image _Image_Music_Mute;
        [SerializeField] private Image _Image_Music_UnMute;
        [SerializeField] private Image _Image_SoundEffect_Mute;
        [SerializeField] private Image _Image_SoundEffect_UnMute;
        [SerializeField] private Image _Image_Vibration_Disable;
        [SerializeField] private Image _Image_Vibration_Enable;
        [SerializeField] private Image _Image_Handle_Music;
        [SerializeField] private Image _Image_Handle_SoundEffect;
        [SerializeField] private Image _Image_Handle_Vibration;
        */
        /*
        [SerializeField] private Sprite _Sprite_Handle_Golden;
        [SerializeField] private Sprite _Sprite_Handle_Silver;
        [SerializeField] private Slider _Slider_Music;
        [SerializeField] private Slider _Slider_SoundEffect;
        [SerializeField] private Slider _Slider_Vibration;
        [SerializeField] private EventTrigger _EventTrigger_Music;
        [SerializeField] private EventTrigger _EventTrigger_SoundEffect;
        [SerializeField] private EventTrigger _EventTrigger_Vibration;
        */
        /*
        private bool _IsMusic;
        private bool _IsSoundEffect;
        private bool _IsVibration;
        */
        [Space(20)]
        [SerializeField] private Text _Text_Prompt;
        [SerializeField] private Text _Text_TitleBar;
        [SerializeField] private LeanLocalizedText _LLT_TitleBar;
        public Text _Text_ResurrectTotal;
        // Variable
        private Vector2 _variable_vector2;

        private void Awake() => _Instance = this;

        private void Start()
        {
            StateChange(Status.Animation);
            _Button_Play.onClick.AddListener(OnButtonPlay);
            _Text_Prompt.text = "開始遊戲";
            _Button_Checkpoint00.onClick.AddListener(() => OnButtonCheckpoint(0.0f));
            _Button_Checkpoint01.onClick.AddListener(() => OnButtonCheckpoint(4000.0f));
            _Button_Checkpoint02.onClick.AddListener(() => OnButtonCheckpoint(8000.0f));
            _Button_Index.onClick.AddListener(OnButtonIndex);
            _Button_Vehicle.onClick.AddListener(OnButtonVehicle);
            for (int _i = 0; _i < _Array_Button_Vehicles.Length; _i++)
            {
                int _j = _i;
                _Array_Button_Vehicles[_i].onClick.AddListener(() => OnButtonVehicleSelected(_j));
            }
            _Button_Settings.onClick.AddListener(OnButtonSettings);
            _Button_Settings_Music.onClick.AddListener(OnButtonSettingsMusic);
            _Button_Settings_SoundEffect.onClick.AddListener(OnButtonSettingsSoundEffect);
            _Button_Settings_Vibration.onClick.AddListener(OnButtonSettingsVibration);
            _Button_Settings_Language.onClick.AddListener(OnButtonSettingsLanguage);
            _Button_Return.onClick.AddListener(OnButtonReturn);
            _Button_Close.onClick.AddListener(OnButtonClose);
            _Button_InGameMenu.onClick.AddListener(OnButtonInGameMenu);
            _Button_Fold.onClick.AddListener(OnButtonFold);
            _Button_ReturnMainMenu.onClick.AddListener(OnButtonReturnMainMenu);
            _Button_Restart.onClick.AddListener(OnButtonRestart);
            _Button_Resurrect.onClick.AddListener(OnButtonResurrect);

            /*
            _Slider_Music.onValueChanged.AddListener(delegate { OnMusicValueChange(); });
            {
                EventTrigger.Entry _entry = new EventTrigger.Entry();
                _entry.eventID = EventTriggerType.PointerDown;
                _entry.callback.AddListener((_data) => { OnMusicDown((PointerEventData)_data); });
                _EventTrigger_Music.triggers.Add(_entry);
            }
            {
                EventTrigger.Entry _entry = new EventTrigger.Entry();
                _entry.eventID = EventTriggerType.PointerUp;
                _entry.callback.AddListener((_data) => { OnMusicUp((PointerEventData)_data); });
                _EventTrigger_Music.triggers.Add(_entry);
            }
            _Slider_SoundEffect.onValueChanged.AddListener(delegate { OnSoundEffectValueChange(); });
            {
                EventTrigger.Entry _entry = new EventTrigger.Entry();
                _entry.eventID = EventTriggerType.PointerDown;
                _entry.callback.AddListener((_data) => { OnSoundEffectDown((PointerEventData)_data); });
                _EventTrigger_SoundEffect.triggers.Add(_entry);
            }
            {
                EventTrigger.Entry _entry = new EventTrigger.Entry();
                _entry.eventID = EventTriggerType.PointerUp;
                _entry.callback.AddListener((_data) => { OnSoundEffectUp((PointerEventData)_data); });
                _EventTrigger_SoundEffect.triggers.Add(_entry);
            }
            _Slider_Vibration.onValueChanged.AddListener(delegate { OnVibrationValueChange(); });
            {
                EventTrigger.Entry _entry = new EventTrigger.Entry();
                _entry.eventID = EventTriggerType.PointerDown;
                _entry.callback.AddListener((_data) => { OnVibrationDown((PointerEventData)_data); });
                _EventTrigger_Vibration.triggers.Add(_entry);
            }
            {
                EventTrigger.Entry _entry = new EventTrigger.Entry();
                _entry.eventID = EventTriggerType.PointerUp;
                _entry.callback.AddListener((_data) => { OnVibrationUp((PointerEventData)_data); });
                _EventTrigger_Vibration.triggers.Add(_entry);
            }
            */
            /*
            OnMusicValueChange();
            OnSoundEffectValueChange();
            OnVibrationValueChange();
            */
        }
        private void Update()
        {
            if (GameManager._InGame) return;
            //Settings();
        }
        /*
        private void Settings()
        {
            if (_Status != Status.Settings) return;
            if (!_IsMusic)
            {
                if (_Slider_Music.value > 0.5f) _Slider_Music.value += TimeSystem._DeltaTime() * 10.0f;
                else if (_Slider_Music.value < 0.5f) _Slider_Music.value -= TimeSystem._DeltaTime() * 10.0f;
            }
            if (!_IsSoundEffect)
            {
                if (_Slider_SoundEffect.value > 0.5f) _Slider_SoundEffect.value += TimeSystem._DeltaTime() * 10.0f;
                else if (_Slider_SoundEffect.value < 0.5f) _Slider_SoundEffect.value -= TimeSystem._DeltaTime() * 10.0f;
            }
            if (!_IsVibration)
            {
                if (_Slider_Vibration.value > 0.5f) _Slider_Vibration.value += TimeSystem._DeltaTime() * 10.0f;
                else if (_Slider_Vibration.value < 0.5f) _Slider_Vibration.value -= TimeSystem._DeltaTime() * 10.0f;
            }
        }
        */

        private void OnButtonPlay()
        {
            StateChange(Status.Checkpoint);
        }
        public void OnButtonCheckpoint(float _meter)
        {
            StateChange(Status.Animation);
            GameManager._Meter = _meter;
            GameManager._Instance.FixZoneTrigger();
            Timeline._Instance._Idle.Stop();
            Timeline._Instance._Opening.Play();
        }
        private void OnButtonIndex()
        {
            StateChange(Status.Index);
        }
        private void OnButtonVehicle()
        {
            StateChange(Status.Vehicle);
            Vehicle();
        }
        private void OnButtonVehicleSelected(int _index)
        {
            _Index_Vehicle = _index;
            VehicleChange();
        }
        private void OnButtonSettings()
        {
            StateChange(Status.Settings);
        }
        private void OnButtonSettingsMusic()
        {
            if (_Enable_Music)
            {
                AudioSystem._Instance.VolumeChangeMusic(0.0f);
                _Image_Music_Frame.color = Color.gray;
                _Image_Music.sprite = _Sprite_Mute;
                _Enable_Music = false;
                return;
            }
            else
            {
                AudioSystem._Instance.VolumeChangeMusic(1.0f);
                _Image_Music_Frame.color = Color.white;
                _Image_Music.sprite = _Sprite_UnMute;
                _Enable_Music = true;
            }
        }
        private void OnButtonSettingsSoundEffect()
        {
            if (_Enable_SoundEffect)
            {
                AudioSystem._Instance.VolumeChangeSoundEffect(0.0f);
                _Image_SoundEffect_Frame.color = Color.gray;
                _Image_SoundEffect.sprite = _Sprite_Mute;
                _Enable_SoundEffect = false;
                return;
            }
            else
            {
                AudioSystem._Instance.VolumeChangeSoundEffect(1.0f);
                _Image_SoundEffect_Frame.color = Color.white;
                _Image_SoundEffect.sprite = _Sprite_UnMute;
                _Enable_SoundEffect = true;
            }
        }
        private void OnButtonSettingsVibration()
        {
            if (_Enable_Vibration)
            {
                GameManager._Instance._Enable_Vibrate = false;
                _Image_Vibration_Frame.color = Color.gray;
                _Image_Vibration.sprite = _Sprite_Vibration_Disable;
                _Enable_Vibration = false;
                return;
            }
            else
            {
                GameManager._Instance._Enable_Vibrate = true;
                _Image_Vibration_Frame.color = Color.white;
                _Image_Vibration.sprite = _Sprite_Vibration_Enable;
                _Enable_Vibration = true;
            }
        }
        private void OnButtonSettingsLanguage()
        {
            if (_IsEnglish)
            {
                LeanLocalization.CurrentLanguage = "Traditional Chinese";
                _IsEnglish = false;
                return;
            }
            else
            {
                LeanLocalization.CurrentLanguage = "English";
                _IsEnglish = true;
            }
        }
        private void OnButtonReturn()
        {
            StateChange(Status.Index);
        }
        private void OnButtonClose()
        {
            StateChange(Status.MainMenu);
        }
        private void OnButtonInGameMenu()
        {
            StateChange(Status.InGameMenu);
        }
        private void OnButtonFold()
        {
            if (_Status == Status.InGameMenu) StateChange(Status.InGame);
            else StateChange(Status.MainMenu);
        }
        private void OnButtonReturnMainMenu()
        {
            StateChange(Status.Animation);
            TimeSystem.TimeScale(1.0f);
            GameManager._InGame = false;
            Timeline._Instance._ReturnMainMenu.Play();
        }
        private void OnButtonRestart()
        {
            OnButtonReturnMainMenu();
        }
        private void OnButtonResurrect()
        {
            if (Advertising.IsRewardedAdReady(RewardedAdNetwork.AdMob, AdPlacement.Default))
            {
                Advertising.ShowRewardedAd(RewardedAdNetwork.AdMob, AdPlacement.Default);
                AdvertisingEvent._Reward_Resurrect = true;
            }
            else Logger.LogWarning("Reward advertising not ready yet.");
        }

        private void Vehicle()
        {
            // 檢查所有載具狀態。
            for (int _i = 0; _i < _Array_Vehicles_Unlock.Length; _i++)
            {
                if (_Array_Vehicles_Unlock[_i])
                {
                    _Array_Button_Vehicles[_i].interactable = true;
                    _Array_GameObject_Vehicles_Locked[_i].SetActive(false);
                    _Array_GameObject_Vehicles_Unlock[_i].SetActive(true);
                }
                else
                {
                    _Array_Button_Vehicles[_i].interactable = false;
                    _Array_GameObject_Vehicles_Locked[_i].SetActive(true);
                    _Array_GameObject_Vehicles_Unlock[_i].SetActive(false);
                }
            }
            // 檢查如果當前的已選擇載具為空值，則自動順位選擇第一輛解鎖載具。
            for (int _i = 0; _i < _Array_Vehicles_Unlock.Length; _i++)
            {
                if (_Index_Vehicle == -1)
                {
                    if (_Array_Vehicles_Unlock[_i]) { _Index_Vehicle = _i; break; }
                    else continue;
                }
                else break;
            }
            VehicleChange();
        }
        private void VehicleChange()
        {
            _SpriteRenderer_Player.sprite = _Array_Image_Vehicles[_Index_Vehicle].sprite;
            Player._Instance._Sprite_Player = _Array_Image_Vehicles[_Index_Vehicle].sprite;
            _SpriteRenderer_AnimationPlayer.sprite = _Array_Image_Vehicles[_Index_Vehicle].sprite;
            _SpriteRenderer_AnimationPlayer_WaterReflect.sprite = _Array_Image_Vehicles[_Index_Vehicle].sprite;
            _Animator_Vehicles.SetBool("00", false);
            _Animator_Vehicles.SetBool("01", false);
            _Animator_Vehicles.SetBool("02", false);
            _Animator_Vehicles.SetBool("03", false);
            switch (_Index_Vehicle)
            {
                case 0:
                    _Animator_Vehicles.SetBool("00", true);
                    break;
                case 1:
                    _Animator_Vehicles.SetBool("01", true);
                    break;
                case 2:
                    _Animator_Vehicles.SetBool("02", true);
                    break;
                case 3:
                    _Animator_Vehicles.SetBool("03", true);
                    break;
                default:
                    Logger.LogWarningFormat("{0}: {1}.", nameof(_Index_Vehicle), _Index_Vehicle);
                    break;
            }
        }

        /*
        private void OnMusicValueChange()
        {
            AudioSystem._Instance.VolumeChangeMusic(_Slider_Music.value);
            if (_Slider_Music.value < 0.5f)
            {
                Color _color = Color.white;
                _color.a = 1.0f;
                _Image_Music_Mute.color = _color;
                _color.a = 0.6f;
                _Image_Music_UnMute.color = _color;
                _Image_Handle_Music.sprite = _Sprite_Handle_Silver;
                return;
            }
            if (_Slider_Music.value > 0.5f)
            {
                Color _color = Color.white;
                _color.a = 0.6f;
                _Image_Music_Mute.color = _color;
                _color.a = 1.0f;
                _Image_Music_UnMute.color = _color;
                _Image_Handle_Music.sprite = _Sprite_Handle_Golden;
                return;
            }
        }
        private void OnMusicDown(PointerEventData _data)
        {
            _IsMusic = true;
        }
        private void OnMusicUp(PointerEventData _data)
        {
            _IsMusic = false;
        }
        private void OnSoundEffectValueChange()
        {
            AudioSystem._Instance.VolumeChangeSoundEffect(_Slider_SoundEffect.value);
            if (_Slider_SoundEffect.value < 0.5f)
            {
                Color _color = Color.white;
                _color.a = 1.0f;
                _Image_SoundEffect_Mute.color = _color;
                _color.a = 0.6f;
                _Image_SoundEffect_UnMute.color = _color;
                _Image_Handle_SoundEffect.sprite = _Sprite_Handle_Silver;
                return;
            }
            if (_Slider_SoundEffect.value > 0.5f)
            {
                Color _color = Color.white;
                _color.a = 0.6f;
                _Image_SoundEffect_Mute.color = _color;
                _color.a = 1.0f;
                _Image_SoundEffect_UnMute.color = _color;
                _Image_Handle_SoundEffect.sprite = _Sprite_Handle_Golden;
                return;
            }
        }
        private void OnSoundEffectDown(PointerEventData _data)
        {
            _IsSoundEffect = true;
        }
        private void OnSoundEffectUp(PointerEventData _data)
        {
            _IsSoundEffect = false;
        }
        private void OnVibrationValueChange()
        {
            if (_Slider_Vibration.value < 0.5f)
            {
                Color _color = Color.white;
                _color.a = 1.0f;
                _Image_Vibration_Disable.color = _color;
                _color.a = 0.6f;
                _Image_Vibration_Enable.color = _color;
                _Image_Handle_Vibration.sprite = _Sprite_Handle_Silver;
                GameManager._Instance._Enable_Vibrate = false;
                return;
            }
            if (_Slider_Vibration.value > 0.5f)
            {
                Color _color = Color.white;
                _color.a = 0.6f;
                _Image_Vibration_Disable.color = _color;
                _color.a = 1.0f;
                _Image_Vibration_Enable.color = _color;
                _Image_Handle_Vibration.sprite = _Sprite_Handle_Golden;
                GameManager._Instance._Enable_Vibrate = true;
                return;
            }
        }
        private void OnVibrationDown(PointerEventData _data)
        {
            _IsVibration = true;
        }
        private void OnVibrationUp(PointerEventData _data)
        {
            _IsVibration = false;
        }
        */

        public void Test()
        {
            //MediationTestSuite.Show();
            Player._Instance._Slider_Health.maxValue = 9999;
            Player._Instance._Slider_Health.value = 9999;
        }

        public void StateChange(Status _status)
        {
            _Status = _status;
            switch (_Status)
            {
                case Status.Animation:
                    {
                        _Object_Play.SetActive(false);
                        _Object_Prompt.SetActive(false);
                        _Object_Checkpoint.SetActive(false);
                        _Object_Menu.SetActive(false);
                        _Object_MenuGroup.SetActive(false);
                        _Object_Return.SetActive(false);
                        _Object_Close.SetActive(false);
                        _Object_Index.SetActive(false);
                        _Object_Vehicle.SetActive(false);
                        _Object_Settings.SetActive(false);
                        _Object_InGameMenu.SetActive(false);
                        _Object_Fold.SetActive(false);
                        _Object_ReturnMainMenu.SetActive(false);
                        _Object_Depth.SetActive(false);
                        _Object_Health.SetActive(false);
                        _Object_ResurrectTotal.SetActive(false);
                        _Object_PropTime.SetActive(false);
                        _Object_Death.SetActive(false);
                        Advertising.HideBannerAd();
                    }
                    break;
                case Status.MainMenu:
                    {
                        _Object_Play.SetActive(true);
                        _Object_Prompt.SetActive(true);
                        _Object_Checkpoint.SetActive(false);
                        _Object_Menu.SetActive(true);
                        _Object_MenuGroup.SetActive(false);
                        _Object_Return.SetActive(false);
                        _Object_Close.SetActive(false);
                        _Object_Index.SetActive(false);
                        _Object_Vehicle.SetActive(false);
                        _Object_Settings.SetActive(false);
                        _Object_InGameMenu.SetActive(false);
                        _Object_Fold.SetActive(false);
                        _Object_ReturnMainMenu.SetActive(false);
                        _Object_Depth.SetActive(false);
                        _Object_Health.SetActive(false);
                        _Object_ResurrectTotal.SetActive(false);
                        _Object_PropTime.SetActive(false);
                        _Object_Death.SetActive(false);
                        Advertising.HideBannerAd();
                    }
                    break;
                case Status.Index:
                    {
                        _Object_Play.SetActive(false);
                        _Object_Prompt.SetActive(false);
                        _Object_Checkpoint.SetActive(false);
                        _Object_Menu.SetActive(false);
                        _Object_MenuGroup.SetActive(true);
                        _Object_Return.SetActive(false);
                        _Object_Close.SetActive(true);
                        _Object_Index.SetActive(true);
                        _Object_Vehicle.SetActive(false);
                        _Object_Settings.SetActive(false);
                        _Object_InGameMenu.SetActive(false);
                        _Object_Fold.SetActive(false);
                        _Object_ReturnMainMenu.SetActive(false);
                        _Object_Depth.SetActive(false);
                        _Object_Health.SetActive(false);
                        _Object_ResurrectTotal.SetActive(false);
                        _Object_PropTime.SetActive(false);
                        _Object_Death.SetActive(false);
                        _LLT_TitleBar.TranslationName = "_Index/Title/Index";
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.Vehicle:
                    {
                        _Object_Play.SetActive(false);
                        _Object_Prompt.SetActive(false);
                        _Object_Checkpoint.SetActive(false);
                        _Object_Menu.SetActive(true);
                        _Object_MenuGroup.SetActive(true);
                        _Object_Return.SetActive(true);
                        _Object_Close.SetActive(false);
                        _Object_Index.SetActive(false);
                        _Object_Vehicle.SetActive(true);
                        _Object_Settings.SetActive(false);
                        _Object_InGameMenu.SetActive(false);
                        _Object_Fold.SetActive(false);
                        _Object_ReturnMainMenu.SetActive(false);
                        _Object_Depth.SetActive(false);
                        _Object_Health.SetActive(false);
                        _Object_ResurrectTotal.SetActive(false);
                        _Object_PropTime.SetActive(false);
                        _Object_Death.SetActive(false);
                        _LLT_TitleBar.TranslationName = "_Index/Title/_Vehicle";
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.Settings:
                    {
                        _Object_Play.SetActive(false);
                        _Object_Prompt.SetActive(false);
                        _Object_Checkpoint.SetActive(false);
                        _Object_Menu.SetActive(false);
                        _Object_MenuGroup.SetActive(true);
                        _Object_Return.SetActive(true);
                        _Object_Close.SetActive(false);
                        _Object_Index.SetActive(false);
                        _Object_Vehicle.SetActive(false);
                        _Object_Settings.SetActive(true);
                        _Object_InGameMenu.SetActive(false);
                        _Object_Fold.SetActive(false);
                        _Object_ReturnMainMenu.SetActive(false);
                        _Object_Depth.SetActive(false);
                        _Object_Health.SetActive(false);
                        _Object_ResurrectTotal.SetActive(false);
                        _Object_PropTime.SetActive(false);
                        _Object_Death.SetActive(false);
                        _LLT_TitleBar.TranslationName = "_Index/Title/Settings";
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.Checkpoint:
                    {
                        _Object_Play.SetActive(false);
                        _Object_Prompt.SetActive(false);
                        _Object_Checkpoint.SetActive(true);
                        _Object_Menu.SetActive(true);
                        _Object_MenuGroup.SetActive(false);
                        _Object_Return.SetActive(false);
                        _Object_Close.SetActive(false);
                        _Object_Index.SetActive(false);
                        _Object_Vehicle.SetActive(false);
                        _Object_Settings.SetActive(false);
                        _Object_InGameMenu.SetActive(false);
                        _Object_Fold.SetActive(false);
                        _Object_ReturnMainMenu.SetActive(false);
                        _Object_Depth.SetActive(false);
                        _Object_Health.SetActive(false);
                        _Object_ResurrectTotal.SetActive(false);
                        _Object_PropTime.SetActive(false);
                        _Object_Death.SetActive(false);
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.InGame:
                    {
                        _Object_Play.SetActive(false);
                        _Object_Prompt.SetActive(false);
                        _Object_Checkpoint.SetActive(false);
                        _Object_Menu.SetActive(false);
                        _Object_MenuGroup.SetActive(false);
                        _Object_Return.SetActive(false);
                        _Object_Close.SetActive(false);
                        _Object_Index.SetActive(false);
                        _Object_Vehicle.SetActive(false);
                        _Object_Settings.SetActive(false);
                        _Object_InGameMenu.SetActive(true);
                        _Object_Fold.SetActive(false);
                        _Object_ReturnMainMenu.SetActive(false);
                        _Object_Depth.SetActive(true);
                        _Object_Health.SetActive(true);
                        _Object_ResurrectTotal.SetActive(true);
                        //_Object_PropTime.SetActive(null);
                        _Object_Death.SetActive(false);
                        GameManager._Instance.GameState(true);
                        Advertising.HideBannerAd();
                    }
                    break;
                case Status.InGameMenu:
                    {
                        _Object_Play.SetActive(false);
                        _Object_Prompt.SetActive(false);
                        _Object_Checkpoint.SetActive(false);
                        _Object_Menu.SetActive(false);
                        _Object_MenuGroup.SetActive(false);
                        _Object_Return.SetActive(false);
                        _Object_Close.SetActive(false);
                        _Object_Index.SetActive(false);
                        _Object_Vehicle.SetActive(false);
                        _Object_Settings.SetActive(false);
                        _Object_InGameMenu.SetActive(false);
                        _Object_Fold.SetActive(true);
                        _Object_ReturnMainMenu.SetActive(true);
                        _Object_Depth.SetActive(true);
                        _Object_Health.SetActive(true);
                        _Object_ResurrectTotal.SetActive(true);
                        //_Object_PropTime.SetActive(null);
                        _Object_Death.SetActive(false);
                        GameManager._Instance.GameState(false);
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.DeathMenu:
                    {
                        _Object_Play.SetActive(false);
                        _Object_Prompt.SetActive(false);
                        _Object_Checkpoint.SetActive(false);
                        _Object_Menu.SetActive(false);
                        _Object_MenuGroup.SetActive(false);
                        _Object_Return.SetActive(false);
                        _Object_Close.SetActive(false);
                        _Object_Index.SetActive(false);
                        _Object_Vehicle.SetActive(false);
                        _Object_Settings.SetActive(false);
                        _Object_InGameMenu.SetActive(false);
                        _Object_Fold.SetActive(false);
                        _Object_ReturnMainMenu.SetActive(false);
                        _Object_Depth.SetActive(true);
                        _Object_Health.SetActive(true);
                        _Object_ResurrectTotal.SetActive(true);
                        //_Object_PropTime.SetActive(null);
                        _Object_Death.SetActive(true);
                        if (GameManager._Instance._ResurrectTotal <= 0) _Button_Resurrect.interactable = false;
                        else _Button_Resurrect.interactable = true;
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                default:
                    Logger.LogWarningFormat("StateChange({0}) is Error!", _status);
                    break;
            }
        }
    }
}
