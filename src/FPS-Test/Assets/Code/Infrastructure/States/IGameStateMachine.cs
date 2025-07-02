namespace Code.Infrastructure.States
{
   public interface IGameStateMachine
   {
      void Enter<TState>() where TState : class, IState;
      TState ChangeState<TState>() where TState : class, IExitableState;
      void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
   }
}