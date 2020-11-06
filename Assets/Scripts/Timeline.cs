using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour
{
    public static Timeline _Instance;
    public PlayableDirector _Prelude;
    public PlayableDirector _Idle;
    public PlayableDirector _Opening;
    public PlayableDirector _FadeIn;
    public PlayableDirector _FadeOut;
    public PlayableDirector _ReturnMainMenu;

    private void Awake()
    {
        _Instance = this;
    }
}
