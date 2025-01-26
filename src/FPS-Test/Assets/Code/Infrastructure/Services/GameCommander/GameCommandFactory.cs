using Code.Infrastructure.Contexts;
using Code.Infrastructure.Services.LocalDi;

namespace Code.Infrastructure.Services.GameCommander
{
   public class GameCommandFactory : IGameCommandFactory
   {
      private readonly InfrastructureContext _infrastructureContext;
      private readonly ILocalDiService _localDi;

      public GameCommandFactory(InfrastructureContext infrastructureContext,
         ILocalDiService localDi)
      {
         _infrastructureContext = infrastructureContext;
         _localDi = localDi;
      }

      public TCommand CreateCommand<TCommand>(params object[] args) where TCommand : GameCommandBase
      {
         TCommand command = _localDi.Container.Instantiate<TCommand>();
         _infrastructureContext.RegisterReflection(command);
         return command;
      }
   }
}