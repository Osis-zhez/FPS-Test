using FPS_Test_Scene_Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace FPS_Test_Scene_Code.Gameplay.Character
{
   public class CharacterMovement : MonoBehaviour
   {
      [SerializeField] private CharacterController _characterController;
      [SerializeField] private float _speed = 10f;
      private Vector3 _moveDirection;
      
      private IInputService _inputService;

      [Inject]
      public void Construct(IInputService inputService)
      {
         _inputService = inputService;
      }
      
      private void Update()
      {
         Move();
      }

      private void Move()
      {
         float xInput = _inputService.GetHorizontalMoveAxis();
         float zInput = _inputService.GetVerticalMoveAxis();

         _moveDirection = transform.right * xInput + transform.forward * zInput;

         if (_inputService.HasMoveInput()) 
            _characterController.Move(_moveDirection * _speed * Time.deltaTime);
      }
   }
}