using System;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Services.Scene
{
  public interface ISceneLoader
  {
    void Load(string name, Action onLoaded = null);
    UniTask LoadUnitask(string name);
    string GetActiveScene();
    void LoadInitialScene(Action onLoaded = null);
  }
}