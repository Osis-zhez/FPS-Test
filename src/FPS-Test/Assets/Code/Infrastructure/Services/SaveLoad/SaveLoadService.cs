using Code.Data;
using Code.Infrastructure.Context;
using Code.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Code.Infrastructure.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";
    
    private readonly IPersistentProgressService _progressService;
    private readonly InfrastructureContext _infrastructureContext;

    public SaveLoadService(IPersistentProgressService progressService,
      InfrastructureContext infrastructureContext)
    {
      _progressService = progressService;
      _infrastructureContext = infrastructureContext;
    }

    public void SaveProgress()
    {
      foreach (ISavedProgress progressWriter in _infrastructureContext.ProgressWriters)
        progressWriter.UpdateProgress(_progressService.Progress);
      
      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
      Debug.Log(PlayerPrefs.GetString(ProgressKey));
    }

    public PlayerProgress LoadProgress()
    {
      Debug.Log(PlayerPrefs.GetString(ProgressKey));
      return PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
    }
  }
}