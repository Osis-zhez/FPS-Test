using System;
using Code.Infrastructure.Services.Window;
using UnityEngine;

namespace Code.UI.Windows.Interfaces
{
   [Serializable]
   public class WindowConfig
   {
      public WindowTypeId Id;
      public GameObject Prefab;
   }
}