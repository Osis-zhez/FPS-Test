using Code.Infrastructure.Contexts;
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
   public class LoadLevelState : IPayloadedState<string>
   {
      private readonly GameStateMachine _stateMachine;
      private readonly SceneLoader _sceneLoader;
      private readonly LoadingCurtain _loadingCurtain;
      private readonly IGameFactory _gameFactory;
      private readonly IPersistentProgressService _progressService;
      private readonly ICleanUpService _cleanUpService;
      private readonly InfrastructureContext _infrastructureContext;

      private LevelStaticData _levelData;
      private IGameCommanderService _gameCommander;

      public LoadLevelState(GameStateMachine stateMachine,
         SceneLoader sceneLoader, 
         LoadingCurtain loadingCurtain, 
         IGameFactory gameFactory,
         IPersistentProgressService progressService,
         ICleanUpService cleanUpService,
         InfrastructureContext infrastructureContext,
         IGameCommanderService gameCommander)
      {
         _stateMachine = stateMachine;
         _sceneLoader = sceneLoader;
         _loadingCurtain = loadingCurtain;
         _gameFactory = gameFactory;
         _progressService = progressService;
         _cleanUpService = cleanUpService;
         _infrastructureContext = infrastructureContext;
         _gameCommander = gameCommander;
      }

      public void Enter(string levelDataName)
      {
         Debug.Log("Enter");
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
         //здесь через async идет отход в другой поток, и сцена живет своей жизнь и старт запускается асинхронно
         //Сначала создаются игровые системы
         //Потом поверх них создаются Features, Типа WeaponFeature, таким образом системы обмениются данными через context, а общаются через фичи
         await InitGameSystems();
         await InitGameFeatures();
         await InitGameWorld(_levelData); 
         await InitUI();
         await InitPresenters();
         
         InformProgressReaders();
         InitAllActors();

         
         _stateMachine.Enter<GameLoopState>();
      }

      private async UniTask InitGameSystems()
      {
      }

      private async UniTask InitGameFeatures()
      {
      }

      private async UniTask InitGameWorld(LevelStaticData levelData)
      {
         // сделать CommonFeature
         // сделать PlayerFeature
         // сделать WeaponEventFeature
      }

      private async UniTask InitUI()
      {
      }

      private async UniTask InitPresenters()
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