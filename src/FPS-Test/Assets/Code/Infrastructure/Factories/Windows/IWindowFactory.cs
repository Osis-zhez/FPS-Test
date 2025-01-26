using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Infrastructure.Factories.Windows
{
   public interface IWindowFactory : IService
   {
      void SetUIRoot(RectTransform uiRoot);
      void Initialize();
   }
}