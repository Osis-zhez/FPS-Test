using CodeBase.Infrastructure.Services.Input;
using Zenject;

namespace CodeBase.Infrastructure.Installers
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