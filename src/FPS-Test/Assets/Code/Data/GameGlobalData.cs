﻿using System;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.StaticData;

namespace Code.Data
{
   [Serializable]
   public class GameGlobalData
   {
      public bool IsTutorialCompelete;

      public GameGlobalData()
      {
      }

      public GameGlobalData(IStaticDataService staticData)
      {
         
      }
   }
}