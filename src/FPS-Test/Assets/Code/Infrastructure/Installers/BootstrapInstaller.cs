using Code.Gameplay.Common.Time;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Context;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Factories.State;
using Code.Infrastructure.Factories.Systems;
using Code.Infrastructure.Factories.Windows;
using Code.Infrastructure.Services.Ads;
using Code.Infrastructure.Services.Analytics;
using Code.Infrastructure.Services.Audio;
using Code.Infrastructure.Services.CleanUp;
using Code.Infrastructure.Services.Curtain;
using Code.Infrastructure.Services.DI;
using Code.Infrastructure.Services.GameCommander;
using Code.Infrastructure.Services.IAP;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.LocalDi;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.Services.Randomizer;
using Code.Infrastructure.Services.SaveLoad;
using Code.Infrastructure.Services.Scene;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.Services.Warmup;
using Code.Infrastructure.Services.Window;
using Code.Infrastructure.States;
using Code.Infrastructure.States.BootStates;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.View.Factory;
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
         BindAnalyticServices();
         BindAssetManagementServices();
         BindAdsServices();
         BindCommonServices();
         BindInputService();
         BindProgressServices();
         BindContexts();
         BindGameCommander();
         BindStateMachine();
         BindUIFactories();
         BindGameplayFactories();
         BindCleanupWarmupServices();
         BindGameStates();
      }

      private void BindInfrastructureServices()
      {
         Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
         Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
         Container.Bind<IRandomService>().To<RandomService>().AsSingle();
         Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
         Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
      }

      public void BindAnalyticServices()
      {
         Container.Bind<IAnalyticService>().To<AppMetricaAnalyticService>().AsSingle();
      }

      private void BindAssetManagementServices()
      {
         Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle(); 
         Container.Bind<IAssetDownloadReporter>().To<AssetDownloadReporter>().AsSingle(); 
         Container.Bind<IAssetDownloadService>().To<AssetDownloadService>().AsSingle(); 
      }

      private void BindAdsServices()
      {
         Container.Bind<IIAPStateService>().To<IAPStateService>().AsSingle();
         Container.Bind<IIAPRewardService>().To<IAPRewardService>().AsSingle();
         Container.Bind<IIAPProvider>().To<IAPProvider>().AsSingle();
         Container.Bind<IIAPService>().To<IAPService>().AsSingle();
         Container.Bind<IIAPAgregator>().To<IAPAgregator>().AsSingle();
         Container.Bind<IAdsService>().To<AdsService>().AsSingle(); 
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
         
         Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
         Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
         Container.Bind<InputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
         Container.Bind<MetaContext>().FromInstance(Contexts.sharedInstance.meta).AsSingle();
      }

      private void BindGameCommander()
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
         Container.Bind<IWindowService>().To<WindowService>().AsSingle();
      }

      private void BindGameplayFactories()
      {
         Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
         Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
      }

      private void BindCleanupWarmupServices()
      {
         Container.Bind<ICleanUpService>().To<CleanUpService>().AsSingle();
         Container.Bind<IWarmupService>().To<WarmupService>().AsSingle();
      }

      private void BindGameStates()
      {
         Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
         Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
         Container.BindInterfacesAndSelfTo<LevelLoadState>().AsSingle();
         Container.BindInterfacesAndSelfTo<LevelLoopState>().AsSingle();
         Container.BindInterfacesAndSelfTo<MetaGameState>().AsSingle();
      }

      public void Initialize()
      {
         Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
      }
   }
}