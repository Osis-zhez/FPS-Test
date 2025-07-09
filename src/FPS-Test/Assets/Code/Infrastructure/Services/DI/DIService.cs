using System;
using Zenject;

namespace Code.Infrastructure.Services.DI
{
  public class DIService
  {
    private static DIService _instance;
    
    public static DIService Instance => _instance ?? (_instance = new DIService());

    public DiContainer Container = ProjectContext.Instance.Container;
    
    public void RegisterSingle<TService>(TService implementation) where TService : IService
    {
      Container.ResolveAll<IService>();
      Container.BindInstance<TService>(implementation).AsSingle();
    }
    
    public void RegisterLocal<TService>(TService implementation) where TService : IService
    {
      Type type = implementation.GetType();
      
      Container.BindInstance(implementation.GetType().BaseType).AsSingle();
      implementation.GetType();
    }
  }
}