using Code.Infrastructure.Services;
using Code.Infrastructure.States;

namespace Code.Infrastructure.Factories.State
{
   public interface IStateFactory : IService
   {
      TState CreateState<TState>() where TState : IExitableState;
      TState CreateState<TState>(params object[] args) where TState : IExitableState;
      TState GetState<TState>() where TState : class, IExitableState;
   }
}