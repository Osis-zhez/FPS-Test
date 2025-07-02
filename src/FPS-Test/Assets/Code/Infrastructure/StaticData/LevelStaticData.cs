using System;
using Code.Infrastructure.Services.Scene;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Code.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
  public class LevelStaticData : SerializedScriptableObject
  {
    [Title("Level Config", titleAlignment: TitleAlignments.Centered)]
    [GUIColor(0f, 0.95f, 0f, 1f)]
    public string LevelKey;
    [GUIColor(0.3f, 0.95f, 0.8f, 1f)]
    public string NextLevelKey;
    [GUIColor(0.3f, 0.95f, 0.8f, 1f)]
    public SceneId SceneId;
    
    [GUIColor(1f, 1f, 0, 1f)]
    public int MinGoldReward;
    [GUIColor(1f, 1f, 0, 1f)]
    public int MaxGoldReward;

    [FoldoutGroup("Player Info")]
    [InfoBox("Для создания игрока используется позиция и поворот префаба PlayerInitialPoint. Поэтому этот префаб обязательно должен быть на сцене. Иначе ошибка.")]
    public string Path = "Resources_moved/Gameplay/Initial Points";

    [Header("Zombie Settings")]
    [FoldoutGroup("Zombie Settings")]
    public AssetReferenceT<TextAsset> WavesData;

    [FoldoutGroup("Zombie Settings")]
    public float ZombieMinScaleMultiplier = 0.8f;
    [FoldoutGroup("Zombie Settings")]
    public float ZombieMaxScaleMultiplier = 1.1f;
    [FoldoutGroup("Zombie Settings")]
    public float ZombieHealthMultiplier = 1;
    [FoldoutGroup("Zombie Settings")]
    public float ZombieDamageMultiplier = 1;
    [FoldoutGroup("Zombie Settings")]
    public float ZombieSpeedMultiplier = 1;


    [Button]
    public void CollectLevelProperties()
    {
    }
    

    public string GetSceneId()
    {
      if (SceneId == SceneId.Random)
      {
        int sceneIndex = Random.Range(3, 7);
        return ((SceneId) sceneIndex).ToString();
      }
      
      return SceneId.ToString();
    }
  
  }
  
  [Serializable]
  [TypeInfoBox("Для создания игрока используется позиция и поворот префаба PlayerInitialPoint. Поэтому этот префаб обязательно должен быть на сцене. Иначе ошибка.")]
  public class PlayerInfoDiscription {}
}