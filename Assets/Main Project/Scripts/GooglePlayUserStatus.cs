using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayUserStatus : MonoBehaviour
{
    private void OnEnable()
    {
        GameServices.UserLoginSucceeded += OnUserLoginSucceeded;
        GameServices.UserLoginFailed += OnUserLoginFailed;
    }
    private void OnDisable()
    {
        GameServices.UserLoginSucceeded -= OnUserLoginSucceeded;
        GameServices.UserLoginFailed -= OnUserLoginFailed;
    }

    private void OnUserLoginSucceeded()
    {
        Debug.LogWarning("User logged in successfully.");
    }
    private void OnUserLoginFailed()
    {
        Debug.LogWarning("User login failed.");
    }
}
