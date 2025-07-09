using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Services.Warmup
{
   public interface IWarmupService : IService
   {
      UniTask WarmupLevelMode();
      UniTask WarmupMeta();
      UniTask WarmupTutorial();
   }
}