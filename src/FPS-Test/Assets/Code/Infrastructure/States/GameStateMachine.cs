using System;
using System.Collections.Generic;
using Code.Infrastructure.Factories;
using Code.Infrastructure.Factories.State;
using Zenject;

namespace Code.Infrastructure.States
{
   public class GameStateMachine : IGameStateMachine, ITickable, ILateTickable
   {
      private readonly IStateFactory _stateFactory;
      private Dictionary<Type, IExitableState> _states;
      private IExitableState _activeState;

      public GameStateMachine(IStateFactory stateFactory)
      {
         _stateFactory = stateFactory;
      }
      
      public void Tick()
      {
         if (_activeState is IUpdatable updatableState)
            updatableState.Tick();
      }
      
      void ILateTickable.LateTick()
      {
         if (_activeState is ILateUpdatable updatableState)
            updatableState.LateTick();
      }

      public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
      {
         TState state = ChangeState<TState>();
         state.Enter(payload);
      }
      
      public void Enter<TState>() where TState : class, IState
      {
         IState state = ChangeState<TState>();
         state.Enter();
      }


      public TState ChangeState<TState>() where TState : class, IExitableState
      {
         _activeState?.Exit();

         TState state = _stateFactory.GetState<TState>();
         _activeState = state;

         return state;
      }

      private TState GetState<TState>() where TState : class, IExitableState =>
         _states[typeof(TState)] as TState;

      
   }
}