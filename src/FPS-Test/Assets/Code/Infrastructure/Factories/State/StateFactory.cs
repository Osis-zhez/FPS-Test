using Code.Infrastructure.Services;
using Code.Infrastructure.Services.DI;
using Code.Infrastructure.States;

namespace Code.Infrastructure.Factories.State
{
   public class StateFactory : IStateFactory
   {
      private readonly DIService _di;

      public StateFactory(DIService di)
      {
         _di = di;
      }

      public TState GetState<TState>() where TState : class, IExitableState
      {
         return _di.Container.Resolve<TState>();
      }
      
      public TState CreateState<TState>() where TState : IExitableState
      {
         return _di.Container.Instantiate<TState>();
      }
      
      public TState CreateState<TState>(params object[] args) where TState : IExitableState
      {
         return _di.Container.Instantiate<TState>(args);
      }
   }
}