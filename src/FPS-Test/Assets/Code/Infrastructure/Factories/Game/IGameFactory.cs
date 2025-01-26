using System.Collections.Generic;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Factories.Game
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    List<IInitialize> Starters { get; }
    UniTask Cleanup();
    LevelStaticData LoadLevelData(string levelDataName);
    void WarmUp();
    TSystem CreateGameSystem<TSystem>() where TSystem : class, IGameSystem;
    UniTask<GameObject> InstantiateRegistered(string prefabPath, Vector3 at);
  }
}