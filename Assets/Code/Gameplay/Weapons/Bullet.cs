using System;
using System.Collections;
using UnityEngine;

namespace Code.Gameplay.Weapons
{
   public class Bullet : MonoBehaviour
   {
      [SerializeField] private Rigidbody _rigidbody;
      [SerializeField] private float _timeToDisable = 3f;
      
      public event Action<Bullet> OnDisable;

      private void OnEnable()
      {
         StartCoroutine(TimerForDisable());
      }

      public void SetDirection(Vector3 direction, float speed)
      {
         _rigidbody.velocity = Vector3.zero;
         transform.forward = direction;

         _rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
      }

      private void OnCollisionEnter(Collision other)
      {
         OnDisable?.Invoke(this);
         _rigidbody.velocity = Vector3.zero;
      }

      private IEnumerator TimerForDisable()
      {
         yield return new WaitForSeconds(_timeToDisable);
         OnDisable?.Invoke(this);
      }
   }
}