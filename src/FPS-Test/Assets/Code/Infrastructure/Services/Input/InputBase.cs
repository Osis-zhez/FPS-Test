using System;
using System.Collections.Generic;
using Code.Infrastructure.Services.Input.Initializer;
using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
   public abstract class InputBase : IInputService
   {
    
      public Dictionary<ButtonActionId, Action> ButtonActions = new Dictionary<ButtonActionId, Action>();

      protected const string HudStartWaveBtn = "HudStartWaveBtn";
      protected const string HudJumpBtn = "Hud_Jump";
      protected const string HudShootBtn = "Hud_Shoot";
      protected const string PauseBtn = "PauseBtn";
      protected const string HudReloadBtn = "Hud_Reload";
      private const string HealthPack = "HealthPack";
      private const string GranadeThrow = "GranadeThrow";
      private const string RifleAmmoBuy = "RifleAmmoBuy";
      private const string ShotgunAmmoBuy = "ShotgunAmmoBuy";
      private const string HealthPackBuy = "HealthPackBuy";
      private const string GrenadeBuy = "GrenadeBuy";
      private const string Fire = "Fire";
      private const string MetaUIStartButton = "MetaUIStartButton";
      private const string PickupBtn = "PickupBtn";
      private const string MetaUiSurvivalButton = "MetaUISurvivalButton";

      public void CleanUp() =>
         ButtonActions.Clear();

      public abstract void Initialize(InputMode inputMode);

      public abstract InputMode GetInputMode();

      public abstract bool IsHudStartWaveBtnUp();

      public void ClickActionButton(ButtonActionId buttonId)
      {
         // if (!Enabled) return;
         if (ButtonActions.ContainsKey(buttonId))
            ButtonActions[buttonId].Invoke();
      }
      

      public void SubscribeOnActionButton(ButtonActionId buttonTypeId, Action action)
      {
         if (!ButtonActions.ContainsKey(buttonTypeId))
            ButtonActions.Add(buttonTypeId, null);
         ButtonActions[buttonTypeId] += action;
      }

      public void UnsubscribeActionButton(ButtonActionId buttonTypeId, Action action)
      {
         if (ButtonActions.TryGetValue(buttonTypeId, out Action currentAction))
            currentAction -= action;
      }

      public abstract bool IsHudShoot();

      public abstract bool IsReloadBtnUp();

      public abstract bool IsHudJumpBtnUp();

      public bool IsHealthPackBtnUp() => 
         SimpleInput.GetButtonUp(HealthPack);

      public bool IsGranadeBtnUp() => 
         SimpleInput.GetButtonUp(GranadeThrow);

      public bool IsRifleAmmoBuyBtnUp() => 
         SimpleInput.GetButtonUp(RifleAmmoBuy);
    
      public bool IsShotgunAmmoBuyBtnUp() => 
         SimpleInput.GetButtonUp(ShotgunAmmoBuy);
    
      public bool IsHealthPackBuyBtnUp() =>
         SimpleInput.GetButtonUp(HealthPackBuy);

      public bool IsGrenadeBuyBtnUp() => 
         SimpleInput.GetButtonUp(GrenadeBuy);

      public bool IsAttackButtonUp() => 
         SimpleInput.GetButtonUp(Fire);

      public bool IsMetaUIStartBtnUp() => 
         SimpleInput.GetButtonUp(MetaUIStartButton);
      
      public bool IsMetaUISurvivalBtnUp() => 
         SimpleInput.GetButtonUp(MetaUiSurvivalButton);

      public bool IsPickupBtnUp() => 
         SimpleInput.GetButtonUp(PickupBtn);

      public abstract Vector2 GetMoveAxis();

      public abstract Vector2 GetLookAxis();

      public abstract Vector2 GetShootAxis();

      public abstract bool GetIsLookTouchPressed();

      public abstract bool GetIsShootAxisPressed();
   }
}