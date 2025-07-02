using UnityEditor;
using UnityEngine;

namespace Code.Editor
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