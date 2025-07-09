using Code.Common.Destruct;
using Code.Infrastructure.Factories.Systems;
using Code.Infrastructure.View;

namespace Code.Gameplay.LevelMode.Features.Battle
{
    public class BattleFeature : Feature
    {
        public BattleFeature(ISystemFactory systems)
        {
            Add(systems.Create<BindViewFeature>());
            
            Add(systems.Create<ProcessDestructedFeature>());
        }
    }
}
