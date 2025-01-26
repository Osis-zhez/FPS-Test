using System;

namespace Code.Infrastructure.Services.Ads
{
   public interface IAdsService
   {
      event Action OnRewardVideoReady;
      void Initialize();
      void ShowInterstitial();
      void ShowRewardedVideo(Action onVideoFinished);
      bool IsRewardedVideoReady();
      bool IsInterstitialReady();
   }
}