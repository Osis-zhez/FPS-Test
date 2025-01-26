using System;
using System.Collections;
using Code.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Code.Infrastructure.Services.Scene
{
  public class SceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IAssetProvider _assets;

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
    }

    public void Load(string name, Action onLoaded = null) =>
      _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    
    public async UniTask LoadUnitask(string name)
    {
      await LoadSceneUnitask(name);
    }

    private async UniTask LoadSceneUnitask(string nextScene)
    {
      AsyncOperationHandle<SceneInstance> waitNextScene = Addressables.LoadSceneAsync(nextScene);
      await waitNextScene.ToUniTask();
    }

    private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
      Debug.Log(nextScene);
      
      AsyncOperationHandle<SceneInstance> waitNextScene = Addressables.LoadSceneAsync(nextScene);
      
      while (!waitNextScene.IsDone)
        yield return null;

      onLoaded?.Invoke();

      yield return onLoaded;
    }
  }
}