using FPS_Test_Code.Infrastructure.Services.Input;
using Zenject;

namespace FPS_Test_Code.Infrastructure.Installers
{
   public class BootstrapInstaller : MonoInstaller
   {
      public override void InstallBindings()
      {
         BindInputService();
      }

      private void BindInputService()
      {
         Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
      }
   }
}