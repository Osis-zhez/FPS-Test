using System.Collections.Generic;
using Code.UI.Windows.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
   [CreateAssetMenu(fileName = "WindowConfig", menuName = "Static Data/Window Config")]
   public class WindowsStaticData : ScriptableObject
   {
      public List<WindowConfig> WindowConfigs;
   }
}