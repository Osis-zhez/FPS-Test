using System;
using System.Collections.Generic;
using Code.Infrastructure.Services.LocalDi;

namespace Code.Infrastructure.Services.GameCommander
{
   public sealed class GameCommanderService : IGameCommanderService
   {
      private readonly ILocalDiService _localDi;
      private readonly IGameCommandFactory _commandFactory;
      private Dictionary<Type, GameCommandBase> _commands = new Dictionary<Type, GameCommandBase>();

      public GameCommanderService(ILocalDiService localDi,
         IGameCommandFactory commandFactory)
      {
         _localDi = localDi;
         _commandFactory = commandFactory;
      }

      public TCommand CreateDisposeCommand<TCommand>() where TCommand : GameCommandBase => 
         _commandFactory.CreateCommand<TCommand>();

      public TCommand GetCommand<TCommand>() where TCommand : GameCommandBase => 
         CheckCommand<TCommand>();

      private TCommand CheckCommand<TCommand>(params object[] args) where TCommand : GameCommandBase
      {
         if (_commands.TryGetValue(typeof(TCommand), out GameCommandBase command))
            return command as TCommand;
         
         _commands[typeof(TCommand)] = _commandFactory.CreateCommand<TCommand>();
         return _commands[typeof(TCommand)] as TCommand;
      }

      public void CleanUp()
      {
         _commands.Clear();
      }
   }

   public interface IGameCommanderService : IService
   {
      void CleanUp();
      TCommand GetCommand<TCommand>() where TCommand : GameCommandBase;
      TCommand CreateDisposeCommand<TCommand>() where TCommand : GameCommandBase;
   }
}