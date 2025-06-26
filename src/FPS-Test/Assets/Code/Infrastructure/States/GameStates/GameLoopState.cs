using Code.Gameplay.Features.Battle;
using Code.Infrastructure.Contexts;
using Code.Infrastructure.Factories;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Factories.Systems;

namespace Code.Infrastructure.States.GameStates
{
  public class GameLoopState : IState, IUpdatable
  {
    private readonly GameStateMachine _stateMachine;
    private readonly InfrastructureContext _infrastructureContext;
    private readonly IGameFactory _gameFactory;
    private readonly ISystemFactory _systems;
    private BattleFeature _battleFeature;

    public GameLoopState(GameStateMachine stateMachine,
      InfrastructureContext infrastructureContext,
      IGameFactory gameFactory,
      ISystemFactory systems)
    {
      _stateMachine = stateMachine;
      _infrastructureContext = infrastructureContext;
      _gameFactory = gameFactory;
      _systems = systems;
    }

    public void Enter()
    {

      _battleFeature = _systems.Create<BattleFeature>();
      _battleFeature.Initialize();
    }

    public void Tick()
    {
      foreach (IUpdatable updatable in _infrastructureContext.Updatables) 
        updatable.Tick();
      
      _battleFeature.Execute();
      _battleFeature.Cleanup();
    }

    public void Exit()
    {
    }
  }
}