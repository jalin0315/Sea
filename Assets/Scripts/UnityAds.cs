using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAds: MonoBehaviour, IUnityAdsListener
{
    public static UnityAds _Instance;
    private readonly string _GameID_Android = "3870441";
    private readonly string _GameID_Ios = "3870440";
    private readonly string _PlacementID_Video = "video";
    private readonly string _PlacementID_RewardedVideo = "rewardedVideo";
    private readonly string _PlacementID_Banner = "banner";
    private readonly bool _TestMode = true;
    [SerializeField] private Button _Button_RewardedVideo;

    private void Awake() => _Instance = this;

    private void Start()
    {
        Advertisement.AddListener(this);
#if UNITY_ANDROID
        Advertisement.Initialize(_GameID_Android, _TestMode);
#elif UNITY_IOS
        Advertisement.Initialize(_GameID_Ios, _TestMode);
#endif
        //_Button_RewardedVideo.interactable = Advertisement.IsReady(_PlacementID_RewardedVideo);
        if (_Button_RewardedVideo) _Button_RewardedVideo.onClick.AddListener(ShowRewardedVideo);
        //StartCoroutine(ShowBanner());
    }

    /*
    public void ShowInterstitialVideo()
    {
        // 調用插頁式廣告之前 確認廣告是否載入成功
        if (Advertisement.IsReady(_PlacementID_Video)) Advertisement.Show(_PlacementID_Video);
        else Debug.LogWarning("Interstitial ad not ready at the moment! Please try again later!");
    }
    */
    public void ShowRewardedVideo()
    {
        // 調用獎勵廣告之前 確認廣告是否載入成功
        if (Advertisement.IsReady(_PlacementID_RewardedVideo)) Advertisement.Show(_PlacementID_RewardedVideo);
        else Debug.LogWarning("Rewarded video is not ready at the moment! Please try again later!");
    }
    private IEnumerator ShowBanner()
    {
        while (!Advertisement.isInitialized) yield return new WaitForSeconds(0.5f);
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(_PlacementID_Banner);
    }

    // 實現 IUnityAdsListener 接口函式
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // 廣告狀態邏輯條件
        if (showResult == ShowResult.Finished) Debug.Log("You got rewarded!");
        else if (showResult == ShowResult.Skipped) Debug.Log("You skipped the ad.");
        else if (showResult == ShowResult.Failed) Debug.LogWarning("The ad did not finish due to an error.");
    }
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == _PlacementID_RewardedVideo)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
            //Debug.Log("Ads rewarded ready!");
        }
    }
    public void OnUnityAdsDidError(string message) => Debug.LogError(message);
    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
        Debug.Log(placementId);
    }
    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy() => Advertisement.RemoveListener(this);
}
