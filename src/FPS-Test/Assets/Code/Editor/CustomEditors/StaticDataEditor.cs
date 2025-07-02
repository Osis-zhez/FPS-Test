using Code.Infrastructure.StaticData;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.CustomEditors
{
   public class StaticDataEditor : OdinMenuEditorWindow
   {
      [MenuItem("Tools/Static Data Editor")]
      private static void OpenWindow() 
      {
         GetWindow<StaticDataEditor>().Show();
      }

      protected override OdinMenuTree BuildMenuTree()
      {
         var tree = new OdinMenuTree();
         tree.Selection.SupportsMultiSelect = false;

         // tree.Add("Create New", new CreateNewEnemyData());
         tree.AddAllAssetsAtPath("Level Data", "Assets/Resources_moved/StaticData/Levels", typeof(LevelStaticData));
         tree.AddAllAssetsAtPath("Weapon Data", "Assets/Resources_moved/StaticData/Weapons", typeof(WeaponStaticData));
         tree.AddAllAssetsAtPath("Enemy Data", "Assets/Resources_moved/StaticData/Enemy", typeof(EnemyStaticData));
         return tree;
      }

      public class CreateNewEnemyData
      {
         public CreateNewEnemyData()
         {
            enemyData = ScriptableObject.CreateInstance<EnemyStaticData>();
            enemyData.EnemyName = "New Enemy Data";
         }

         [InlineEditor(Expanded = true)]
         public EnemyStaticData enemyData;

         [Button("Add New Enemy SO")]
         private void CreateNewData()
         {
            AssetDatabase.CreateAsset(enemyData, "Assets/Scripts/" + enemyData.EnemyName + ".asset");
            AssetDatabase.SaveAssets();
         }
      }

      protected override void OnBeginDrawEditors()
      {
         OdinMenuTreeSelection selected = this.MenuTree.Selection;

         SirenixEditorGUI.BeginHorizontalToolbar();
         {
            GUILayout.FlexibleSpace();

            if (SirenixEditorGUI.ToolbarButton("Delete Current"))
            {
               EnemyStaticData asset = selected.SelectedValue as EnemyStaticData;
               string path = AssetDatabase.GetAssetPath(asset);
               AssetDatabase.DeleteAsset(path);
               AssetDatabase.SaveAssets();
            }

         }
         SirenixEditorGUI.EndHorizontalToolbar(); 
      }
   }
}