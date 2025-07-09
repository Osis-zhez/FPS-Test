using Code.Infrastructure.Context;
using Code.Infrastructure.Factories;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Services.CleanUp;
using Code.Infrastructure.Services.Curtain;
using Code.Infrastructure.Services.GameCommander;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.Services.Scene;
using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
   public class LevelLoadState : IPayloadedState<string>
   {
      private readonly IGameStateMachine _stateMachine;
      private readonly ISceneLoader _sceneLoader;
      private readonly LoadingCurtain _loadingCurtain;
      private readonly IGameFactory _gameFactory;
      private readonly IPersistentProgressService _progressService;
      private readonly ICleanUpService _cleanUpService;
      private readonly InfrastructureContext _infrastructureContext;

      private LevelStaticData _levelData;

      public LevelLoadState(IGameStateMachine stateMachine,
         ISceneLoader sceneLoader, 
         LoadingCurtain loadingCurtain, 
         IGameFactory gameFactory,
         IPersistentProgressService progressService,
         ICleanUpService cleanUpService,
         InfrastructureContext infrastructureContext)
      {
         _stateMachine = stateMachine;
         _sceneLoader = sceneLoader;
         _loadingCurtain = loadingCurtain;
         _gameFactory = gameFactory;
         _progressService = progressService;
         _cleanUpService = cleanUpService;
         _infrastructureContext = infrastructureContext;
      }

      public void Enter(string levelDataName)
      {
         _loadingCurtain.Show();
         _cleanUpService.CleanUp();
         _gameFactory.WarmUp();
         _levelData = _gameFactory.LoadLevelData(levelDataName);
         _sceneLoader.Load(_levelData.GetSceneId(), OnLoaded);
      }

      public void Exit()
      {
         _loadingCurtain.Hide();
      }

      private async void OnLoaded()
      {
         await InitGameSystems();
         await InitGameWorld(_levelData); 
         await InitUI();
         
         InformProgressReaders();
         InitAllActors();
         
         _stateMachine.Enter<LevelLoopState>();
      }

      private async UniTask InitGameSystems()
      {
      }
      
      private async UniTask InitGameWorld(LevelStaticData levelData)
      {
       
      }

      private async UniTask InitUI()
      {
      }
      
      private void InitAllActors()
      {
         foreach (IInitialize initialize in _infrastructureContext.Initializes) 
            initialize.Initialize();
      }

      private void InformProgressReaders()
      {
         Debug.Log(PlayerPrefs.GetString("Progress"));
         foreach (ISavedProgressReader progressReader in _infrastructureContext.ProgressReaders)
            progressReader.LoadProgress(_progressService.Progress);
         foreach (ISavedProgressReader progressWriter in _infrastructureContext.ProgressWriters)
            progressWriter.LoadProgress(_progressService.Progress);
      }
   }
}