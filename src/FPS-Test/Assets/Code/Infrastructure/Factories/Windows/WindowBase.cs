using System;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.Services.Window;
using CodeBase.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Factories.Windows
{
   public class WindowBase : MonoBehaviour
   {
      public event Func<float> OnShow; 
      public WindowTypeId WindowId;

      protected IPersistentProgressService ProgressService;
      protected PlayerProgress Progress => ProgressService.Progress;

      protected float _timeToDelay;

      protected virtual void Awake() => 
         OnAwake();

      protected async virtual void Start()
      {
         SubscribeUpdates();

         OnShow?.Invoke();
         
         _timeToDelay = 0;
         
         Debug.Log(_timeToDelay);
         await UniTask.WaitForSeconds(_timeToDelay);
         Time.timeScale = 0;
      }

      private void OnDestroy() => 
         CleanUp();

      protected virtual void OnAwake()
      {
      }

      protected virtual void SubscribeUpdates()
      {
         
      }

      protected virtual void CleanUp()
      {
         
      }
   }
}