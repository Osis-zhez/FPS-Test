using System;

namespace Code.Infrastructure.Services.IAP
{
   public interface IIAPRewardService
   {
      void AddGold(int goldAmount);
      void AddGranade(int granadeAmount);
      void BuyWeapon(IAPItemType itemType);
      event Action<int> OnAddGold;
      event Action<int> OnGranadeAmountChanged;
      void BuyDonate();
      event Action OnBuyDonate;
   }
}