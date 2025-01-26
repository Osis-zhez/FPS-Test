using FPS_Test_Scene_Code.Gameplay.GameSystems.ObjectPools;
using Zenject;

namespace FPS_Test_Scene_Code.Infrastructure.Installers
{
   public class SceneInstaller : MonoInstaller
   {
      public override void InstallBindings()
      {
         BindObjectPoolSystems();
      }

      private void BindObjectPoolSystems()
      {
         Container.BindInterfacesAndSelfTo<ObjectPoolBulletSystem>().AsSingle();
      }
   }
}