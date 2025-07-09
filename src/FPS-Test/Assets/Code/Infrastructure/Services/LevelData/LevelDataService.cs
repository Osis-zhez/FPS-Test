using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.StaticData;

namespace Code.Infrastructure.Services.LevelData
{
  public class LevelDataService : ILevelDataService
  {
    private readonly IStaticDataService _staticData;
    private LevelStaticData _meta3DLevelData;
    private LevelStaticData _currentLevelData;
    private LevelStaticData _previousLevelData;
    private LevelStaticData _nextLevelData;

    public LevelDataService(IStaticDataService staticData)
    {
      _staticData = staticData;
    }
    
    public LevelStaticData CurrentLevelData => _currentLevelData;
    public LevelStaticData PreviousLevelData => _previousLevelData;
    public LevelStaticData NextLevelData => _nextLevelData;
    public LevelStaticData Meta3DLevelData => _meta3DLevelData;

    public void SetCurrentLevel(string levelName)
    {
      if(_previousLevelData != null && _currentLevelData != null)
        _previousLevelData = _currentLevelData;
      _currentLevelData = _staticData.GetLevel(levelName);
    }
  }
}