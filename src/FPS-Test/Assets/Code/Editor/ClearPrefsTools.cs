using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
  public class ClearPrefsTools
  {
    [MenuItem("Tools/ClearPrefs")]
    public static void ClearPrefs()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();
    }
  }
}