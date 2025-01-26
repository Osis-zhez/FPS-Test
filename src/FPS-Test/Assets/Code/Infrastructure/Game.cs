using Code.Infrastructure.Factories.State;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Audio;
using Code.Infrastructure.Services.Curtain;
using Code.Infrastructure.Services.Scene;
using Code.Infrastructure.States;

namespace Code.Infrastructure
{
   public class Game
   {
      public GameStateMachine StateMachine;

      public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, AudioService audioService)
      {
         StateMachine = InstallRegisterStateMachine(coroutineRunner, curtain, audioService);
      }

      //нужно сделать какой то предэтап бутстрапа, где создается стейтмашина
      public GameStateMachine InstallRegisterStateMachine(ICoroutineRunner coroutineRunner,
         LoadingCurtain curtain,
         AudioService audioService)
      {
         DIService di = DIService.Instance;
         di.Container.Bind<DIService>().FromInstance(DIService.Instance).AsSingle();
         di.Container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
         di.Container.Bind<LoadingCurtain>().FromInstance(curtain).AsSingle();
         di.Container.Bind<AudioService>().FromInstance(audioService).AsSingle();

         di.Container.Bind<IStateFactory>().To<StateFactory>().AsSingle().NonLazy();
         di.Container.Bind<SceneLoader>().To<SceneLoader>().AsSingle().NonLazy();

         GameStateMachine stateMachine = di.Container.Instantiate<GameStateMachine>();
         di.Container.Bind<GameStateMachine>().FromInstance(stateMachine).AsSingle();

         return stateMachine;
      }
   }
}