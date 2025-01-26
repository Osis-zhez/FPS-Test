using Zenject;

namespace Code.Infrastructure.Services.LocalDi
{
   public interface ILocalDiService
   {
      void CleanUp();
      void WarmUp();
      DiContainer Container { get; set; }
   }
}