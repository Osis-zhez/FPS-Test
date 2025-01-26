using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor.Tools
{
   public class RemoveMissingComponents
   {
      private static ILogger Logger => Debug.unityLogger;

      [MenuItem("Tools/Remove Missing Components")]
      public static void FindMissingComponents()
      {
         Logger.Log("Hello From Validator");

         var scene = SceneManager.GetActiveScene();

         var gameObjectQueue = new Queue<GameObject>(scene.GetRootGameObjects());

         while (gameObjectQueue.Count > 0)
         {
            var gameObject = gameObjectQueue.Dequeue();

            FindMissingComponents(gameObject);

            foreach (Transform child in gameObject.transform) 
               gameObjectQueue.Enqueue(child.gameObject);
         }
      }

      private static void FindMissingComponents(GameObject gameObject)
      {
         var hasMissingScripts = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject) > 0;
         
         if (hasMissingScripts)
            Logger.Log(gameObject.name);
      }
   }
}