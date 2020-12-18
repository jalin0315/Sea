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

public class MenuSystem : MonoBehaviour
{
    public static MenuSystem _Instance;
    public enum Status
    {
        Animation,
        MainMenu,
        Index,
        Submarine,
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
    [SerializeField] private GameObject _Object_Submarine;
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
    [SerializeField] private Button _Button_Submarine;
    [SerializeField] private Button _Button_Settings;
    [SerializeField] private Button _Button_Return;
    [SerializeField] private Button _Button_Close;
    [SerializeField] private Button _Button_InGameMenu;
    [SerializeField] private Button _Button_Fold;
    [SerializeField] private Button _Button_ReturnMainMenu;
    [SerializeField] private Button _Button_Restart;
    [SerializeField] private Button _Button_Resurrect;
    [Space(20)]
    [SerializeField] private Image _Image_Music_Mute;
    [SerializeField] private Image _Image_Music_UnMute;
    [SerializeField] private Image _Image_SoundEffect_Mute;
    [SerializeField] private Image _Image_SoundEffect_UnMute;
    [SerializeField] private Image _Image_Vibration_Disable;
    [SerializeField] private Image _Image_Vibration_Enable;
    [SerializeField] private Image _Image_Handle_Music;
    [SerializeField] private Image _Image_Handle_SoundEffect;
    [SerializeField] private Image _Image_Handle_Vibration;
    [SerializeField] private Sprite _Sprite_Handle_Golden;
    [SerializeField] private Sprite _Sprite_Handle_Silver;
    [SerializeField] private Slider _Slider_Music;
    [SerializeField] private Slider _Slider_SoundEffect;
    [SerializeField] private Slider _Slider_Vibration;
    [SerializeField] private EventTrigger _EventTrigger_Music;
    [SerializeField] private EventTrigger _EventTrigger_SoundEffect;
    [SerializeField] private EventTrigger _EventTrigger_Vibration;
    private bool _IsMusic;
    private bool _IsSoundEffect;
    private bool _IsVibration;
    [Space(20)]
    [SerializeField] private Text _Text_Prompt;
    [SerializeField] private Text _Text_TitleBar;
    [SerializeField] private LeanLocalizedText _LLT_TitleBar;
    public Text _Text_ResurrectTotal;

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
        _Button_Submarine.onClick.AddListener(OnButtonSubmarine);
        _Button_Settings.onClick.AddListener(OnButtonSettings);
        _Button_Return.onClick.AddListener(OnButtonReturn);
        _Button_Close.onClick.AddListener(OnButtonClose);
        _Button_InGameMenu.onClick.AddListener(OnButtonInGameMenu);
        _Button_Fold.onClick.AddListener(OnButtonFold);
        _Button_ReturnMainMenu.onClick.AddListener(OnButtonReturnMainMenu);
        _Button_Restart.onClick.AddListener(OnButtonRestart);
        _Button_Resurrect.onClick.AddListener(OnButtonResurrect);

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
        OnMusicValueChange();
        OnSoundEffectValueChange();
        OnVibrationValueChange();
    }

    private void Update()
    {
        if (GameManager._Instance._InGame) return;
        Settings();
    }
    private void Settings()
    {
        if (_Status != Status.Settings) return;
        if (!_IsMusic)
        {
            if (_Slider_Music.value > 0.5f) _Slider_Music.value += CTJ.TimeSystem._DeltaTime() * 10.0f;
            else if (_Slider_Music.value < 0.5f) _Slider_Music.value -= CTJ.TimeSystem._DeltaTime() * 10.0f;
        }
        if (!_IsSoundEffect)
        {
            if (_Slider_SoundEffect.value > 0.5f) _Slider_SoundEffect.value += CTJ.TimeSystem._DeltaTime() * 10.0f;
            else if (_Slider_SoundEffect.value < 0.5f) _Slider_SoundEffect.value -= CTJ.TimeSystem._DeltaTime() * 10.0f;
        }
        if (!_IsVibration)
        {
            if (_Slider_Vibration.value > 0.5f) _Slider_Vibration.value += CTJ.TimeSystem._DeltaTime() * 10.0f;
            else if (_Slider_Vibration.value < 0.5f) _Slider_Vibration.value -= CTJ.TimeSystem._DeltaTime() * 10.0f;
        }
    }

