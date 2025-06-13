using Code.Infrastructure.Installers;
using Code.Infrastructure.States;
using Code.Infrastructure.States.BootStates;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    private ProjectContext _projectContext;

    private void Awake()
    {
      var bootstrapper = FindObjectOfType<BootstrapInstaller>();

      if (bootstrapper != null) return;

      _projectContext = ProjectContext.Instance;

      InitializeProjectContext();

      InitializeBootstrapState();
    }

    private void InitializeProjectContext() =>
      _projectContext.EnsureIsInitialized();

    private void InitializeBootstrapState() =>
      _projectContext.Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
  }
}