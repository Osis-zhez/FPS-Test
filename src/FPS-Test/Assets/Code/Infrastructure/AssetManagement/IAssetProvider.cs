using System;
using Code.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.AssetManagement
{
  public interface IAssetProvider : IService
  {
    UniTask<GameObject> Instantiate(string path, Vector3 at, Quaternion angle);
    UniTask<GameObject> Instantiate(string path);
    UniTask<T> Load<T>(AssetReference assetReference) where T : class;
    UniTask<T> Load<T>(string address) where T : class;
    UniTask Initialize();
    void Cleanup();
    UniTask<T> LoadWithoutRegistration<T>(AssetReference assetReference) where T : class;
    UniTask LoadScopeConfigsAsync<T>(string assetslabel, Type type);
    UniTask<TConfig> LoadSingleConfig<TConfig>() where TConfig : ScriptableObject;
    UniTask<TConfig[]> LoadConfigs<TConfig>() where TConfig : ScriptableObject;
    UniTask LoadScopeConfigsAsync(string assetslabel);
    GameObject LoadAsset(string path);
    T LoadAsset<T>(string path) where T : Component;
  }
}