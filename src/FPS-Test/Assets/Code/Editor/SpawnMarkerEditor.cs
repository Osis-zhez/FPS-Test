using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
  public class SpawnMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo()
    {
      Gizmos.color = Color.red;
    }
  }
}
