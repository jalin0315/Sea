using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Timeline : MonoBehaviour
{
    public static Timeline _Instance;
    public PlayableDirector _Prelude;
    public PlayableDirector _Idle;
    public PlayableDirector _Opening;
    public PlayableDirector _FadeIn;
    public PlayableDirector _FadeOut;
    public PlayableDirector _ReturnMainMenu;
    public bool _SkipEnable;
    [SerializeField] private AudioSource[] _AudioSource;

    private void Awake()
    {
        _Instance = this;
    }

    public void Skip()
    {
        if (_Prelude.state == PlayState.Playing) _Prelude.time = 8.0d;
        else if (_Opening.state == PlayState.Playing) _Opening.time = 8.0d;
    }
    public void SkipEnable(bool _enable) => _SkipEnable = _enable;
    public void AudioEnable(bool _enable)
    {
        if (_enable)
        {
            for (int _i = 0; _i < _AudioSource.Length; _i++) _AudioSource[_i].volume = 1.0f;
            return;
        }
        if (!_enable)
        {
            for (int _i = 0; _i < _AudioSource.Length; _i++) _AudioSource[_i].volume = 0.0f;
            return;
        }
    }
}
