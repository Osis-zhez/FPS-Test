using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
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