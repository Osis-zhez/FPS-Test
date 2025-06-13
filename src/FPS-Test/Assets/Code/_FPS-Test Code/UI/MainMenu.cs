using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code._FPS_Test_Code.UI
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