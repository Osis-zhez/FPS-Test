using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Factories.Windows;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Ads;
using Code.Infrastructure.Services.Audio;
using Code.Infrastructure.Services.Input.Initializer;
using Code.Infrastructure.Services.Scene;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States.BootStates
{
   public class BootstrapState : IState
   {
       private const string Initial = "0. Initial";
      
      private readonly IGameStateMachine _stateMachine;
      private readonly IStaticDataService _staticDataService;
      private readonly IAudioService _audioService;
      private readonly IAdsService _adsService;
      private readonly SceneLoader _sceneLoader;
      private readonly DIService _di;
      private readonly IAssetProvider _assets;
      private readonly IInputInitializer _inputInitializer;
      private IWindowFactory _windowFactory;

      public BootstrapState(IGameStateMachine stateMachine,
         IStaticDataService staticDataService,
         IAudioService audioService,
         IAdsService adsService,
         IWindowFactory windowFactory,
         SceneLoader sceneLoader, 
         DIService di,
         IAssetProvider assets,
         IInputInitializer inputInitializer)
      {
         _windowFactory = windowFactory;
         Debug.Log("BootStrapConstruct");
         _stateMachine = stateMachine;
         _staticDataService = staticDataService;
         _audioService = audioService;
         _adsService = adsService;
         _sceneLoader = sceneLoader;
         _di = di;
         _assets = assets;
         _inputInitializer = inputInitializer;
      }

      public async void Enter()
      {
         Debug.Log("BootEnter");
         await RegisterServices();
         _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
      }

      public void Exit()
      {
      }

      private async UniTask RegisterServices()
      {
         await _assets.Initialize();
         await _staticDataService.Load();
         InputInitialize();
         await _audioService.Initialize();
         _windowFactory.Initialize();
         _adsService.Initialize();
      }

      private void InputInitialize()
      {
         GameGlobalStaticData gameData = _staticDataService.GetGameGlobalData();
         _inputInitializer.Initialize(gameData.InputMode);
      }

      private void EnterLoadLevel() =>
         _stateMachine.Enter<LoadProgressState>();
   }
}