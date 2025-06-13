using System;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.StaticData;
using UnityEngine;

namespace Code.Data.ResourcesLootData
{
  [Serializable]
  public class ResourceData
  {
    public int GoldAmount;
    public int SparePartsAmount;

    public ResourceData()
    {
    }

    public ResourceData(IStaticDataService staticData)
    {
      Debug.Log("Create ResourceData");
      GameGlobalStaticData _globalStaticData = staticData.GetGameGlobalData();

      GoldAmount = _globalStaticData.Gold;
      SparePartsAmount = 0;
    }
  }
}