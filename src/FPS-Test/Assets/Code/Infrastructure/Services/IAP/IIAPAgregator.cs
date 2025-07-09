namespace Code.Infrastructure.Services.IAP
{
  public interface IIAPAgregator
  {
    IIAPStateService IApStateService { get; }
  }
}