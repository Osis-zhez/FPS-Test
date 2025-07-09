using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data.InApp;
using Code.Infrastructure.Services.PersistentProgress;
using Code.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Code.Infrastructure.Services.IAP
{
   public class IAPService : IIAPService
   {
      private readonly IIAPProvider _iapProvider;
      private readonly IIAPRewardService _iapRewardService;
      private readonly IPersistentProgressService _progressService;
      private readonly ISaveLoadService _saveLoad;

      public bool IsInitialized => _iapProvider.IsInitialized;
      public event Action Initialized;

      public IAPService(IIAPProvider iapProvider,
         IIAPRewardService iapRewardService,
         IPersistentProgressService progressService,
         ISaveLoadService saveLoad)
      {
         _iapProvider = iapProvider;
         _iapRewardService = iapRewardService;
         _progressService = progressService;
         _saveLoad = saveLoad;
      }

      public void Initialize()
      {
         _iapProvider.Initialize(this);
         _iapProvider.Initialized += () => Initialized?.Invoke();
      }

      public List<ProductDescription> Products() =>
         ProductDescriptions().ToList();

      public void StartPurchase(string productId) =>
         _iapProvider.StartPurchase(productId);

      public PurchaseProcessingResult ProcessPurchase(Product purchasedProduct)
      {
         ProductConfig productConfig = _iapProvider.Configs[purchasedProduct.definition.id];
         Debug.Log(productConfig.IapItemType);
         Debug.Log(productConfig.Name);
         switch (productConfig.IapItemType)
         {
            case IAPItemType.Donate10:
               _iapRewardService.BuyDonate();
               _progressService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
               break;
            case IAPItemType.Gold10000:
               _iapRewardService.AddGold(productConfig.Quantity);
               _progressService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
               break;
            case IAPItemType.GranadesX10:
               // _iapCompleteService.AddGranade(productConfig.Quantity);
               _progressService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
               break;
            case IAPItemType.RifleM16:
               _iapRewardService.BuyWeapon(IAPItemType.RifleM16);
               _progressService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
               break;
            case IAPItemType.RocketLauncher:
               _iapRewardService.BuyWeapon(IAPItemType.RocketLauncher);
               _progressService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
               break;
         }

         _saveLoad.SaveProgress();
         
         return PurchaseProcessingResult.Complete;
      }

      private IEnumerable<ProductDescription> ProductDescriptions()
      {
         PurchaseData purchaseData = _progressService.Progress.PurchaseData;

         foreach (string productId in _iapProvider.Products.Keys)
         {
            ProductConfig config = _iapProvider.Configs[productId];
            Product product = _iapProvider.Products[productId];

            BoughtIAP boughtIap = purchaseData.BoughtIAPs.Find(x => x.IAPid == productId);

            if (boughtIap != null)
               if (ProductBoughtOut(boughtIap, config))
                  continue;

            yield return new ProductDescription
            {
               Id = productId,
               Product = product,
               Config = config,
               AvailablePurchasesLeft =
                  boughtIap == null
                     ? config.MaxPurchaseCount
                     : config.MaxPurchaseCount - boughtIap.Count
            };
         }
      }

      private bool ProductBoughtOut(BoughtIAP boughtIap, ProductConfig config) =>
         boughtIap != null && boughtIap.Count >= config.MaxPurchaseCount;
   }
}