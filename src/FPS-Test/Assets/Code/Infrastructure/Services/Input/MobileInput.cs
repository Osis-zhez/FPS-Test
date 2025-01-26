using Code.Infrastructure.Services.Input.Initializer;
using Code.Infrastructure.Services.Input.TouchInput;
using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
  public class MobileInput : InputBase
  {
    private readonly ITouchInputService _touchInput;
    private InputMode _inputMode;

    public MobileInput(ITouchInputService touchInput)
    {
      _touchInput = touchInput;
    }

    public override void Initialize(InputMode inputMode)
    {
      _inputMode = inputMode;
    }

    public override InputMode GetInputMode() => 
      _inputMode;

    public override bool IsHudStartWaveBtnUp() => 
      SimpleInput.GetButtonUp(HudStartWaveBtn);

    public override bool IsHudShoot() => 
      SimpleInput.GetButton(HudShootBtn);

    public override bool IsReloadBtnUp() => 
      SimpleInput.GetButtonUp(HudReloadBtn);

    public override bool IsHudJumpBtnUp() => 
      SimpleInput.GetButtonUp(HudJumpBtn);

    public override Vector2 GetMoveAxis() => 
      _touchInput.MoveAxis;

    public override Vector2 GetLookAxis() => 
      _touchInput.LookAxis;

    public override Vector2 GetShootAxis() => 
      _touchInput.ShootAxis;

    public override bool GetIsLookTouchPressed() => 
      _touchInput.IsLookTouchPressed;

    public override bool GetIsShootAxisPressed() => 
      _touchInput.IsShootTouchPressed;
  }

  public enum ButtonActionId
  {
    Unknown = 0,
    PauseButton = 1,
    SettingsButton = 2,
  }
}