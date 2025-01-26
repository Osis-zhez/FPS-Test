using Code.Infrastructure.Services.Audio;
using Code.Infrastructure.Services.Curtain;
using UnityEngine;

namespace Code.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public LoadingCurtain CurtainPrefab;
    public AudioService AudioService;
    private Game _game;

    private void Awake()
    {
      _game = new Game(this, Instantiate(CurtainPrefab), Instantiate(AudioService));
      // _game.StateMachine.Enter<BootstrapState1>();

      DontDestroyOnLoad(this);
    }
  }
}