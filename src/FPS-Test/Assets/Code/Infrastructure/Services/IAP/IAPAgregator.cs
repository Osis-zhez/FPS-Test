namespace Code.Infrastructure.Services.IAP
{
  public class IAPAgregator : IIAPAgregator
  {
    private readonly IIAPStateService _iapStateService;

    public IAPAgregator(IIAPStateService iapStateService)
    {
      _iapStateService = iapStateService;
    }
    
    public IIAPStateService IApStateService => _iapStateService;
  }
}