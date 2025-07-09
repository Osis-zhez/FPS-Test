using UnityEngine.Purchasing;

namespace Code.Infrastructure.Services.IAP
{
  public class ProductDescription
  {
    public string Id;
    public Product Product;
    public ProductConfig Config;
    public int AvailablePurchasesLeft;
  }
}