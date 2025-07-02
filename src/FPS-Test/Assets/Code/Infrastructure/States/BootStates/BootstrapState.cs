using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.Audio;
using Code.Infrastructure.Services.Input.Initializer;
using Code.Infrastructure.Services.StaticData;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States.BootStates
{
   public class BootstrapState : IState
   {
      private readonly IGameStateMachine _stateMachine;
      private readonly IStaticDataService _staticDataService;
      private readonly IAudioService _audioService;
      private readonly IAssetProvider _assets;
      private readonly IInputInitializer _inputInitializer;

      public BootstrapState(IGameStateMachine stateMachine,
         IStaticDataService staticDataService,
         IAudioService audioService,
         IAssetProvider assets)
      {
         _stateMachine = stateMachine;
         _staticDataService = staticDataService;
         _audioService = audioService;
         _assets = assets;
      }

      public async void Enter()
      {
         await RegisterServices();
         _stateMachine.Enter<LoadProgressState>();
      }

      public void Exit()
      {
      }

      private async UniTask RegisterServices()
      {
         await _assets.Initialize();
         await _staticDataService.Load();
         // await _audioService.Initialize();
      }
   }
}