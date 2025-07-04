﻿using UnityEngine;

namespace Code.Extensions
{
   public static class Extensions
   {
      public static T ToData<T>(this ScriptableObject staticData) where T : ScriptableObject => 
         staticData as T;
   }
}