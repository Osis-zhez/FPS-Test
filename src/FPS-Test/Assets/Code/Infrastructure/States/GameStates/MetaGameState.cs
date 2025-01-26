using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Contexts;
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
    private readonly SceneLoader _sceneLoader;
    private readonly InfrastructureContext _infrastructureContext;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly ICleanUpService _cleanUpService;

    private LevelStaticData _levelData;

    public MetaGameState(SceneLoader sceneLoader,
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
      _levelData = _gameFactory.LoadLevelData(_progressService.Progress.WorldData.PositionOnLevel.Level);
      _sceneLoader.Load(ScenesAddress.Meta, OnLoaded);
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
      // await _gameFactory.CreateLevelServiceRoot();
      // await _gameFactory.CreateGameSystem<ZoomMapSystem>();
      // await _gameFactory.CreateGameSystem<MoveMapSystem>();
      // await _gameFactory.CreateGameSystem<MapLevelSelectSystem>();
      // await _gameFactory.CreateGameSystem<UpgradeSystem>(AssetAddress.UpgradeSystem);
    }

    private async UniTask InitFeatures()
    {
    }

    private async UniTask InitUI()
    {
    }

    private async UniTask InitPresenters()
    {
      // _presenterFactory.CreatePresenter<UpgradeWindowPresenter>();
      // _presenterFactory.CreatePresenter<TeamWindowPresenter>();
      // _presenterFactory.CreatePresenter<BusWindowPresenter>();
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