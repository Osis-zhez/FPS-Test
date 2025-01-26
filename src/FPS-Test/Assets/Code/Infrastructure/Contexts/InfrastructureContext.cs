using System;
using System.Collections.Generic;
using Code.Infrastructure.Factories;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Services.PersistentProgress;

namespace Code.Infrastructure.Contexts
{
   public class InfrastructureContext : IInfrastructureContext
   {
      public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
      public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
      public List<IGameSystem> GameSystems { get; } = new List<IGameSystem>();
      public List<IInitialize> Initializes { get; } = new List<IInitialize>();
      public List<IDisposable> Disposables{ get; } = new List<IDisposable>();

      public List<IGameStart> Starters { get; } = new List<IGameStart>();
      public List<IUpdatable> Updatables { get; } = new List<IUpdatable>();

      public void CleanUp()
      {
         ProgressReaders.Clear();
         ProgressWriters.Clear();
         GameSystems.Clear();
         Initializes.Clear();
         Disposables.Clear();
      }

      public void CleanUpMonobehKernel()
      {
         Updatables.Clear();
         Starters.Clear();
      }

      public void Start()
      {
         if (Starters.Count > 0)
            foreach (IGameStart starter in Starters)
               starter.GameStart();
      }

      public void Tick()
      {
         if (Updatables.Count > 0)
            foreach (IUpdatable updatable in Updatables)
               updatable.Tick();
      }

      public void RegisterReflection<TSystem>(TSystem system)
      {
         if (system is IInitialize)
            Initializes.Add(system as IInitialize);
         
         if (system is IGameStart)
            Starters.Add(system as IGameStart);

         if (system is IUpdatable)
            Updatables.Add(system as IUpdatable);
         
         if (system is IGameSystem)
            GameSystems.Add(system as IGameSystem);
         
         if (system is IDisposable)
            Disposables.Add(system as IDisposable);
      }
   }
}