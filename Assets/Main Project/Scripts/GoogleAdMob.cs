using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CTJ
{
    public class GoogleAdMob : MonoBehaviour
    {
        public static GoogleAdMob _Instance;
        private BannerView _BannerView;
        private InterstitialAd _InterstitialAd;
        private RewardedAd _RewardedAd;
        public bool _Resurrect;
        public bool _MaxHealthPower;

        private void Awake()
        {
            _Instance = this;
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(initStatus => { });
            RequestBanner();
            RequestInterstitialAd();
            RequestRewardedAd();
        }

        private void RequestBanner()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif
            // Create a 320x50 banner at the top of the screen.
            _BannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

            // Called when an ad request has successfully loaded.
            _BannerView.OnAdLoaded += HandleOnBannerAdLoaded;
            // Called when an ad request failed to load.
            _BannerView.OnAdFailedToLoad += HandleOnBannerAdFailedToLoad;
            // Called when an ad is clicked.
            _BannerView.OnAdOpening += HandleOnBannerAdOpened;
            // Called when the user returned from the app after an ad click.
            _BannerView.OnAdClosed += HandleOnBannerAdClosed;
            // Called when the ad click caused the user to leave the application.
            _BannerView.OnAdLeavingApplication += HandleOnBannerAdLeavingApplication;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the banner with the request.
            _BannerView.LoadAd(request);
            _BannerView.Hide();
        }
        private void HandleOnBannerAdLoaded(object sender, EventArgs args)
        {
            print("HandleBannerAdLoaded event received");
        }
        private void HandleOnBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            print("HandleFailedToReceiveBannerAd event received with message: " + args.Message);
        }
        private void HandleOnBannerAdOpened(object sender, EventArgs args)
        {
            print("HandleBannerAdOpened event received");
        }
        private void HandleOnBannerAdClosed(object sender, EventArgs args)
        {
            print("HandleBannerAdClosed event received");
        }
        private void HandleOnBannerAdLeavingApplication(object sender, EventArgs args)
        {
            print("HandleBannerAdLeavingApplication event received");
        }
        public void ShowBanner() => _BannerView.Show();
        public void HideBanner() => _BannerView.Hide();

        private void RequestInterstitialAd()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
            // Initialize an InterstitialAd.
            _InterstitialAd = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            _InterstitialAd.OnAdLoaded += HandleOnInterstitialAdLoaded;
            // Called when an ad request failed to load.
            _InterstitialAd.OnAdFailedToLoad += HandleOnInterstitialAdFailedToLoad;
            // Called when an ad is shown.
            _InterstitialAd.OnAdOpening += HandleOnInterstitialAdOpened;
            // Called when the ad is closed.
            _InterstitialAd.OnAdClosed += HandleOnInterstitialAdClosed;
            // Called when the ad click caused the user to leave the application.
            _InterstitialAd.OnAdLeavingApplication += HandleOnInterstitialAdLeavingApplication;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            _InterstitialAd.LoadAd(request);
        }
        public void InterstitialAd()
        {
            if (_InterstitialAd.IsLoaded()) _InterstitialAd.Show();
            else if (!_InterstitialAd.IsLoaded()) RequestInterstitialAd();
        }
        private void HandleOnInterstitialAdLoaded(object sender, EventArgs args)
        {
            print("HandleInterstitialAdLoaded event received");
        }
        private void HandleOnInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            print("HandleFailedToReceiveInterstitialAd event received with message: " + args.Message);
        }
        private void HandleOnInterstitialAdOpened(object sender, EventArgs args)
        {
            print("HandleInterstitialAdOpened event received");
        }
        private void HandleOnInterstitialAdClosed(object sender, EventArgs args)
        {
            print("HandleInterstitialAdClosed event received");
            _InterstitialAd.Destroy();
            RequestInterstitialAd();
        }
        private void HandleOnInterstitialAdLeavingApplication(object sender, EventArgs args)
        {
            print("HandleInterstitialAdLeavingApplication event received");
        }

        private void RequestRewardedAd()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif
            // Initialize an InterstitialAd.
            _RewardedAd = new RewardedAd(adUnitId);

            // Called when an ad request has successfully loaded.
            _RewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            _RewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            _RewardedAd.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            _RewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            _RewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            _RewardedAd.OnAdClosed += HandleRewardedAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            _RewardedAd.LoadAd(request);
        }
        public void Resurrect(bool _enable)
        {
            _Resurrect = _enable;
            if (_RewardedAd.IsLoaded()) _RewardedAd.Show();
        }
        public void MaxHealthPower(bool _enable)
        {
            _MaxHealthPower = _enable;
            if (_RewardedAd.IsLoaded()) _RewardedAd.Show();
            else if (!_RewardedAd.IsLoaded()) RequestRewardedAd();
        }
        private void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            print("HandleRewardedAdLoaded event received");
        }
        private void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
        {
            print("HandleRewardedAdFailedToLoad event received with message: " + args.Message);
        }
        private void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            print("HandleRewardedAdOpening event received");
            Time.timeScale = 0.0f;
        }
        private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            print("HandleRewardedAdFailedToShow event received with message: " + args.Message);
            RequestRewardedAd();
        }
        private void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            print("HandleRewardedAdClosed event received");
            _Resurrect = false;
            _MaxHealthPower = false;
            if (MenuSystem._Instance._Status == MenuSystem.Status.InGame) Time.timeScale = 1.0f;
            RequestRewardedAd();
        }
        private void HandleUserEarnedReward(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
            if (_Resurrect)
            {
                Player._Instance.DeathDisable();
                GameManager._Instance.ResurrectControl(-1);
                _Resurrect = false;
                return;
            }
            if (_MaxHealthPower)
            {
                Player._Instance.MaxHealth();
                _MaxHealthPower = false;
                return;
            }
        }
    }
}
