using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.Ads;
using Code.Infrastructure.Services.Analytics;
using Code.Infrastructure.Services.Audio;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.LevelMode.ServicesExam.Behaviours
{
  public class ServicesExample : MonoBehaviour
  {
    private IAssetProvider _assets;
    private IAudioService _audioService;
    private IAnalyticService _analyticService;
    private IAdsService _adsService;

    [Inject]
    public void Construct(IAssetProvider assets,
      IAudioService audioService,
      IAnalyticService analyticService,
      IAdsService adsService)
    {
      _assets = assets;
      _audioService = audioService;
      _analyticService = analyticService;
      _adsService = adsService;
    }

    public void ExampleMethod()
    {
      _assets.LoadAsset("ExampleAsset");
      
      _audioService.PlaySfx(AudioId.None);
      
      _analyticService.LogMessage("Example Log");
      
      _adsService.ShowInterstitial();
    }
  }
}