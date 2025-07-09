using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Context;
using Code.Infrastructure.Factories;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Services.CleanUp;
using Code.Infrastructure.Services.Curtain;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.Services.SaveLoad;
using Code.Infrastructure.Services.Scene;
using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States.GameStates
{
  public class MetaGameState : IState, IUpdatable
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly InfrastructureContext _infrastructureContext;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly ICleanUpService _cleanUpService;

    public MetaGameState(ISceneLoader sceneLoader,
      InfrastructureContext infrastructureContext,
      LoadingCurtain loadingCurtain, 
      IGameFactory gameFactory,
      IPersistentProgressService progressService,
      ISaveLoadService saveLoadService,
      ICleanUpService cleanUpService)
    {
      _sceneLoader = sceneLoader;
      _infrastructureContext = infrastructureContext;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
      _cleanUpService = cleanUpService;
    }

    public void Enter()
    {
      _loadingCurtain.Show();
      _cleanUpService.CleanUp();
      _gameFactory.WarmUp();
      _sceneLoader.Load(SceneAddress.Meta, OnLoaded);
    }

    public void Exit()
    {
      _saveLoadService.SaveProgress();
    }

    public void Tick()
    {
      _infrastructureContext.Tick();
    }

    private async void OnLoaded()
    {
      await InitSystems();
      await InitFeatures();
      await InitUI();
      await InitPresenters();

      InformProgressReaders();
      InitAllActors();
      
      _loadingCurtain.Hide();
    }

    private async UniTask InitSystems()
    {
     
    }

    private async UniTask InitFeatures()
    {
    }

    private async UniTask InitUI()
    {
    }

    private async UniTask InitPresenters()
    {
    
    }

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReader in _infrastructureContext.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private void InitAllActors()
    {
      foreach (IInitialize starter in _infrastructureContext.Initializes) 
        starter.Initialize();
    }
  }
}