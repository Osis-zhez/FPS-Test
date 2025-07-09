namespace Code.Infrastructure.Services.IAP
{
  public interface IIAPStateService
  {
    void SetRewardState(RewardStateId stateId);
    RewardStateId RewardStateId { get; }
  }
}