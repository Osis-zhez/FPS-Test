using Code.Infrastructure.Services.Input;
using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Network
{
   public class NetworkInputPopulator : MonoBehaviour
   {
      [SerializeField] private NetworkCallbacksReceiver _callbacksReceiver;

      private IInputService _inputService;

      [Inject]
      public void Construct(IInputService inputService)
      {
         _inputService = inputService;
      }

      private void OnEnable()
      {
         _callbacksReceiver.OnPopulateInput += PopulateInput;
      }

      private void OnDisable()
      {
         _callbacksReceiver.OnPopulateInput -= PopulateInput;
      }

      private void PopulateInput(NetworkRunner runner, NetworkInput input)
      {
         NetworkInputData inputData = new NetworkInputData();
         inputData.MovementInput.x = _inputService.GetHorizontalMoveAxis();
         inputData.MovementInput.y = _inputService.GetVerticalMoveAxis();

         input.Set(inputData);
      }
   }
}