using System.Collections.Generic;
using Code.Infrastructure.Factories.Windows;
using UnityEngine;

namespace Code.Infrastructure.Services.Window
{
   public class WindowService : IWindowService
   {
      private readonly IWindowFactory _windowFactory;
      private readonly List<WindowBase> _openedWindows = new List<WindowBase>();
      
      private Dictionary<WindowTypeId, WindowStaticBase> _windows = new Dictionary<WindowTypeId, WindowStaticBase>();
      
      public Dictionary<WindowTypeId, WindowStaticBase> Windows
      {
         get => _windows;
         set => _windows = value;
      }

      public WindowService(IWindowFactory windowFactory)
      {
         _windowFactory = windowFactory;
      }

      // public void Open(WindowTypeId windowId) => 
      //    _openedWindows.Add(_windowFactory.CreateWindow(windowId));

      public void Open(WindowTypeId windowId)
      {
         throw new System.NotImplementedException();
      }

      public void Close(WindowTypeId windowId)
      {
         WindowBase window = _openedWindows.Find(x => x.WindowId == windowId);
      
         _openedWindows.Remove(window);
      
         GameObject.Destroy(window.gameObject);
      }

      public void OpenWindow(WindowTypeId windowTypeId) =>
         _windows[windowTypeId].Show();

      public void CleanUp() => 
         _windows.Clear();
   }
}