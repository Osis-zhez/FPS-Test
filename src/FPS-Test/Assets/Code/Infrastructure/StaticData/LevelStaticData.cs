using System;
using Code.Infrastructure.Services.Scene;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
    public string WinScreenLevelKey;
    [GUIColor(0.3f, 0.95f, 0.8f, 1f)]
    public SceneTypeId SceneId;
    
    [GUIColor(1f, 1f, 0, 1f)]
    public int MinGoldReward;
    [GUIColor(1f, 1f, 0, 1f)]
    public int MaxGoldReward;

    [FoldoutGroup("Player Info")]
    [InfoBox("Для создания игрока используется позиция и поворот префаба PlayerInitialPoint. Поэтому этот префаб обязательно должен быть на сцене. Иначе ошибка.")]
    public string Path = "Resources_moved/Gameplay/Initial Points";
    
    [FoldoutGroup("Survivor Bus")]
    [InfoBox("Для создания автобуса используется позиция и поворот префаба BusInitialPoint. Поэтому этот префаб обязательно должен быть на сцене. Иначе ошибка. Также создание автобуса можно отключить")]
    public bool IsBusInstantiate;


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

    public Transform GetPlayerInitialPoint() => 
      GameObject.FindGameObjectWithTag("PlayerInitialPoint").transform;

    public Transform GetBusInitialPoint() => 
      GameObject.FindGameObjectWithTag("BusInitialPoint").transform;

    public string GetSceneId()
    {
      if (SceneId == SceneTypeId.Random)
      {
        int sceneIndex = Random.Range(3, 7);
        return ((SceneTypeId) sceneIndex).ToString();
      }
      
      return SceneId.ToString();
    }
    
    [TabGroup("tab2", "General", SdfIconType.ImageAlt, TextColor = "green")]
    public string playerName2;
    [TabGroup("tab2", "General")]
    public int playerLevel2;
    [TabGroup("tab2", "General")]
    public string playerClass2;
    
    [TabGroup("tab2", "Stats", SdfIconType.BarChartLineFill, TextColor = "blue")]
    public int strength2;
    [TabGroup("tab2", "Stats")]
    public int dexterity2;
    [TabGroup("tab2", "Stats")]
    public int intelligence2;
    
    [TabGroup("tab2", "Quests", SdfIconType.Question, TextColor = "@questColor", TabName = "")]
    public bool hasMainQuest2;

    [TabGroup("tab2", "Quests")]
    public Color questColor = new Color(1, 0.5f, 0);
  }
  
  [Serializable]
  [TypeInfoBox("Для создания игрока используется позиция и поворот префаба PlayerInitialPoint. Поэтому этот префаб обязательно должен быть на сцене. Иначе ошибка.")]
  public class PlayerInfoDiscription {}
  
  [Serializable]
  [TypeInfoBox("Для создания автобуса используется позиция и поворот префаба BusInitialPoint. Поэтому этот префаб обязательно должен быть на сцене. Иначе ошибка. Также создание автобуса можно отключить")]
  public class BusInfoDiscription {}
}