using System;
using System.Collections.Generic;
using Code.Infrastructure.Services.LocalDi;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.Services.Window;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories.Windows
{
   class WindowFactory : IWindowFactory
   {
      private readonly IStaticDataService _staticData;
      private readonly IInstantiator _instantiator;
      private readonly ILocalDiService _localDiService;
      private RectTransform _uiRoot;
      private Dictionary<WindowTypeId, GameObject> _windowPrefabsById = new Dictionary<WindowTypeId, GameObject>();

      public WindowFactory(IStaticDataService staticData, 
         IInstantiator instantiator,
         ILocalDiService localDiService)
      {
         _staticData = staticData;
         _instantiator = instantiator;
         _localDiService = localDiService;
      }

      public void Initialize()
      {
         // _windowPrefabsById = _staticData.GetWindowsConfig().WindowConfigs.ToDictionary(x => x.Id, x => x.Prefab);
      }

      public void SetUIRoot(RectTransform uiRoot) =>
         _uiRoot = uiRoot;

      // public WindowBase CreateWindow(WindowTypeId windowId) => 
      //    _localDiService.Container.InstantiatePrefabForComponent<WindowBase>(GetWindowPrefab(windowId), _uiRoot);

      private GameObject GetWindowPrefab(WindowTypeId id) =>
         _windowPrefabsById.TryGetValue(id, out GameObject prefab)
            ? prefab
            : throw new Exception($"Prefab config for window {id} was not found");
   }
}