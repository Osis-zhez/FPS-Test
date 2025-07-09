using Code._FPS_Test_Code.Gameplay.GameSystems.ObjectPools;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Factories.Windows;
using Code.Infrastructure.Services.Window;
using Zenject;

namespace Code.Infrastructure.Installers
{
   public class GameInstaller : MonoInstaller
   {
      public override void InstallBindings()
      {
         BindObjectPoolSystems();
      }

      private void BindObjectPoolSystems()
      {
         Container.Bind<IObjectPoolBulletSystem>().To<ObjectPoolBulletSystem>().AsSingle();
      }
      
   }
}