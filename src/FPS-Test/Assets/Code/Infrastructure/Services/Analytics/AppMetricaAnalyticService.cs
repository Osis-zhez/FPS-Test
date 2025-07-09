namespace Code.Infrastructure.Services.Analytics
{
   public class AppMetricaAnalyticService : IAnalyticService
   {
      private string _apKey = "123";
      
      public void LogMessage(string eventName) => 
         AnalyticStatic.LogMessage(eventName);
   }
}