    private void OnButtonPlay()
    {
        StateChange(Status.Checkpoint);
    }
    public void OnButtonCheckpoint(float _meter)
    {
        StateChange(Status.Animation);
        GameManager._Instance._Meter = _meter;
        GameManager._Instance._Result = Convert.ToInt32(GameManager._Instance._Meter);
        GameManager._Instance.FixZoneTrigger();
        Timeline._Instance._Idle.Stop();
        Timeline._Instance._Opening.Play();
    }
    private void OnButtonIndex()
    {
        StateChange(Status.Index);
    }
    private void OnButtonSubmarine()
    {
        StateChange(Status.Submarine);
    }
    private void OnButtonSettings()
    {
        StateChange(Status.Settings);
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
        CTJ.TimeSystem.TimeScale(1.0f);
        GameManager._Instance._InGame = false;
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
    }

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

    public void Test()
    {
        MediationTestSuite.Show();
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
                    _Object_Submarine.SetActive(false);
                    _Object_Settings.SetActive(false);
                    _Object_InGameMenu.SetActive(false);
                    _Object_Fold.SetActive(false);
                    _Object_ReturnMainMenu.SetActive(false);
                    _Object_Depth.SetActive(false);
                    _Object_Health.SetActive(false);
                    _Object_ResurrectTotal.SetActive(false);
                    _Object_PropTime.SetActive(false);
                    _Object_Death.SetActive(false);
                    Advertising.DestroyBannerAd();
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
                    _Object_Submarine.SetActive(false);
                    _Object_Settings.SetActive(false);
                    _Object_InGameMenu.SetActive(false);
                    _Object_Fold.SetActive(false);
                    _Object_ReturnMainMenu.SetActive(false);
                    _Object_Depth.SetActive(false);
                    _Object_Health.SetActive(false);
                    _Object_ResurrectTotal.SetActive(false);
                    _Object_PropTime.SetActive(false);
                    _Object_Death.SetActive(false);
                    Advertising.DestroyBannerAd();
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
                    _Object_Submarine.SetActive(false);
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
            case Status.Submarine:
                {
                    _Object_Play.SetActive(false);
                    _Object_Prompt.SetActive(false);
                    _Object_Checkpoint.SetActive(false);
                    _Object_Menu.SetActive(true);
                    _Object_MenuGroup.SetActive(false);
                    _Object_Return.SetActive(false);
                    _Object_Close.SetActive(false);
                    _Object_Index.SetActive(false);
                    _Object_Submarine.SetActive(true);
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
                    _Object_Submarine.SetActive(false);
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
                    _Object_Submarine.SetActive(false);
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
                    _Object_Submarine.SetActive(false);
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
                    Advertising.DestroyBannerAd();
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
                    _Object_Submarine.SetActive(false);
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
                    _Object_Submarine.SetActive(false);
                    _Object_Settings.SetActive(false);
                    _Object_InGameMenu.SetActive(false);
                    _Object_Fold.SetActive(false);
                    _Object_ReturnMainMenu.SetActive(false);
                    _Object_Depth.SetActive(true);
                    _Object_Health.SetActive(true);
                    _Object_ResurrectTotal.SetActive(true);
                    //_Object_PropTime.SetActive(null);
                    _Object_Death.SetActive(true);
                    GameManager._Instance.GameState(false);
                    if (GameManager._Instance._ResurrectTotal <= 0) _Button_Resurrect.interactable = false;
                    else _Button_Resurrect.interactable = true;
                    Advertising.ShowBannerAd(BannerAdPosition.Bottom);
                }
                break;
            default:
                Debug.LogWarningFormat("StateChange({0}) is Error!", _status);
                break;
        }
    }
}
