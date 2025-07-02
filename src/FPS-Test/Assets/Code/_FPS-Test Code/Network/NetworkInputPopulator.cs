using Code.Infrastructure.Services.Input;
using Fusion;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code._FPS_Test_Code.Network
{
   public class NetworkInputPopulator : MonoBehaviour
   {
      [FormerlySerializedAs("_callbacksReceiver")] [SerializeField] private NetworkCallbacksReceiverMonoBeh callbacksReceiverMonoBeh;

      private IInputService _inputService;

      [Inject]
      public void Construct(IInputService inputService)
      {
         _inputService = inputService;
      }

      private void OnEnable()
      {
         callbacksReceiverMonoBeh.OnPopulateInput += PopulateInput;
      }

      private void OnDisable()
      {
         callbacksReceiverMonoBeh.OnPopulateInput -= PopulateInput;
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