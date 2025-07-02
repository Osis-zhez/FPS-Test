using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
   public class BundleCacheCleaner
   {
      [MenuItem("Tools/Clear addressable bundles cache")]
      private static void ClearBundleCache()
      {
         Caching.ClearCache();
      }
   }
}