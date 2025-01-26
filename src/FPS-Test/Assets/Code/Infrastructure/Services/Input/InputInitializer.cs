using Code.Infrastructure.Services.Input.Initializer;

namespace Code.Infrastructure.Services.Input
{
   public class InputInitializer : IInputInitializer
   {
      private readonly DIService _diService;

      public InputInitializer(DIService diService)
      {
         _diService = diService;
      }

      public void Initialize(InputMode inputMode)
      {
         switch (inputMode)
         {
            case InputMode.Mobile:
               _diService.Container.Bind<IInputService>().To<MobileInput>().AsSingle();
               break;
            case InputMode.Standalone:
               _diService.Container.Bind<IInputService>().To<StandaloneInput>().AsSingle();
               break;
         }

         IInputService inputService = _diService.Container.Resolve<IInputService>();
         inputService.Initialize(inputMode);
      }
   }
}