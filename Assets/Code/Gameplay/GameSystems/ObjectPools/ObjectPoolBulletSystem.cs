using Code.Gameplay.Weapons;
using UnityEngine;
using UnityEngine.Pool;

namespace Code.Gameplay.GameSystems.ObjectPools
{
   public class ObjectPoolBulletSystem 
   {
      private GameObject _bulletPrefab;
      private ObjectPool<Bullet> _bulletPool;

      public ObjectPoolBulletSystem()
      {
         _bulletPrefab = Resources.Load<GameObject>("Gameplay/Weapons/Bullet");

         _bulletPool = new ObjectPool<Bullet>(
            CreatePooledObject,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyPoolObject);
      }

      public Bullet GetBulletFromPool() =>
         _bulletPool.Get();

      private Bullet CreatePooledObject()
      {
         GameObject bulletObject = GameObject.Instantiate(_bulletPrefab, Vector3.zero, Quaternion.identity);
         Bullet bulletInstance = bulletObject.GetComponent<Bullet>();
         bulletInstance.OnDisable += ReturnObjectToPool;
         bulletInstance.gameObject.SetActive(false);

         return bulletInstance;
      }

      private void ReturnObjectToPool(Bullet bulletInstance) => 
         _bulletPool.Release(bulletInstance);

      private void OnTakeFromPool(Bullet bulletInstance) => 
         bulletInstance.gameObject.SetActive(true);

      private void OnReturnToPool(Bullet bulletInstance) => 
         bulletInstance.gameObject.SetActive(false);

      private void OnDestroyPoolObject(Bullet bulletInstance) => 
         GameObject.Destroy(bulletInstance.gameObject);
   }
}