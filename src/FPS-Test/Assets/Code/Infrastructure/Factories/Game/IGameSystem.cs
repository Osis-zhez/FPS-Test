using System;

namespace Code.Infrastructure.Factories.Game
{
   public interface IGameSystem : IDisposable
   {
      void IDisposable.Dispose(){}
   }
}