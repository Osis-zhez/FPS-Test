using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Code.Infrastructure.Services.Input.TouchInput
{
   public class TouchInputService : ITouchInputService
   {

      public event Action<Finger> OnAnyFingerDown;

      private LayerMask _colliderLayerMask = LayerMask.GetMask("Ground Input");
      private RaycastHit _raycastHit;
      
      private Vector2 _moveAxis;
      private Vector2 _lookAxis;
      private Vector2 _shootAxis;
    
      private bool isLookTouchPressed;
      private bool isShootTouchPressed;

      public TouchInputService()
      {
         EnhancedTouchSupport.Enable();

         Touch.onFingerDown += OnFingerDown;
      }

      public Vector2 MoveAxis 
      {
         get => _moveAxis;
         set => _moveAxis = value;
      }

      public Vector2 LookAxis 
      {
         get => _lookAxis;
         set => _lookAxis = value;
      }

      public Vector2 ShootAxis 
      {
         get => _shootAxis;
         set => _shootAxis = value;
      }

      public bool IsLookTouchPressed
      {
         get => isLookTouchPressed;
         set => isLookTouchPressed = value;
      }

      public bool IsShootTouchPressed
      {
         get => isShootTouchPressed;
         set => isShootTouchPressed = value;
      }

      public bool GetTouchFingerIsActive(int fingerIndex) => 
         Touch.fingers[fingerIndex].isActive;

      public Touch GetActiveTouches(int touchIndex) => 
         Touch.activeTouches[touchIndex];

      public Vector3 GetTouchWorldPosition(int activeTouchIndex)
      {
         Touch myTouch = Touch.activeTouches[activeTouchIndex];
         Vector3 touchScreenPos = myTouch.screenPosition;
         Ray raycast = Camera.main.ScreenPointToRay(touchScreenPos);
         if (Physics.Raycast(raycast, out _raycastHit, 999f, _colliderLayerMask))
            return _raycastHit.point;
         else
            return Vector3.zero;
      }

      private void OnFingerDown(Finger finger) => 
         OnAnyFingerDown?.Invoke(finger);

      public void EnableEnhancedTouch() => 
         EnhancedTouchSupport.Enable();

      public void DisableEnhancedTouch() => 
         EnhancedTouchSupport.Disable();
   }
}