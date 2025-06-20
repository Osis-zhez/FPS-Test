using Code.Data;

namespace Code.Infrastructure.Services.PersistentProgress
{
  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress);
  }
}