using Code._FPS_Test_Code.Gameplay.GameSystems.ObjectPools;
using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code._FPS_Test_Code.Gameplay.Weapons
{
   public class RifleM16 : MonoBehaviour
   {
      [SerializeField] private Transform _shootPoint;
      [SerializeField] private float _bulletSpeed;
      
      private IInputService _inputService;
      private ObjectPoolBulletSystem _objectPoolBulletSystem;

      [Inject]
      public void Construct(IInputService inputService,
         ObjectPoolBulletSystem objectPoolBulletSystem)
      {
         _inputService = inputService;
         _objectPoolBulletSystem = objectPoolBulletSystem;
      }

      private void Update()
      {
         if (_inputService.HasShootInput()) 
            Shoot();
      }

      public void Shoot()
      {
         Bullet bulletInstance = _objectPoolBulletSystem.GetBulletFromPool();
         bulletInstance.transform.position = _shootPoint.position;
         bulletInstance.transform.forward = transform.forward;
         bulletInstance.SetDirection(transform.forward, _bulletSpeed);
      }
   }
}