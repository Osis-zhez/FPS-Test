// using System;
// using AppodealStack.Monetization.Api;
// using AppodealStack.Monetization.Common;
// using CodeBase.Infrastructure.Services.Analytics;
// using UnityEngine;
//
// namespace CodeBase.Infrastructure.Services.Ads
// {
//    public class AdsService : IAdsService, IRewardedVideoAdListener, IInterstitialAdListener
//    {
//       private readonly IAnalyticService _analyticService;
//       private Action _onInterstitialFinished;
//       private Action _onVideoFinished;
//       
//       private const string AndroidGameId = "11111";
//       private const string IOSGameId = "22222";
//
//       private const string RewardedVideoPlacementId = "rewardVideo";
//       
//       private string _gameId;
//
//       public event Action OnRewardVideoReady;
//
//       public AdsService(IAnalyticService analyticService)
//       {
//          _analyticService = analyticService;
//       }
//
//       public void Initialize()
//       {
//          switch (Application.platform)
//          {
//             case RuntimePlatform.Android:
//                _gameId = AndroidGameId;
//                break;
//             case RuntimePlatform.IPhonePlayer:
//                _gameId = IOSGameId;
//                break;
//             case RuntimePlatform.WindowsEditor:
//                _gameId = AndroidGameId;
//                break;
//             default:
//                Debug.Log("Unsupported playform for ads");
//                break;
//          }
//          
//          int adTypes = AppodealAdType.RewardedVideo | AppodealAdType.Interstitial ;
//          string appKey = "0d19343fa5210bcce3efeaa28efe661a098e82e8b4cc9284";
//          Appodeal.SetTesting(false);
//          AppodealCallbacks.Sdk.OnInitialized += OnInitilizationFinished;
//          Appodeal.Initialize(appKey, adTypes);
//          Appodeal.SetRewardedVideoCallbacks(this);
//          Appodeal.SetInterstitialCallbacks(this);
//       }
//
//       private void OnInitilizationFinished(object sender, SdkInitializedEventArgs e)
//       {
//          
//       }
//       
//       public void ShowInterstitial()
//       {
//          Debug.Log(IsInterstitialReady());
//          if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
//          {
//             Time.timeScale = 0;
//             Appodeal.Show(AppodealShowStyle.Interstitial);
//          }
//       }
//     
//       public void ShowRewardedVideo(Action onVideoFinished)
//       {
//          if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
//          {
//             Time.timeScale = 0;
//             Appodeal.Show(AppodealShowStyle.RewardedVideo);
//             // GameAnalytics.Instance.RewardedAd();
//             _onVideoFinished = onVideoFinished;
//          }
//       }
//       
//       public bool IsRewardedVideoReady() => 
//          Appodeal.IsLoaded(AppodealAdType.RewardedVideo);
//       
//       public bool IsInterstitialReady() => 
//          Appodeal.IsLoaded(AppodealAdType.Interstitial);
//
//       #region RewardedVideo
//       public void OnRewardedVideoLoaded(bool isPrecache)
//       {
//          Debug.Log($"Reward video ready");
//          
//          OnRewardVideoReady?.Invoke();
//       }
//
//       public void OnRewardedVideoFailedToLoad()
//       {
//          Debug.Log("Reward Video Failed");
//       }
//
//       public void OnRewardedVideoShowFailed()
//       {
//          Debug.Log("Reward Shown Failed");
//       }
//
//       public void OnRewardedVideoShown()
//       {
//          
//       }
//
//       public void OnRewardedVideoFinished(double amount, string currency)
//       {
//          _onVideoFinished?.Invoke();
//          _onVideoFinished = null;
//       }
//
//       public void OnRewardedVideoClosed(bool finished)
//       {
//       }
//
//       public void OnRewardedVideoExpired()
//       {
//       }
//
//       public void OnRewardedVideoClicked()
//       {
//       }
//       
//       #endregion
//       
//       
//       #region Interstitial
//       public void OnInterstitialLoaded(bool isPrecache)
//       {
//          Debug.Log("Interstitial loaded");
//       }
//
//       public void OnInterstitialFailedToLoad()
//       {
//          Debug.Log("Interstitial fail to load");
//       }
//
//       public void OnInterstitialShowFailed()
//       {
//          Debug.Log("Interstitial show failed");
//       }
//
//       public void OnInterstitialShown()
//       {
//          Debug.Log("Interstitial show");
//       }
//
//       public void OnInterstitialClosed()
//       {
//          Debug.Log("Interstitial closed");
//          
//          Time.timeScale = 1;
//          _onInterstitialFinished?.Invoke();
//          _onInterstitialFinished = null;
//       }
//
//       public void OnInterstitialClicked()
//       {
//       }
//
//       public void OnInterstitialExpired()
//       {
//          
//       }
//       
//       #endregion
//    }
// }