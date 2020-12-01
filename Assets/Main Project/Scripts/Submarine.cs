using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        if (MenuSystem._Instance._Status == MenuSystem.Status.MainMenu)
        {
            MenuSystem._Instance.StateChange(MenuSystem.Status.Submarine);
            Vibration.Vibrate(1);
        }
    }
}
