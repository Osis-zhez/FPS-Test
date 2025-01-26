using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Code.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    private readonly Dictionary<string, AsyncOperationHandle> _completedCashe = new Dictionary<string, AsyncOperationHandle>();
    private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();
    private IList<IResourceLocation> _allConfigLocations;
    private string _assetLabelName;

    public async UniTask Initialize()
    {
      await Addressables.InitializeAsync();
    }
    
    public async UniTask<T> LoadWithoutRegistration<T>(AssetReference assetReference) where T : class => 
      await Addressables.LoadAssetAsync<T>(assetReference);

    public async UniTask<T> Load<T>(AssetReference assetReference) where T : class
    {
      if (_completedCashe.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
        return completedHandle.Result as T;

      return await RunWithCacheOnComplete(
        Addressables.LoadAssetAsync<T>(assetReference), 
        cacheKey: assetReference.AssetGUID);
    }

    public async UniTask<T> Load<T>(string address) where T : class
    {
      if (_completedCashe.TryGetValue(address, out AsyncOperationHandle completedHandle))
        return completedHandle.Result as T;
      
      AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

      return await RunWithCacheOnComplete(
        Addressables.LoadAssetAsync<T>(address), 
        cacheKey: address);
    }

    public UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion angle) => 
      Addressables.InstantiateAsync(address, at, angle).ToUniTask();
    
    public UniTask<GameObject> Instantiate(string address) => 
      Addressables.InstantiateAsync(address).ToUniTask();

    public void Cleanup()
    {
      foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
      foreach (AsyncOperationHandle handle in resourceHandles)
        Addressables.Release(handle);
      
      _completedCashe.Clear();
      _handles.Clear();
    }

    private async UniTask<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
    {
      handle.Completed += completeHandle =>
      {
        _completedCashe[cacheKey] = completeHandle;
      };

      AddHandle<T>(cacheKey, handle);

      return await handle.ToUniTask();
    }

    private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
    {
      if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
      {
        resourceHandles = new List<AsyncOperationHandle>();
        _handles[key] = resourceHandles;
      }

      resourceHandles.Add(handle);
    }
    
    public async UniTask<TConfig[]> LoadConfigs<TConfig>() where TConfig : ScriptableObject
    {
      _assetLabelName = "config";
      await LoadScopeConfigsAsync<TConfig>(_assetLabelName, typeof(TConfig));
      List<UniTask<TConfig>> uniTasks = new List<UniTask<TConfig>>();

      foreach (IResourceLocation location in _allConfigLocations)
      {
        AsyncOperationHandle<TConfig> handle = Addressables.LoadAssetAsync<TConfig>(location);
        uniTasks.Add(handle.ToUniTask());
        // Addressables.Release(handle);
      }

      TConfig[] loadedConfigs = await UniTask.WhenAll(uniTasks);

      return loadedConfigs;
    }

    public async UniTask<TConfig> LoadSingleConfig<TConfig>() where TConfig : ScriptableObject
    {
      _assetLabelName = "config";
      await LoadScopeConfigsAsync<TConfig>(_assetLabelName, typeof(TConfig));
      List<UniTask<TConfig>> uniTasks = new List<UniTask<TConfig>>();

      foreach (IResourceLocation location in _allConfigLocations)
      {
        AsyncOperationHandle<TConfig> handle = Addressables.LoadAssetAsync<TConfig>(location);
        uniTasks.Add(handle.ToUniTask());
      }

      TConfig[] loadedConfigs = await UniTask.WhenAll(uniTasks);

      return loadedConfigs[0];
    }

    public async UniTask LoadScopeConfigsAsync<T>(string assetslabel, Type type) =>
      _allConfigLocations = await Addressables.LoadResourceLocationsAsync(assetslabel, type).ToUniTask();
    
    public async UniTask LoadScopeConfigsAsync(string assetslabel) =>
      _allConfigLocations = await Addressables.LoadResourceLocationsAsync(assetslabel).ToUniTask();
    
    public GameObject LoadAsset(string path)
    {
      return Resources.Load<GameObject>(path);
    }

    public T LoadAsset<T>(string path) where T : Component
    {
      return Resources.Load<T>(path);
    }
  }
}