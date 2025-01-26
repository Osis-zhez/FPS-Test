using Code.Infrastructure.Services.Input.Initializer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Infrastructure.Services.Input
{
   public class StandaloneInput : InputBase
   {
      private InputMode _inputMode;

      public override void Initialize(InputMode inputMode)
      {
         _inputMode = inputMode;
      }

      public override InputMode GetInputMode() => 
         _inputMode;

      public override bool IsHudStartWaveBtnUp() => 
         UnityEngine.Input.GetKeyUp(KeyCode.N);

      public override bool IsHudShoot() => 
         UnityEngine.Input.GetMouseButton(0);

      public override bool IsReloadBtnUp() => 
         UnityEngine.Input.GetKeyUp(KeyCode.R);

      public override bool IsHudJumpBtnUp()
      {
         if (UnityEngine.Input.GetKey(KeyCode.Space))
            return true;
         else
            return false;
      }

      public override Vector2 GetMoveAxis()
      {
         float x = UnityEngine.Input.GetAxis("Horizontal");
         float y = UnityEngine.Input.GetAxis("Vertical");
         return new Vector2(x, y);
      }

      public override Vector2 GetLookAxis()
      {
         float x = Mouse.current.delta.ReadValue().x;
         float y = Mouse.current.delta.ReadValue().y;
         return new Vector2(x, y);
      }

      public override Vector2 GetShootAxis() =>
         new Vector2();

      public override bool GetIsLookTouchPressed() =>
         true;

      public override bool GetIsShootAxisPressed() =>
         false;
   }
}