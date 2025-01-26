using CodeBase.Gameplay.GameSystems.ObjectPools;
using Zenject;

namespace CodeBase.Infrastructure.Installers
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