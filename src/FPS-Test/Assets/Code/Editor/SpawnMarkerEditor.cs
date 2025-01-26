using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
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
