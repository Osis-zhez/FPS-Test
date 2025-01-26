using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPS_Test_Scene_Code.UI
{
   public class MainMenu : MonoBehaviour
   {
      [SerializeField] private string _startSceneName;
      [SerializeField] private Button _playBtn;

      private void Awake()
      {
         _playBtn.onClick.AddListener(StartNetWorkGame);
      }

      private void StartNetWorkGame()
      {
         SceneManager.LoadScene(_startSceneName);
      }
   }
}