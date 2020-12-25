using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public void Initialize()
    {
        background.gameObject.SetActive(false);
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!CTJ.GameManager._InGame) return;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!CTJ.GameManager._InGame) return;
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}
