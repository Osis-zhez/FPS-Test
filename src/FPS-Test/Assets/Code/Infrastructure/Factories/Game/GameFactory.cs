using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Contexts;
using Code.Infrastructure.Services.LocalDi;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.Services.Randomizer;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories.Game
{
   public class GameFactory : IGameFactory
   {
      public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
      public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
      public List<IInitialize> Starters { get; } = new List<IInitialize>();

      private readonly ILocalDiService _localDi;
      private readonly IAssetProvider _assets;
      private readonly IStaticDataService _staticData;
      private readonly IRandomService _randomService;
      private readonly InfrastructureContext _infrastructureContext;
      private readonly IInstantiator _instantiator;
      private LevelStaticData _levelData;

      public GameFactory(ILocalDiService localDi,
         IAssetProvider assets,
         IStaticDataService staticData,
         IRandomService randomService,
         InfrastructureContext infrastructureContext,
         IInstantiator instantiator)
      {
         _localDi = localDi;
         _assets = assets;
         _staticData = staticData;
         _randomService = randomService;
         _infrastructureContext = infrastructureContext;
         _instantiator = instantiator;
      }

      public void WarmUp()
      {
         _localDi.WarmUp();
      }

      public async UniTask Cleanup()
      {
      }

      public async UniTask<TService> CreateGameSystem<TService>(string assetAddress) where TService : class, IGameSystem
      {
         GameObject prefab = await _assets.Load<GameObject>(assetAddress);
         
         GameObject systemObject = Object.Instantiate(prefab); //здесь нужно закрепить сервисы к парент объекту
         Debug.Log(systemObject);
         RegisterReflectionForGameObject(systemObject);

         TService system = systemObject.GetComponent<TService>();
         _localDi.Container.BindInterfacesAndSelfTo<TService>().FromInstance(system).AsSingle();
         _localDi.Container.InjectGameObject(systemObject);

         return system;
      }

      public TSystem CreateGameSystem<TSystem>() where TSystem : class, IGameSystem
      {
         _localDi.Container.BindInterfacesAndSelfTo<TSystem>().AsSingle().NonLazy();
         TSystem system = _localDi.Container.Resolve<TSystem>();

         RegisterReflectionForSystem(system);
         
         return system;
      }

      public LevelStaticData LoadLevelData(string levelDataName)
      {
         _levelData = _staticData.GetLevel(levelDataName);
         _localDi.Container.Bind<LevelStaticData>().FromInstance(_levelData).AsSingle();
         return _levelData;
      }

      public async UniTask<GameObject> InstantiateRegistered(string prefabPath, Vector3 at)
      {
         GameObject gameObject = await _assets.Instantiate(path: prefabPath);
         RegisterReflectionForGameObject(gameObject);

         return gameObject;
      }

      public GameObject InstantiateRegistered(GameObject prefab)
      {
         GameObject gameObject = Object.Instantiate(prefab);
         RegisterReflectionForGameObject(gameObject);
         
         return gameObject;
      }
      
      private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
      {
         GameObject gameObject = await _assets.Instantiate(path: prefabPath);
         RegisterReflectionForGameObject(gameObject);

         return gameObject;
      }

      private void RegisterReflectionForGameObject(GameObject gameObject)
      {
         RegisterProgressWatchers(gameObject);
         
         foreach (IInitialize initialize in gameObject.GetComponentsInChildren<IInitialize>())
            _infrastructureContext.Initializes.Add(initialize);
      }

      private void RegisterReflectionForSystem<TSystem>(TSystem system)
      {
         if (system is ISavedProgress savedProgressSystem)
            Register(savedProgressSystem);

         _infrastructureContext.RegisterReflection(system);
      }

      private void Register(ISavedProgressReader progressReader)
      {
         if (progressReader is ISavedProgress progressWriter)
            _infrastructureContext.ProgressWriters.Add(progressWriter);

         _infrastructureContext.ProgressReaders.Add(progressReader);
      }

      private async UniTask RegisterProgressWatchers(GameObject gameObject)
      {
         foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            Register(progressReader);
      }
   }
}