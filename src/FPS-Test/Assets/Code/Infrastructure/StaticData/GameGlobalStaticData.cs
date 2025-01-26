using System;
using System.Collections.Generic;
using Code.Infrastructure.Services.Input.Initializer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "GameGlobalData", menuName = "Static Data/GameGlobal")]
  public class GameGlobalStaticData : ScriptableObject, ISerializationCallbackReceiver
  {
    [Header("New Game Start Settings")] 
    public InputMode InputMode;
    public bool IsTutorialComplete;
    public int HealthPacks;
    public int Granades;
    public int RifleAmmo;
    public int ShotgunAmmo;
    public int Gold;
    public List<BuyAmmunitionInfo> BuyAmmunitionInfoList;
    
    [FoldoutGroup("PC Controller InfoBox")]
    [TextArea(minLines:4, maxLines: 10)]
    public string Controller;

    [ColorFoldoutGroup("Enemy Spawn Manager", 0.9f, 0, 0)]
    public float RandomXPositionForSpawner;
    [ColorFoldoutGroup("Enemy Spawn Manager")]
    public float RandomZPositionForSpawner;
    [ColorFoldoutGroup("Enemy Spawn Manager")]
    public int EndAllWaveTimer;
    [ColorFoldoutGroup("Enemy Spawn Manager")]
    public float NextWaveBecameTimer;
    [ColorFoldoutGroup("Enemy Spawn Manager")]
    public float DelayBetweenZombieSpawn;
    
    [ColorFoldoutGroup("Notification Panel", 0f, 0.9f, 0)]
    public Color GoldNotificationColor;
    [ColorFoldoutGroup("Notification Panel")]
    public Color SparePartsColor;
    [ColorFoldoutGroup("Notification Panel")]
    public Color GranadeColor;
    [ColorFoldoutGroup("Notification Panel")]
    public Color HealthPackColor;

    public void OnBeforeSerialize()
    {
      
    }

    public void OnAfterDeserialize()
    {
      // foreach (BuyAmmoInfo buyInfo in BuyAmmoInfoList)
      //   AmmoBuyInfoDictionary.Add(buyInfo.AmmoId, buyInfo);
    }
  }

  [Serializable]
  public class BuyAmmoInfo
  {
    public int Cost;
    public int AddPerBuy;
  }

  [Serializable]
  public class BuyAmmunitionInfo
  {
    public int Cost;
    public int AddPerBuy;
  }
}