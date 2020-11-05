using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public static MenuSystem _Instance;
    public enum Status
    {
        None = -1,
        ShowSettings,
        HighScore,
        Achievement,
        IllustratedBook,
        Submarine,
        Animation,
        InGame,
        ShowInGameMenu
    }
    public Status _Status;
    [Space(20)]
    [SerializeField] private GameObject _Object_Play;
    [SerializeField] private GameObject _Object_Prompt;
    [SerializeField] private GameObject _Object_Settings;
    [SerializeField] private GameObject _Object_InGameMenu;
    [SerializeField] private GameObject _Object_Fold;
    [SerializeField] private GameObject _Object_Cancel;
    [SerializeField] private GameObject _Object_Audio;
    [SerializeField] private GameObject _Object_HighScore;
    [SerializeField] private GameObject _Object_ReturnMainMenu;
    [SerializeField] private GameObject _Object_Achievement;
    [SerializeField] private GameObject _Object_IllustratedBook;
    [SerializeField] private GameObject _Object_Advertising;
    [Space(20)]
    /*
    [SerializeField] private Image _Image_Play;
    [SerializeField] private Image _Image_Settings;
    [SerializeField] private Image _Image_Cancel;
    [SerializeField] private Image _Image_HighScore;
    [SerializeField] private Image _Image_Achievement;
    [SerializeField] private Image _Image_IllustratedBook;
    */
    [Space(20)]
    [SerializeField] private Button _Button_Play;
    [SerializeField] private Button _Button_Settings;
    [SerializeField] private Button _Button_InGameMenu;
    [SerializeField] private Button _Button_Fold;
    [SerializeField] private Button _Button_Cancel;
    [SerializeField] private Button _Button_Audio;
    [SerializeField] private Button _Button_HighScore;
    [SerializeField] private Button _Button_ReturnMainMenu;
    [SerializeField] private Button _Button_Achievement;
    [SerializeField] private Button _Button_IllustratedBook;
    [Space(20)]
    [SerializeField] private Image _Image_Audio;
    [Space(20)]
    [SerializeField] private Sprite _Sprite_Audio_UnMute;
    [SerializeField] private Sprite _Sprite_Audio_Mute;
    [Space(20)]
    [SerializeField] private Text _Text_Prompt;

    private void Awake() => _Instance = this;

    private void Start()
    {
        StateChange(Status.None);
        _Button_Play.onClick.AddListener(OnButtonPlay);
        _Button_Settings.onClick.AddListener(OnButtonSettings);
        _Button_InGameMenu.onClick.AddListener(OnButtonInGameMenu);
        _Button_Fold.onClick.AddListener(OnButtonFold);
        _Button_Cancel.onClick.AddListener(OnButtonCancel);
        _Button_Audio.onClick.AddListener(OnButtonAudio);
        _Button_HighScore.onClick.AddListener(OnButtonHighScore);
        _Button_ReturnMainMenu.onClick.AddListener(OnButtonReturnMainMenu);
        _Button_Achievement.onClick.AddListener(OnButtonAchievement);
        _Button_IllustratedBook.onClick.AddListener(OnButtonIllustratedBook);
        _Text_Prompt.text = "Click to Start";
    }

    private void OnButtonPlay()
    {
        StateChange(Status.Animation);
        Timeline._Instance._Idle.Stop();
        Timeline._Instance._OpeningAnimation.Play();
    }
    private void OnButtonSettings() => StateChange(Status.ShowSettings);
    private void OnButtonInGameMenu() => StateChange(Status.ShowInGameMenu);
    private void OnButtonFold()
    {
        if (_Status == Status.ShowInGameMenu) StateChange(Status.InGame);
        else StateChange(Status.None);
    }
    private void OnButtonCancel() => StateChange(Status.None);
    private void OnButtonAudio()
    {
        if (_Image_Audio.sprite == _Sprite_Audio_UnMute) _Image_Audio.sprite = _Sprite_Audio_Mute;
        else _Image_Audio.sprite = _Sprite_Audio_UnMute;
    }
    private void OnButtonHighScore() => StateChange(Status.HighScore);
    private void OnButtonReturnMainMenu()
    {
        GameManager._Instance._InGame = false;
        Timeline._Instance._TransitionsReturnMenu.Play();
        StateChange(Status.Animation);
    }
    private void OnButtonAchievement() => StateChange(Status.Achievement);
    private void OnButtonIllustratedBook() => StateChange(Status.IllustratedBook);

    public void StateChange(Status _status)
    {
        _Status = _status;
        if (_Status == Status.None)
        {
            _Object_Play.SetActive(true);
            _Object_Prompt.SetActive(true);
            _Object_Settings.SetActive(true);
            _Object_InGameMenu.SetActive(false);
            _Object_Fold.SetActive(false);
            _Object_Cancel.SetActive(false);
            _Object_Audio.SetActive(false);
            _Object_HighScore.SetActive(false);
            _Object_ReturnMainMenu.SetActive(false);
            _Object_Achievement.SetActive(false);
            _Object_IllustratedBook.SetActive(false);
            _Object_Advertising.SetActive(true);
            GameManager._Instance._Text_Depth.text = null;
            Player._Instance._Text_Health.text = null;
            return;
        }
        if (_Status == Status.ShowSettings)
        {
            _Object_Play.SetActive(false);
            _Object_Prompt.SetActive(false);
            _Object_Settings.SetActive(false);
            _Object_InGameMenu.SetActive(false);
            _Object_Fold.SetActive(true);
            _Object_Cancel.SetActive(false);
            _Object_Audio.SetActive(true);
            _Object_HighScore.SetActive(true);
            _Object_ReturnMainMenu.SetActive(false);
            _Object_Achievement.SetActive(true);
            _Object_IllustratedBook.SetActive(true);
            _Object_Advertising.SetActive(true);
            return;
        }
        if (_Status == Status.HighScore)
        {
            _Object_Play.SetActive(false);
            _Object_Prompt.SetActive(false);
            _Object_Settings.SetActive(false);
            _Object_InGameMenu.SetActive(false);
            _Object_Fold.SetActive(false);
            _Object_Cancel.SetActive(true);
            _Object_Audio.SetActive(false);
            _Object_HighScore.SetActive(false);
            _Object_ReturnMainMenu.SetActive(false);
            _Object_Achievement.SetActive(false);
            _Object_IllustratedBook.SetActive(false);
            _Object_Advertising.SetActive(false);
            return;
        }
        if (_Status == Status.Achievement)
        {
            _Object_Play.SetActive(false);
            _Object_Prompt.SetActive(false);
            _Object_Settings.SetActive(false);
            _Object_InGameMenu.SetActive(false);
            _Object_Fold.SetActive(false);
            _Object_Cancel.SetActive(true);
            _Object_Audio.SetActive(false);
            _Object_HighScore.SetActive(false);
            _Object_ReturnMainMenu.SetActive(false);
            _Object_Achievement.SetActive(false);
            _Object_IllustratedBook.SetActive(false);
            _Object_Advertising.SetActive(false);
            return;
        }
        if (_Status == Status.IllustratedBook)
        {
            _Object_Play.SetActive(false);
            _Object_Prompt.SetActive(false);
            _Object_Settings.SetActive(false);
            _Object_InGameMenu.SetActive(false);
            _Object_Fold.SetActive(false);
            _Object_Cancel.SetActive(true);
            _Object_Audio.SetActive(false);
            _Object_HighScore.SetActive(false);
            _Object_ReturnMainMenu.SetActive(false);
            _Object_Achievement.SetActive(false);
            _Object_IllustratedBook.SetActive(false);
            _Object_Advertising.SetActive(false);
            return;
        }
        if (_Status == Status.Submarine)
        {
            _Object_Play.SetActive(false);
            _Object_Prompt.SetActive(false);
            _Object_Settings.SetActive(false);
            _Object_InGameMenu.SetActive(false);
            _Object_Fold.SetActive(false);
            _Object_Cancel.SetActive(true);
            _Object_Audio.SetActive(false);
            _Object_HighScore.SetActive(false);
            _Object_ReturnMainMenu.SetActive(false);
            _Object_Achievement.SetActive(false);
            _Object_IllustratedBook.SetActive(false);
            _Object_Advertising.SetActive(false);
            return;
        }
        if (_Status == Status.Animation)
        {
            _Object_Play.SetActive(false);
            _Object_Prompt.SetActive(false);
            _Object_Settings.SetActive(false);
            _Object_InGameMenu.SetActive(false);
            _Object_Fold.SetActive(false);
            _Object_Cancel.SetActive(false);
            _Object_Audio.SetActive(false);
            _Object_HighScore.SetActive(false);
            _Object_ReturnMainMenu.SetActive(false);
            _Object_Achievement.SetActive(false);
            _Object_IllustratedBook.SetActive(false);
            _Object_Advertising.SetActive(false);
            return;
        }
        if (_Status == Status.InGame)
        {
            _Object_Play.SetActive(false);
            _Object_Prompt.SetActive(false);
            _Object_Settings.SetActive(false);
            _Object_InGameMenu.SetActive(true);
            _Object_Fold.SetActive(false);
            _Object_Cancel.SetActive(false);
            _Object_Audio.SetActive(false);
            _Object_HighScore.SetActive(false);
            _Object_ReturnMainMenu.SetActive(false);
            _Object_Achievement.SetActive(false);
            _Object_IllustratedBook.SetActive(false);
            _Object_Advertising.SetActive(false);
            return;
        }
        if (_Status == Status.ShowInGameMenu)
        {
            _Object_Play.SetActive(false);
            _Object_Prompt.SetActive(false);
            _Object_Settings.SetActive(false);
            _Object_InGameMenu.SetActive(false);
            _Object_Fold.SetActive(true);
            _Object_Cancel.SetActive(false);
            _Object_Audio.SetActive(true);
            _Object_HighScore.SetActive(false);
            _Object_ReturnMainMenu.SetActive(true);
            _Object_Achievement.SetActive(false);
            _Object_IllustratedBook.SetActive(false);
            _Object_Advertising.SetActive(false);
        }
    }
}
