using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Contexts;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Factories.State;
using Code.Infrastructure.Factories.Systems;
using Code.Infrastructure.Factories.Windows;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Audio;
using Code.Infrastructure.Services.CleanUp;
using Code.Infrastructure.Services.Curtain;
using Code.Infrastructure.Services.GameCommander;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.LocalDi;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.Services.Randomizer;
using Code.Infrastructure.Services.SaveLoad;
using Code.Infrastructure.Services.Scene;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.Services.Window;
using Code.Infrastructure.States;
using Code.Infrastructure.States.BootStates;
using Code.Infrastructure.States.GameStates;
using Zenject;

namespace Code.Infrastructure.Installers
{
   public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
   {
      public LoadingCurtain Curtain;
      public AudioService AudioService;

      public override void InstallBindings()
      {
         BindInfrastructureServices();
         BindAssetManagementServices();
         BindAdsServices();
         BindCommonServices();
         BindInputService();
         BindProgressServices();
         BindContexts();
         BindGameplayServices();
         BindStateMachine();
         BindUIFactories();
         BindGameplayFactories();
         BindCleanUpService();
         BindGameStates();
      }

      private void BindInfrastructureServices()
      {
         Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
         Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
         Container.Bind<IWindowService>().To<WindowService>().AsSingle();
         Container.Bind<IRandomService>().To<RandomService>().AsSingle();
         Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
      }

      private void BindAssetManagementServices()
      {
         Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle(); 
         Container.Bind<IAssetDownloadReporter>().To<AssetDownloadReporter>().AsSingle(); 
         Container.Bind<IAssetDownloadService>().To<AssetDownloadService>().AsSingle(); 
      }

      private void BindAdsServices()
      {
      }

      private void BindCommonServices()
      {
         LoadingCurtain curtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(Curtain);
         AudioService audioService = Container.InstantiatePrefabForComponent<AudioService>(AudioService);
         Container.BindInterfacesAndSelfTo<LoadingCurtain>().FromInstance(curtain).AsSingle();
         Container.Bind<IAudioService>().FromInstance(audioService).AsSingle();
         Container.Bind<DIService>().FromInstance(DIService.Instance).AsSingle();
         Container.Bind<ILocalDiService>().FromInstance(LocalDiService.Instance).AsSingle();
         Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().NonLazy();
      }

      private void BindInputService()
      {
         Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
      }

      private void BindProgressServices()
      {
         Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
         Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
      }

      private void BindContexts()
      {
         Container.Bind<InfrastructureContext>().To<InfrastructureContext>().AsSingle();
         Container.Bind<CtxGameContext>().To<CtxGameContext>().AsSingle();
      }

      private void BindGameplayServices()
      {
         Container.Bind<IGameCommandFactory>().To<GameCommandFactory>().AsSingle();
         Container.Bind<IGameCommanderService>().To<GameCommanderService>().AsSingle();
      }

      private void BindStateMachine()
      {
         Container.Bind<IStateFactory>().To<StateFactory>().AsSingle().NonLazy();
         Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
      }

      private void BindUIFactories()
      {
         Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
      }

      private void BindGameplayFactories()
      {
         Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
      }

      private void BindCleanUpService()
      {
         Container.Bind<ICleanUpService>().To<CleanUpService>().AsSingle();
      }

      private void BindGameStates()
      {
         Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
         Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
         Container.BindInterfacesAndSelfTo<LoadLevelState>().AsSingle();
         Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();
         Container.BindInterfacesAndSelfTo<MetaGameState>().AsSingle();
      }

      public void Initialize()
      {
         Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
      }
   }
}