using Code.Gameplay.Features.Battle;
using Code.Infrastructure.Contexts;
using Code.Infrastructure.Factories;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Factories.Systems;

namespace Code.Infrastructure.States.GameStates
{
  public class GameLoopState : IState, IUpdatable, ILateUpdatable
  {
    private readonly ISystemFactory _systems;
    private BattleFeature _battleFeature;
    private bool _isExitState;

    public GameLoopState(ISystemFactory systems)
    {
      _systems = systems;
    }

    public void Enter()
    {
      _isExitState = false;

      _battleFeature = _systems.Create<BattleFeature>();
      _battleFeature.Initialize();
    }

    void IUpdatable.Tick()
    {
      _battleFeature.Execute();
      _battleFeature.Cleanup();
    }

    void ILateUpdatable.LateTick()
    {
      if (_isExitState)
        _battleFeature = null;
    }

    public void Exit()
    {
      _battleFeature.DeactivateReactiveSystems();
      _battleFeature.ClearReactiveSystems();

      DestructEntities();

      _battleFeature.Cleanup();
      _battleFeature.TearDown();

      _isExitState = true;
    }

    private void DestructEntities()
    {
      
    }
  }
}