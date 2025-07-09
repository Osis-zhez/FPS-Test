using Io.AppMetrica;
using UnityEngine;

namespace Code.Infrastructure.Services.Analytics
{
  public static class AnalyticStatic
  {
    private static string appKey = "123";
    
    public static void Initialize() => 
      AppMetrica.Activate(new AppMetricaConfig(appKey));

    public static void LogMessage(string message)
    {
      Debug.Log(message);
#if !UNITY_EDITOR
        AppMetrica.ReportEvent(message);
#endif
    }
    
    public static void LogMessage(string key, string value)
    {
      Debug.Log(key);
#if !UNITY_EDITOR
        AppMetrica.ReportEvent(key, value);
#endif
    }
  }
}