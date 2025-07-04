﻿using Code._FPS_Test_Code.Gameplay.GameSystems.ObjectPools;
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
         Container.BindInterfacesAndSelfTo<ObjectPoolBulletSystem>().AsSingle();
      }
   }
}