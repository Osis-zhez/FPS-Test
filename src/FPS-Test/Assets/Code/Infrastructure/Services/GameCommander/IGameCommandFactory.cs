namespace Code.Infrastructure.Services.GameCommander
{
   public interface IGameCommandFactory
   {
      TCommand CreateCommand<TCommand>(params object[] args) where TCommand : GameCommandBase;
   }
}