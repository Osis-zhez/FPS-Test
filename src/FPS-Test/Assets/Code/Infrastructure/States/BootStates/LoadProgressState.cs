using Code.Data;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.Curtain;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.Services.SaveLoad;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.States.GameStates;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States.BootStates
{
   public class LoadProgressState : IState
   {
      private readonly GameStateMachine _gameStateMachine;
      private readonly IPersistentProgressService _progressService;
      private readonly ISaveLoadService _saveLoadProgress;
      private readonly IStaticDataService _staticDataService;
      private readonly IAssetDownloadService _assetDownloadService;
      private readonly LoadingCurtain _curtain;

      public LoadProgressState(GameStateMachine gameStateMachine,
         IPersistentProgressService progressService,
         ISaveLoadService saveLoadProgress,
         IStaticDataService staticDataService,
         IAssetDownloadService assetDownloadService,
         LoadingCurtain curtain)
      {
         _gameStateMachine = gameStateMachine;
         _progressService = progressService;
         _saveLoadProgress = saveLoadProgress;
         _staticDataService = staticDataService;
         _assetDownloadService = assetDownloadService;
         _curtain = curtain;
      }

      public async void Enter()
      {
         LoadProgressOrInitNew();
         
         // await LoadRemoteAssetBundles();
         
         _gameStateMachine.Enter<LevelLoadState, string>("Level 1");
      }

      public void Exit()
      {
      }

      private void LoadProgressOrInitNew()
      {
         _progressService.Progress =
            _saveLoadProgress.LoadProgress()
            ?? NewProgress();
         Debug.Log(PlayerPrefs.GetString("Progress"));
      }

      private async UniTask LoadRemoteAssetBundles()
      {
         await _assetDownloadService.InitializeDownloadDataAsync();
         float downloadSize = _assetDownloadService.GetDownloadSizeMb();
         
         _curtain.AssetDownloadBar.SetDownloadizeText(downloadSize.ToString());

         if (downloadSize > 0)
            await _assetDownloadService.UpdateContentAsync();
      }

      private PlayerProgress NewProgress()
      {
         var progress = new PlayerProgress(_staticDataService, initialLevel: "Level 1");

         return progress;
      }
   }
}