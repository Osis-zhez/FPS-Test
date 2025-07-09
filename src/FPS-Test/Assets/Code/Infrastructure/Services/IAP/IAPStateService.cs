namespace Code.Infrastructure.Services.IAP
{
  public class IAPStateService : IIAPStateService
  {
    private RewardStateId _rewardStateId;

    
    public IAPStateService()
    {
      
    }

    public RewardStateId RewardStateId => _rewardStateId;

    public void SetRewardState(RewardStateId stateId) => 
      _rewardStateId = stateId;
  }
}