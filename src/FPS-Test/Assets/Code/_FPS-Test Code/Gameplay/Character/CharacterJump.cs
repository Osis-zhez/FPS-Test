using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code._FPS_Test_Code.Gameplay.Character
{
   public class CharacterJump : MonoBehaviour
   {
      [SerializeField] private CharacterController _characterController;
      [SerializeField] private float _gravity = -10f;
      [SerializeField] private float _jumpHeight = 5;
      [SerializeField] private Transform _groundCheck;
      [SerializeField] private LayerMask _groundLayer;
      
      private Vector3 _velocity;
      private bool _isGrounded;
      
      private IInputService _inputService;

      [Inject]
      public void Construct(IInputService inputService)
      {
         _inputService = inputService;
      }

      private void Update()
      {
         CheckIsGrounded();
         ProcessJump();
      }

      private void ProcessJump()
      {
         if (_inputService.IsJumpBtnUp())
            if (_isGrounded)
               Jump();
            else
               _velocity.y += _gravity * Time.deltaTime;
         else
            _velocity.y += _gravity * Time.deltaTime;
         
         _characterController.Move(_velocity * Time.deltaTime);
      }

      private void Jump() => 
         _velocity.y = Mathf.Sqrt(_jumpHeight * 2 * - _gravity);

      private void CheckIsGrounded() => 
         _isGrounded = Physics.CheckSphere(_groundCheck.position, 0.3f, _groundLayer);
   }
}