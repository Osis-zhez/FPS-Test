using System;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.StaticData;

namespace Code.Data
{
  [Serializable]
  public class AmmunitionData
  {
    public int Grenades;
    public int HealhPacks;
    
    public AmmunitionData(IStaticDataService staticDataService)
    {
    }
  }
}