using Code._FPS_Test_Code.Gameplay.Weapons;

namespace Code._FPS_Test_Code.Gameplay.GameSystems.ObjectPools
{
  public interface IObjectPoolBulletSystem
  {
    Bullet GetBulletFromPool();
  }
}