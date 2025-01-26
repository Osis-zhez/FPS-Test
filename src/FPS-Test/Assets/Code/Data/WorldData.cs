using System;

namespace CodeBase.Data
{
  [Serializable]
  public class WorldData
  {
    public PositionOnLevel PositionOnLevel;
    public LootData1 lootData1;

    public WorldData(string initialLevel)
    {
      PositionOnLevel = new PositionOnLevel(initialLevel);
      lootData1 = new LootData1();
    }
  }
}