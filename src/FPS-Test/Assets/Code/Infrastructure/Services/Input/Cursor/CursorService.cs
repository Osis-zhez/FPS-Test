using Code.Infrastructure.Services.Input.Initializer;
using UnityEngine;

namespace Code.Infrastructure.Services.Input.Cursor
{
   public class CursorService : ICursorService
   {
      private readonly IInputService _inputService;

      public CursorService(IInputService inputService)
      {
         _inputService = inputService;
      }

      public void LockCursor()
      {
         if (_inputService.GetInputMode() == InputMode.Standalone)
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
      }

      public void UnlockCursor()
      {
         if (_inputService.GetInputMode() == InputMode.Standalone)
         {
            Debug.Log("Unlock");
            UnityEngine.Cursor.lockState = CursorLockMode.None;
         }
      }
   }
}