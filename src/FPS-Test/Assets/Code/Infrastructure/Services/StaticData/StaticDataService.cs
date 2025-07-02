using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Code.Infrastructure.Services.StaticData
{
   public class StaticDataService : IStaticDataService
   {
      private readonly IAssetProvider _assets;
      private Dictionary<string, LevelStaticData> _levels = new();
      private GameGlobalStaticData _gameGlobalStaticData;
      private AudioStaticData _audioStaticData;
      private WindowsStaticData _windowsStaticData;
      private IList<IResourceLocation> _allConfigLocations;

      public StaticDataService(IAssetProvider assets)
      {
         _assets = assets;
      }

      public async UniTask Load()
      {
         foreach (var config in await _assets.LoadConfigs<LevelStaticData>())
            _levels.Add(config.LevelKey, config);
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