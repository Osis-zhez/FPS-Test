using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Installers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    private void Awake()
    {
      var bootstrapper = FindObjectOfType<BootstrapInstaller>();

      if (bootstrapper != null) return;
      
      SceneManager.LoadScene(SceneAddress.Initial);
    }
  }
}