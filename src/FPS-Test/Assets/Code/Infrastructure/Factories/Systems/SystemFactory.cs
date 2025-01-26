using Code.Infrastructure.Services.LocalDi;
using Entitas;

namespace Code.Infrastructure.Factories.Systems
{
   public class SystemFactory : ISystemFactory
   {
      private readonly ILocalDiService _container;

      public SystemFactory(ILocalDiService container) => 
         _container = container;

      public T Create<T>() where T : ISystem =>
         _container.Container.Instantiate<T>();

      public T Create<T>(params object[] args) where T : ISystem =>
         _container.Container.Instantiate<T>(args);
   }
}