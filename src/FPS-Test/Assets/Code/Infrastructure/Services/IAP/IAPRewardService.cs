using System;

namespace Code.Infrastructure.Services.IAP
{
   public class IAPRewardService : IIAPRewardService
   {
      public event Action<int> OnAddGold;
      public event Action<int> OnGranadeAmountChanged;
      public event Action OnBuyDonate;

      public void BuyDonate() => 
         OnBuyDonate?.Invoke();

      public void AddGold(int goldAmount) =>
         OnAddGold?.Invoke(goldAmount);

      public void AddGranade(int granadeAmount) =>
         OnGranadeAmountChanged?.Invoke(granadeAmount);

      public void BuyWeapon(IAPItemType itemType)
      {
         switch (itemType)
         {
            case IAPItemType.RifleM16:
               break;
            case IAPItemType.RocketLauncher:
               break;
         }
      }
   }
}