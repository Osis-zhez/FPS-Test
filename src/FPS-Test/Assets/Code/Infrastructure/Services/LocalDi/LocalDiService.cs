using Zenject;

namespace Code.Infrastructure.Services.LocalDi
{
   public class LocalDiService : ILocalDiService
   {
      public static LocalDiService Instance = _instance ?? (_instance = new LocalDiService());
      
      public DiContainer Container { get; set; }

      private readonly DIService _di = DIService.Instance;
      
      private static LocalDiService _instance;

      public LocalDiService()
      {
         
      }

      public void WarmUp()
      {
         Container = new DiContainer(_di.Container);
      }

      public void CleanUp()
      {
         Container = new DiContainer();
      }
   }
}