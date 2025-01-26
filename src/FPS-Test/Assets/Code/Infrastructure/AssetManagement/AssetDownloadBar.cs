using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Infrastructure.AssetManagement
{
   public class AssetDownloadBar : MonoBehaviour
   {
      [SerializeField] private Image _imageFill;
      [SerializeField] private TextMeshProUGUI _textSize;

      public void SetProgress(float percentageProgress)
      {
         _imageFill.fillAmount = percentageProgress;
      }

      public void SetDownloadizeText(string sizeText)
      {
         _textSize.text = $"Downloading: {sizeText} Mb";
      }
   }
}