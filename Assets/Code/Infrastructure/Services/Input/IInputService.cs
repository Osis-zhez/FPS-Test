using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
   public interface IInputService
   {
      float GetVerticalMoveAxis();
      float GetHorizontalMoveAxis();
      float GetVerticalLookAxis();
      float GetHorizontalLookAxis();
      bool HasMoveInput();
      bool HasShootInput();
      bool HasLookInput();
      bool IsJumpBtnUp();
   }
}