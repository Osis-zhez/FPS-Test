using System.Collections.Generic;
using Code.Infrastructure.Factories;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.PersistentProgress;

namespace Code.Infrastructure.Context
{
   public interface IInfrastructureContext : IService
   {
      List<ISavedProgressReader> ProgressReaders { get; }
      List<ISavedProgress> ProgressWriters { get; }
      List<IInitialize> Initializes { get; }
      List<IUpdatable> Updatables { get; }
      List<IGameSystem> GameSystems { get; }
      void Tick();
      void CleanUpMonobehKernel();
      void CleanUp();
      void RegisterReflection<TSystem>(TSystem system);
   }
}