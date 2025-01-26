using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.Window;
using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Code.Infrastructure.Services.StaticData
{
   public class StaticDataService : IStaticDataService
   {
      private readonly IAssetProvider _assets;
      private Dictionary<string, LevelStaticData> _levels = new Dictionary<string, LevelStaticData>();

      private GameGlobalStaticData _gameGlobalStaticData = new GameGlobalStaticData();
      private AudioStaticData _audioStaticData = new AudioStaticData();
      private WindowsStaticData _windowsStaticData = new WindowsStaticData();
      private Dictionary<WindowTypeId, GameObject> _windowPrefabsById = new Dictionary<WindowTypeId, GameObject>();

      private IList<IResourceLocation> _allConfigLocations;
      private string _assetLabelName = "config";

      public StaticDataService(IAssetProvider assets)
      {
         _assets = assets;
      }

      public async UniTask Load()
      {
         foreach (var config in await _assets.LoadConfigs<LevelStaticData>())
            _levels.Add(config.LevelKey, config);

         _gameGlobalStaticData = await _assets.LoadSingleConfig<GameGlobalStaticData>();

         _windowsStaticData = await _assets.LoadSingleConfig<WindowsStaticData>();

         _audioStaticData = await _assets.LoadSingleConfig<AudioStaticData>();
      }
      

      public LevelStaticData GetLevel(string sceneKey) =>
         _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
            ? staticData
            : null;

      public WindowsStaticData GetWindowsConfig() =>
         _windowsStaticData;

      public GameGlobalStaticData GetGameGlobalData() =>
         _gameGlobalStaticData;

      public AudioStaticData GetAudioConfig() =>
         _audioStaticData;
   }
}