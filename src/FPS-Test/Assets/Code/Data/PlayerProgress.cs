using System;
using Code.Data.InApp;
using Code.Data.ResourcesLootData;
using Code.Infrastructure.Services.StaticData;

namespace Code.Data
{
   [Serializable]
   public class PlayerProgress
   {
      public GameGlobalData GameGlobalData;
      public State HeroState;
      public WorldData WorldData;
      public Stats HeroStats;
      public KillData KillData;
      public ResourceData ResourceData;
      public PurchaseData PurchaseData;
      public AmmunitionData AmmunitionData;

      public PlayerProgress()
      {
      }
      

      public PlayerProgress(IStaticDataService staticDataService, string initialLevel)
      {
         GameGlobalData = new GameGlobalData(staticDataService);
         WorldData = new WorldData(initialLevel);
         HeroState = new State();
         HeroStats = new Stats();
         KillData = new KillData();
         ResourceData = new ResourceData(staticDataService);
         PurchaseData = new PurchaseData();
         AmmunitionData = new AmmunitionData(staticDataService);
      }
   }
}