using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    UniTask Load();
    LevelStaticData GetLevel(string sceneKey);
    public GameGlobalStaticData GetGameGlobalData();
    AudioStaticData GetAudioConfig();
    WindowsStaticData GetWindowsConfig();
  }
}