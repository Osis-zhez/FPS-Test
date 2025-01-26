using System;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.StaticData;

namespace CodeBase.Data
{
  [Serializable]
  public class AmmunitionData
  {
    public int Grenades;
    public int HealhPacks;
    
    public AmmunitionData(IStaticDataService staticDataService)
    {
      if (staticDataService == null) return;
      
      GameGlobalStaticData _globalStaticData = staticDataService.GetGameGlobalData();

      Grenades = _globalStaticData.Granades;
      HealhPacks = _globalStaticData.HealthPacks;
    }
  }
}