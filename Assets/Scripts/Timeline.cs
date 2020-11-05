using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour
{
    public static Timeline _Instance;
    public PlayableDirector _Idle;
    public PlayableDirector _OpeningAnimation;
    public PlayableDirector _TransitionsFadeIn;
    public PlayableDirector _TransitionsFadeOut;
    public PlayableDirector _TransitionsReturnMenu;

    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        _Idle.Play();
    }
}
