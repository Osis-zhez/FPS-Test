using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Code.Infrastructure.Services.Input.TouchInput
{
   public interface ITouchInputService
   {
      Vector2 MoveAxis { get; set; }
      Vector2 LookAxis { get; set; }
      Vector2 ShootAxis { get; set; }
      bool IsLookTouchPressed { get; set; }
      bool IsShootTouchPressed { get; set; }
      bool GetTouchFingerIsActive(int fingerIndex);
      Touch GetActiveTouches(int touchIndex);
      Vector3 GetTouchWorldPosition(int activeTouchIndex);
      void EnableEnhancedTouch();
      void DisableEnhancedTouch();
      event Action<Finger> OnAnyFingerDown;
   }
}