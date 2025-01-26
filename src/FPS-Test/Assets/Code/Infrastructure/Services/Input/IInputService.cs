using System;
using Code.Infrastructure.Services.Input.Initializer;
using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
  public interface IInputService : IService
  {
    void CleanUp();
    void Initialize(InputMode inputMode);
    void ClickActionButton(ButtonActionId buttonId);
    void SubscribeOnActionButton(ButtonActionId buttonTypeId, Action action);
    void UnsubscribeActionButton(ButtonActionId buttonTypeId, Action action);
    InputMode GetInputMode();
    bool IsHudShoot();
    bool IsReloadBtnUp();
    bool IsHudJumpBtnUp();
    bool IsHealthPackBtnUp();
    bool IsGranadeBtnUp();
    bool IsRifleAmmoBuyBtnUp();
    bool IsShotgunAmmoBuyBtnUp();
    bool IsHealthPackBuyBtnUp();
    bool IsGrenadeBuyBtnUp();
    bool IsAttackButtonUp();
    bool IsMetaUIStartBtnUp();
    bool IsPickupBtnUp();
    Vector2 GetMoveAxis();
    Vector2 GetLookAxis();
    Vector2 GetShootAxis();
    bool GetIsLookTouchPressed();
    bool GetIsShootAxisPressed();
    bool IsHudStartWaveBtnUp();
    bool IsMetaUISurvivalBtnUp();
  }
}