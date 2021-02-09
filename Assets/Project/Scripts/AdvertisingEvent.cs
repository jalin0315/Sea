using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class AdvertisingEvent : MonoBehaviour
    {
        public static bool _Reward_Play_01_Ad_00;
        public static bool _Reward_Play_02_Ad_00;
        public static bool _Reward_Play_02_Ad_01;
        public static bool _Reward_Resurrect;
        public static bool _Interstitial_MaxHealth;

        private void Start()
        {
            _Reward_Play_01_Ad_00 = false;
            _Reward_Play_02_Ad_00 = false;
            _Reward_Play_02_Ad_01 = false;
            _Reward_Resurrect = false;
            _Interstitial_MaxHealth = false;
        }

        private void OnEnable()
        {
            Advertising.InterstitialAdCompleted += InterstitialAdCompletedHandler;
            Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
            Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
            Advertising.AdsRemoved += AdsRemovedHandler;
        }

        private void OnDisable()
        {
            Advertising.InterstitialAdCompleted -= InterstitialAdCompletedHandler;
            Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
            Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
        }

        private void InterstitialAdCompletedHandler(InterstitialAdNetwork _interstitial_ad_network, AdPlacement _ad_placement)
        {
            Logger.Log("Interstitial ad has been closed.");
            if (_Interstitial_MaxHealth)
            {
                Player._Instance.MaxHealth();
                _Interstitial_MaxHealth = false;
            }
        }

        private void RewardedAdCompletedHandler(RewardedAdNetwork _rewarded_ad_network, AdPlacement _ad_placement)
        {
            Logger.Log("Rewarded ad has completed. The user should be rewarded now.");
            if (_Reward_Play_01_Ad_00)
            {
                Database._Play_01_Unlock_00 = 1;
                MenuSystem._Instance.OnButtonPlay();
                _Reward_Play_01_Ad_00 = false;
            }
            if (_Reward_Play_02_Ad_00)
            {
                Database._Play_02_Unlock_00 = 1;
                MenuSystem._Instance.OnButtonPlay();
                _Reward_Play_02_Ad_00 = false;
            }
            if (_Reward_Play_02_Ad_01)
            {
                Database._Play_02_Unlock_01 = 1;
                MenuSystem._Instance.OnButtonPlay();
                _Reward_Play_02_Ad_01 = false;
            }
            if (_Reward_Resurrect)
            {
                Player._Instance.Resurrection();
                GameManager._Instance.ResurrectControl(-1);
                _Reward_Resurrect = false;
            }
        }

        private void RewardedAdSkippedHandler(RewardedAdNetwork _rewarded_ad_network, AdPlacement _ad_placement)
        {
            Logger.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
            _Reward_Play_01_Ad_00 = false;
            _Reward_Play_02_Ad_00 = false;
            _Reward_Play_02_Ad_01 = false;
            _Reward_Resurrect = false;
            Advertising.LoadRewardedAd();
        }

        private void AdsRemovedHandler()
        {
            Logger.Log("Ads were removed.");
            // Unsubscribe
            Advertising.AdsRemoved -= AdsRemovedHandler;
        }
    }
}
