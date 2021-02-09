using EasyMobile;
using GoogleMobileAdsMediationTestSuite.Api;
using Lean.Localization;
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
            PlayMenu,
            IndexMenu,
            VehicleMenu,
            HowToPlayMenu,
            SettingMenu,
            InGameMenu,
            PauseMenu,
            InGameSettingMenu,
            DeathMenu
        }
        public Status _Status;
        public Animator _Animator;
        [Space(20)]
        [SerializeField] private GameObject _Object_PlayMenu_01;
        [SerializeField] private GameObject _Object_PlayMenu_02;
        [SerializeField] private GameObject _Object_PlayMenu_Lock_01;
        [SerializeField] private GameObject _Object_PlayMenu_Lock_02;
        [Space(20)]
        [SerializeField] private Button _Button_Play;
        [SerializeField] private Button _Button_Play_00;
        [SerializeField] private Button _Button_Play_01;
        [SerializeField] private Button _Button_Play_01_Ad_00;
        [SerializeField] private Button _Button_Play_02;
        [SerializeField] private Button _Button_Play_02_Ad_00;
        [SerializeField] private Button _Button_Play_02_Ad_01;
        [SerializeField] private Button _Button_Cancel;
        [SerializeField] private Button _Button_Index;
        [SerializeField] private Button _Button_Achievement;
        [SerializeField] private Button _Button_Vehicle;
        [SerializeField] private Button[] _Array_Button_Vehicles;
        [SerializeField] private Button _Button_HowToPlayMenu;
        [SerializeField] private Button _Button_Settings;
        [SerializeField] private Button _Button_Settings_Music;
        [SerializeField] private Button _Button_Settings_SoundEffect;
        [SerializeField] private Button _Button_Settings_Vibration;
        [SerializeField] private Button _Button_Settings_Language;
        [SerializeField] private Button _Button_Return;
        [SerializeField] private Button _Button_Close;
        [SerializeField] private Button _Button_PauseMenu;
        [SerializeField] private Button _Button_ReturnMainMenu;
        [SerializeField] private Button _Button_InGameSettingMenu;
        [SerializeField] private Button _Button_Home;
        [SerializeField] private Button _Button_Resurrect;
        [Space(20)]
        [SerializeField] private Image[] _Array_Image_Vehicles;
        [SerializeField] private Animator _Animator_Vehicles;
        public int _Index_Vehicle = -1;
        [SerializeField] private bool[] _Array_Vehicles_Unlock;
        [SerializeField] private GameObject[] _Array_GameObject_Vehicles_Locked;
        [SerializeField] private GameObject[] _Array_GameObject_Vehicles_Unlock;
        [SerializeField] private GameObject[] _Array_GameObject_PromptRaycastTarget;
        [SerializeField] private GameObject[] _Array_GameObject_UnlockPrompt;
        [SerializeField] private SpriteRenderer _SpriteRenderer_Player;
        [SerializeField] private SpriteRenderer _SpriteRenderer_AnimationPlayer;
        [SerializeField] private SpriteRenderer _SpriteRenderer_AnimationPlayer_WaterReflect;
        [SerializeField] private SpriteRenderer _SpriteRenderer_EndPlayer;
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
        [SerializeField] private LeanLocalizedImage _LLI_Title;
        [SerializeField] private LeanLocalizedImage[] _Array_LLI_Vehicle;
        public Text _Text_ResurrectTotal;

        private void Awake()
        {
            _Instance = this;
        }

        private void Start()
        {
            _Button_Play.onClick.AddListener(OnButtonPlay);
            _Button_Play_00.onClick.AddListener(OnButtonPlayIndex00);
            _Button_Play_01.onClick.AddListener(OnButtonPlayIndex01);
            _Button_Play_01_Ad_00.onClick.AddListener(OnButtonPlay01Ad00);
            _Button_Play_02_Ad_00.onClick.AddListener(OnButtonPlay02Ad00);
            _Button_Play_02_Ad_01.onClick.AddListener(OnButtonPlay02Ad01);
            _Button_Play_02.onClick.AddListener(OnButtonPlayIndex02);
            _Button_Index.onClick.AddListener(OnButtonIndexMenu);
            _Button_Achievement.onClick.AddListener(OnButtonAchievement);
            _Button_Vehicle.onClick.AddListener(OnButtonVehicleMenu);
            for (int _i = 0; _i < _Array_Button_Vehicles.Length; _i++)
            {
                int _j = _i;
                _Array_Button_Vehicles[_i].onClick.AddListener(() => OnButtonVehicleSelected(_j));
            }
            _Button_HowToPlayMenu.onClick.AddListener(OnButtonHowToPlayMenu);
            _Button_Settings.onClick.AddListener(OnButtonSettingMenu);
            _Button_Settings_Music.onClick.AddListener(OnButtonSettingsMusic);
            _Button_Settings_SoundEffect.onClick.AddListener(OnButtonSettingsSoundEffect);
            _Button_Settings_Vibration.onClick.AddListener(OnButtonSettingsVibration);
            _Button_Settings_Language.onClick.AddListener(OnButtonSettingsLanguage);
            _Button_Return.onClick.AddListener(OnButtonReturn);
            _Button_Close.onClick.AddListener(OnButtonClose);
            _Button_PauseMenu.onClick.AddListener(OnButtonPauseMenu);
            _Button_Cancel.onClick.AddListener(OnButtonCancel);
            _Button_ReturnMainMenu.onClick.AddListener(OnButtonReturnMainMenu);
            _Button_InGameSettingMenu.onClick.AddListener(OnButtonInGameSettingMenu);
            _Button_Home.onClick.AddListener(OnButtonHome);
            _Button_Resurrect.onClick.AddListener(OnButtonResurrect);
            Vehicle();
            Settings();
            #region Old
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
            #endregion
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

        public void OnButtonPlay()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            _Object_PlayMenu_01.SetActive(Database._Play_01 == 1 ? true : false);
            _Object_PlayMenu_02.SetActive(Database._Play_02 == 1 ? true : false);
            _Object_PlayMenu_Lock_01.SetActive(Database._Play_01 == 0 ? true : false);
            _Object_PlayMenu_Lock_02.SetActive(Database._Play_02 == 0 ? true : false);
            _Button_Play_01.interactable = Database._Play_01_Unlock_00 == 1 ? true : false;
            _Button_Play_01_Ad_00.interactable = Database._Play_01_Unlock_00 == 0 ? true : false;
            if (Database._Play_02_Unlock_00 == 1 ? true : false)
            {
                if (Database._Play_02_Unlock_01 == 1 ? true : false) _Button_Play_02.interactable = true;
                else _Button_Play_02.interactable = false;
            }
            else _Button_Play_02.interactable = false;
            _Button_Play_02_Ad_00.interactable = Database._Play_02_Unlock_00 == 0 ? true : false;
            _Button_Play_02_Ad_01.interactable = Database._Play_02_Unlock_01 == 0 ? true : false;
            StateChange(Status.PlayMenu);
        }
        private void OnButtonPlay01Ad00()
        {
            if (Advertising.IsRewardedAdReady())
            {
                AdvertisingEvent._Reward_Play_01_Ad_00 = true;
                Advertising.ShowRewardedAd();
            }
            else Logger.LogWarning("Reward advertising not ready yet.");
        }
        private void OnButtonPlay02Ad00()
        {
            if (Advertising.IsRewardedAdReady())
            {
                AdvertisingEvent._Reward_Play_02_Ad_00 = true;
                Advertising.ShowRewardedAd();
            }
            else Logger.LogWarning("Reward advertising not ready yet.");
        }
        private void OnButtonPlay02Ad01()
        {
            if (Advertising.IsRewardedAdReady())
            {
                AdvertisingEvent._Reward_Play_02_Ad_01 = true;
                Advertising.ShowRewardedAd();
            }
            else Logger.LogWarning("Reward advertising not ready yet.");
        }
        private void OnButtonPlayIndex00()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            if (GameManager._Instance._End)
            {
                switch (GameManager._Instance._MusicIndex)
                {
                    case 0:
                        AudioSystem._Instance.FadeMusic("End00", 0.0f, 3.0f);
                        break;
                    case 1:
                        AudioSystem._Instance.FadeMusic("End01", 0.0f, 3.0f);
                        break;
                }
            }
            else if (!GameManager._Instance._End)
            {
                AudioSystem._Instance.FadeMusic("Home00", 0.0f, 3.0f);
                AudioSystem._Instance.FadeMusic("Home01", 0.0f, 3.0f);
            }
            StateChange(Status.Animation);
            GameManager._Meter = 0.0f;
            GameManager._Instance.FixZoneTrigger();
            Player._Instance.InitHealth();
            Timeline._Instance._Idle.Stop();
            Timeline._Instance._Opening.Play();
            GameManager._Instance._End = false;
            switch (_Index_Vehicle)
            {
                case 0:
                    GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_burger_start_off);
                    break;
                case 1:
                    GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_torpedo_start_off);
                    break;
                case 2:
                    GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_tidal_start_off);
                    break;
                case 3:
                    GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_fishkenstein_start_off);
                    break;
            }
        }
        private void OnButtonPlayIndex01()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            if (GameManager._Instance._End)
            {
                switch (GameManager._Instance._MusicIndex)
                {
                    case 0:
                        AudioSystem._Instance.FadeMusic("End00", 0.0f, 3.0f);
                        break;
                    case 1:
                        AudioSystem._Instance.FadeMusic("End01", 0.0f, 3.0f);
                        break;
                }
            }
            else if (!GameManager._Instance._End)
            {
                AudioSystem._Instance.FadeMusic("Home00", 0.0f, 3.0f);
                AudioSystem._Instance.FadeMusic("Home01", 0.0f, 3.0f);
            }
            StateChange(Status.Animation);
            GameManager._Meter = 4000.0f;
            GameManager._Instance.FixZoneTrigger();
            Player._Instance.InitHealth();
            Timeline._Instance._Idle.Stop();
            Timeline._Instance._Opening.Play();
            Database._Play_01_Unlock_00 = 0;
            GameManager._Instance._End = false;
        }
        private void OnButtonPlayIndex02()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            if (GameManager._Instance._End)
            {
                switch (GameManager._Instance._MusicIndex)
                {
                    case 0:
                        AudioSystem._Instance.FadeMusic("End00", 0.0f, 3.0f);
                        break;
                    case 1:
                        AudioSystem._Instance.FadeMusic("End01", 0.0f, 3.0f);
                        break;
                }
            }
            else if (!GameManager._Instance._End)
            {
                AudioSystem._Instance.FadeMusic("Home00", 0.0f, 3.0f);
                AudioSystem._Instance.FadeMusic("Home01", 0.0f, 3.0f);
            }
            StateChange(Status.Animation);
            GameManager._Meter = 8000.0f;
            GameManager._Instance.FixZoneTrigger();
            Player._Instance.InitHealth();
            Timeline._Instance._Idle.Stop();
            Timeline._Instance._Opening.Play();
            Database._Play_02_Unlock_00 = 0;
            Database._Play_02_Unlock_01 = 0;
            GameManager._Instance._End = false;
        }
        private void OnButtonIndexMenu()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            StateChange(Status.IndexMenu);
        }
        private void OnButtonAchievement()
        {
            if (GameServices.IsInitialized()) GameServices.ShowAchievementsUI();
            else Logger.LogWarning("Game Services Error!");
        }
        private void OnButtonVehicleMenu()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            StateChange(Status.VehicleMenu);
            Vehicle();
        }
        private void OnButtonVehicleSelected(int _index)
        {
            if (_Index_Vehicle == _index) return;
            int _r = Random.Range(0, 2);
            switch (_r)
            {
                case 0:
                    AudioSystem._Instance.PlaySoundEffect("ChangeSubmarine00");
                    AudioSystem._Instance.FadeSoundEffect("ChangeSubmarine00", 0.0f, 1.5f);
                    break;
                case 1:
                    AudioSystem._Instance.PlaySoundEffect("ChangeSubmarine01");
                    AudioSystem._Instance.FadeSoundEffect("ChangeSubmarine01", 0.0f, 1.5f);
                    break;
            }
            _Index_Vehicle = _index;
            Database._Vehicle_Index = _Index_Vehicle;
            VehicleChange();
        }
        private void OnButtonHowToPlayMenu()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            StateChange(Status.HowToPlayMenu);
        }
        public void OnButtonPromptRaycastTargetDown(int _index)
        {
            _Array_GameObject_UnlockPrompt[_index].SetActive(true);
        }
        public void OnButtonPromptRaycastTargetUp(int _index)
        {
            _Array_GameObject_UnlockPrompt[_index].SetActive(false);
        }
        private void OnButtonSettingMenu()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            StateChange(Status.SettingMenu);
        }
        private void OnButtonSettingsMusic()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            _Enable_Music = Database._Settings_Music == 1 ? true : false;
            if (_Enable_Music)
            {
                AudioSystem._Instance.VolumeChangeMusic(0.0f);
                _Image_Music_Frame.color = Color.gray;
                _Image_Music.sprite = _Sprite_Mute;
                _Enable_Music = false;
            }
            else
            {
                AudioSystem._Instance.VolumeChangeMusic(1.0f);
                _Image_Music_Frame.color = Color.white;
                _Image_Music.sprite = _Sprite_UnMute;
                _Enable_Music = true;
            }
            Database._Settings_Music = _Enable_Music ? 1 : 0;
        }
        private void OnButtonSettingsSoundEffect()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            _Enable_SoundEffect = Database._Settings_SoundEffect == 1 ? true : false;
            if (_Enable_SoundEffect)
            {
                AudioSystem._Instance.VolumeChangeSoundEffect(0.0f);
                Timeline._Instance.AudioEnable(false);
                _Image_SoundEffect_Frame.color = Color.gray;
                _Image_SoundEffect.sprite = _Sprite_Mute;
                _Enable_SoundEffect = false;
            }
            else
            {
                AudioSystem._Instance.VolumeChangeSoundEffect(1.0f);
                Timeline._Instance.AudioEnable(true);
                _Image_SoundEffect_Frame.color = Color.white;
                _Image_SoundEffect.sprite = _Sprite_UnMute;
                _Enable_SoundEffect = true;
            }
            Database._Settings_SoundEffect = _Enable_SoundEffect ? 1 : 0;
        }
        private void OnButtonSettingsVibration()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            _Enable_Vibration = Database._Settings_Vibration == 1 ? true : false;
            if (_Enable_Vibration)
            {
                GameManager._Instance._Enable_Vibrate = false;
                _Image_Vibration_Frame.color = Color.gray;
                _Image_Vibration.sprite = _Sprite_Vibration_Disable;
                _Enable_Vibration = false;
            }
            else
            {
                GameManager._Instance._Enable_Vibrate = true;
                _Image_Vibration_Frame.color = Color.white;
                _Image_Vibration.sprite = _Sprite_Vibration_Enable;
                _Enable_Vibration = true;
            }
            Database._Settings_Vibration = _Enable_Vibration ? 1 : 0;
        }
        private void OnButtonSettingsLanguage()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            if (LeanLocalization.CurrentLanguage == "English") LeanLocalization.CurrentLanguage = "Traditional Chinese";
            else LeanLocalization.CurrentLanguage = "English";
        }
        private void OnButtonReturn()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            switch (_Status)
            {
                case Status.VehicleMenu:
                    StateChange(Status.IndexMenu);
                    break;
                case Status.HowToPlayMenu:
                    StateChange(Status.IndexMenu);
                    break;
                case Status.SettingMenu:
                    StateChange(Status.IndexMenu);
                    break;
                case Status.InGameSettingMenu:
                    StateChange(Status.PauseMenu);
                    break;
            }
        }
        private void OnButtonClose()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            StateChange(Status.MainMenu);
        }
        private void OnButtonPauseMenu()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            StateChange(Status.PauseMenu);
        }
        private void OnButtonCancel()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            switch (_Status)
            {
                case Status.PlayMenu:
                    StateChange(Status.MainMenu);
                    break;
                case Status.PauseMenu:
                    StateChange(Status.InGameMenu);
                    break;
            }
        }
        private void OnButtonReturnMainMenu()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            StateChange(Status.Animation);
            TimeSystem.TimeScale(1.0f);
            GameManager._InGame = false;
            Timeline._Instance._ReturnMainMenu.Play();
        }
        private void OnButtonInGameSettingMenu()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            StateChange(Status.InGameSettingMenu);
        }
        private void OnButtonHome()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            OnButtonReturnMainMenu();
        }
        private void OnButtonResurrect()
        {
            AudioSystem._Instance.PlaySoundEffect("Click");
            if (Advertising.IsRewardedAdReady())
            {
                AdvertisingEvent._Reward_Resurrect = true;
                Advertising.ShowRewardedAd();
            }
            else Logger.LogWarning("Reward advertising not ready yet.");
        }

        private void Vehicle()
        {
            // 匯入資料
            _Index_Vehicle = Database._Vehicle_Index;
            _Array_Vehicles_Unlock[0] = true;
            _Array_Vehicles_Unlock[1] = Database._Vehicle_01 == 1 ? true : false;
            _Array_Vehicles_Unlock[2] = Database._Vehicle_02 == 1 ? true : false;
            _Array_Vehicles_Unlock[3] = Database._Vehicle_03 == 1 ? true : false;
            // 檢查所有載具狀態。
            for (int _i = 0; _i < _Array_Vehicles_Unlock.Length; _i++)
            {
                if (_Array_Vehicles_Unlock[_i])
                {
                    _Array_Button_Vehicles[_i].interactable = true;
                    _Array_GameObject_Vehicles_Locked[_i].SetActive(false);
                    _Array_GameObject_Vehicles_Unlock[_i].SetActive(true);
                    _Array_GameObject_PromptRaycastTarget[_i].SetActive(false);
                    _Array_GameObject_UnlockPrompt[_i].SetActive(false);
                    _Array_LLI_Vehicle[_i].TranslationName = "_VehicleMenu/0" + _i.ToString();
                }
                else
                {
                    _Array_Button_Vehicles[_i].interactable = false;
                    _Array_GameObject_Vehicles_Locked[_i].SetActive(true);
                    _Array_GameObject_Vehicles_Unlock[_i].SetActive(false);
                    _Array_GameObject_PromptRaycastTarget[_i].SetActive(true);
                    _Array_GameObject_UnlockPrompt[_i].SetActive(false);
                    _Array_LLI_Vehicle[_i].TranslationName = "_VehicleMenu/Lock";
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
            _SpriteRenderer_EndPlayer.sprite = _Array_Image_Vehicles[_Index_Vehicle].sprite;
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
        private void Settings()
        {
            _Enable_Music = Database._Settings_Music == 1 ? true : false;
            if (_Enable_Music)
            {
                AudioSystem._Instance.VolumeChangeMusic(1.0f);
                _Image_Music_Frame.color = Color.white;
                _Image_Music.sprite = _Sprite_UnMute;
            }
            else
            {
                AudioSystem._Instance.VolumeChangeMusic(0.0f);
                _Image_Music_Frame.color = Color.gray;
                _Image_Music.sprite = _Sprite_Mute;
            }
            _Enable_SoundEffect = Database._Settings_SoundEffect == 1 ? true : false;
            if (_Enable_SoundEffect)
            {
                AudioSystem._Instance.VolumeChangeSoundEffect(1.0f);
                Timeline._Instance.AudioEnable(true);
                _Image_SoundEffect_Frame.color = Color.white;
                _Image_SoundEffect.sprite = _Sprite_UnMute;
            }
            else
            {
                AudioSystem._Instance.VolumeChangeSoundEffect(0.0f);
                Timeline._Instance.AudioEnable(false);
                _Image_SoundEffect_Frame.color = Color.gray;
                _Image_SoundEffect.sprite = _Sprite_Mute;
            }
            _Enable_Vibration = Database._Settings_Vibration == 1 ? true : false;
            if (_Enable_Vibration)
            {
                GameManager._Instance._Enable_Vibrate = true;
                _Image_Vibration_Frame.color = Color.white;
                _Image_Vibration.sprite = _Sprite_Vibration_Enable;
            }
            else
            {
                GameManager._Instance._Enable_Vibrate = false;
                _Image_Vibration_Frame.color = Color.gray;
                _Image_Vibration.sprite = _Sprite_Vibration_Disable;
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

        public void DebugInvincible()
        {
            Player._Instance._Invincible = true;
        }

        public void StateChange(Status _status)
        {
            _Status = _status;
            switch (_Status)
            {
                case Status.Animation:
                    {
                        _Animator.SetBool("PropTime", false);
                        _Animator.SetTrigger("Animation");
                        Advertising.HideBannerAd();
                    }
                    break;
                case Status.MainMenu:
                    {
                        _Animator.SetTrigger("MainMenu");
                        Advertising.HideBannerAd();
                    }
                    break;
                case Status.PlayMenu:
                    {
                        _Animator.SetTrigger("PlayMenu");
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.IndexMenu:
                    {
                        _Animator.SetTrigger("IndexMenu");
                        _LLI_Title.TranslationName = "_IndexMenu/Title";
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.VehicleMenu:
                    {
                        _Animator.SetTrigger("VehicleMenu");
                        _LLI_Title.TranslationName = "_VehicleMenu/Title";
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.HowToPlayMenu:
                    {
                        _Animator.SetTrigger("HowToPlayMenu");
                        _LLI_Title.TranslationName = "_HowToPlayMenu/Title";
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.SettingMenu:
                    {
                        _Animator.SetTrigger("SettingMenu");
                        _LLI_Title.TranslationName = "_SettingMenu/Title";
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.InGameMenu:
                    {
                        _Animator.SetTrigger("InGameMenu");
                        GameManager._Instance.GameState(true);
                        Advertising.HideBannerAd();
                    }
                    break;
                case Status.PauseMenu:
                    {
                        _Animator.SetTrigger("PauseMenu");
                        GameManager._Instance.GameState(false);
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.InGameSettingMenu:
                    {
                        _Animator.SetTrigger("SettingMenu");
                        _LLI_Title.TranslationName = "_SettingMenu/Title";
                        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                    }
                    break;
                case Status.DeathMenu:
                    {
                        _Animator.SetTrigger("DeathMenu");
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
