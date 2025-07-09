using System;
using UnityEngine.Purchasing;

namespace Code.Infrastructure.Services.IAP
{
  [Serializable]
  public class ProductConfig
  {
    public string Id;
    public string Name;
    public ProductType ProductType;
    public int MaxPurchaseCount; 
    public IAPItemType IapItemType;
    public int Quantity;
    public string Price;
    public string Icon;
    public string AssetAddress;
  }
}