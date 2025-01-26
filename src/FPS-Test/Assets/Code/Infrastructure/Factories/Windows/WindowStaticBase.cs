using System;
using Code.Infrastructure.Services.Input.Cursor;
using Code.Infrastructure.Services.Window;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories.Windows
{
   public class WindowStaticBase : MonoBehaviour
   {
      public WindowTypeId WindowId;
      private ICursorService _cursorService;

      public event Func<float> OnShow;
      public event Func<float> OnHide;

      [Inject]
      public void Construct(ICursorService cursorService)
      {
         _cursorService = cursorService;
      }

      public virtual async void Show()
      {
         gameObject.SetActive(true);
         await PlayShowAnimation();
         Time.timeScale = 0;
         
         _cursorService.UnlockCursor();
         
         Debug.Log("Cursor");
      }

      public virtual async void Hide()
      {
         gameObject.SetActive(false);
         await PlayHideAnimation();
         Time.timeScale = 1;
         
         _cursorService.LockCursor();
      }

      private protected async UniTask PlayShowAnimation()
      {
         float? delayTime = OnShow?.Invoke();
         if (delayTime != null)
            await UniTask.WaitForSeconds((float) delayTime);
      }

      private protected async UniTask PlayHideAnimation()
      {
         float? delayTime = OnHide?.Invoke();
         if (delayTime != null)
            await UniTask.WaitForSeconds((float) delayTime);
      }
   }
}