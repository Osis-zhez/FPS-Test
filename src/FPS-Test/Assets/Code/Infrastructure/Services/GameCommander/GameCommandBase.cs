using System;

namespace Code.Infrastructure.Services.GameCommander
{
   public abstract class GameCommandBase : IDisposable
   {
      public abstract void Execute();

      public virtual void Dispose()
      {
      }
   }
}