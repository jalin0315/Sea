using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
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
            Logger.LogWarning("User logged in successfully.");
        }
        private void OnUserLoginFailed()
        {
            Logger.LogWarning("User login failed.");
        }
    }
}
