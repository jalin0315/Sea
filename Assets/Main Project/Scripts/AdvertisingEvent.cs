using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class AdvertisingEvent : MonoBehaviour
    {
        public static bool _Reward_Resurrect;
        public static bool _Reward_MaxHealth;

        private void Start()
        {
            _Reward_MaxHealth = false;
            _Reward_MaxHealth = false;
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
            Debug.Log("Interstitial ad has been closed.");
        }

        private void RewardedAdCompletedHandler(RewardedAdNetwork _rewarded_ad_network, AdPlacement _ad_placement)
        {
            Debug.Log("Rewarded ad has completed. The user should be rewarded now.");
            if (_Reward_Resurrect)
            {
                Player._Instance.DeathDisable();
                GameManager._Instance.ResurrectControl(-1);
                _Reward_Resurrect = false;
            }
            if (_Reward_MaxHealth)
            {
                Player._Instance.MaxHealth();
                _Reward_MaxHealth = false;
            }
        }

        private void RewardedAdSkippedHandler(RewardedAdNetwork _rewarded_ad_network, AdPlacement _ad_placement)
        {
            Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
            _Reward_Resurrect = false;
            _Reward_MaxHealth = false;
            Advertising.LoadRewardedAd();
        }

        private void AdsRemovedHandler()
        {
            Debug.Log("Ads were removed.");
            // Unsubscribe
            Advertising.AdsRemoved -= AdsRemovedHandler;
        }
    }
}
