namespace Code.Infrastructure.Services.Analytics
{
   public interface IAnalyticService : IService
   {
      void LogEvent(string eventName);
   }
}