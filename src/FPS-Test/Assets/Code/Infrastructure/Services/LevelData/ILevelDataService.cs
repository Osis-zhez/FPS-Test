using Code.Infrastructure.StaticData;

namespace Code.Infrastructure.Services.LevelData
{
  public interface ILevelDataService
  {
    void SetCurrentLevel(string levelName);
    LevelStaticData CurrentLevelData { get; }
    LevelStaticData PreviousLevelData { get; }
    LevelStaticData NextLevelData { get; }
    LevelStaticData Meta3DLevelData { get; }
  }
}