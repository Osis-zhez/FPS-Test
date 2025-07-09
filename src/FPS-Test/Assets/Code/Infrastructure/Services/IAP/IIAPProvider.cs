using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Code.Infrastructure.Services.IAP
{
   public interface IIAPProvider
   {
      Dictionary<string, ProductConfig> Configs { get; }
      Dictionary<string, Product> Products { get; }
      bool IsInitialized { get; }
      event Action Initialized;
      void Initialize(IAPService iapService);
      void StartPurchase(string productId);
      void OnInitialized(IStoreController controller, IExtensionProvider extensions);
      void OnInitializeFailed(InitializationFailureReason error);
      void OnInitializeFailed(InitializationFailureReason error, string message);
      PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent);
      void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason);
   }
}