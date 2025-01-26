using FPS_Test_Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace FPS_Test_Code.Gameplay.Character
{
   public class CharacterLook : MonoBehaviour
   {
      [SerializeField] private float _mouseSensitivity;
      [SerializeField] private Transform _cameraRoot;
      [SerializeField] private Transform _playerBody;

      private float _xLook;
      private float _yLook;
      private float _xRotation;

      private IInputService _inputService;

      [Inject]
      public void Construct(IInputService inputService)
      {
         _inputService = inputService;

         Cursor.lockState = CursorLockMode.Locked;
      }

      private void Update()
      {
         Look();
      }

      private void Look()
      {
         _xLook = _inputService.GetHorizontalLookAxis() * _mouseSensitivity * Time.deltaTime;
         _yLook = _inputService.GetVerticalLookAxis() * _mouseSensitivity * Time.deltaTime;

         _xRotation -= _yLook;
         _xRotation = Mathf.Clamp(_xRotation, -80, 40);

         _cameraRoot.localRotation = Quaternion.Euler(_xRotation, 0, 0);

         _playerBody.Rotate(Vector3.up * _xLook);
      }
   }
}