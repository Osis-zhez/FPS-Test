using System.Collections.Generic;
using Code.Infrastructure.Factories.Windows;

namespace Code.Infrastructure.Services.Window
{
   public interface IWindowService
   {
      void CleanUp();
      // void OpenWindow(WindowTypeId windowTypeId);
      Dictionary<WindowTypeId, WindowStaticBase> Windows { get; set; }
      void Open(WindowTypeId windowId);
      void Close(WindowTypeId windowId);
   }
}