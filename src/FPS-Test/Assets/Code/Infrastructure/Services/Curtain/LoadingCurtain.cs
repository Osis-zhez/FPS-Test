using System;
using System.Collections;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Services.Curtain
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;
    public AssetDownloadBar AssetDownloadBar;
    
    public Action OnHide;

    private IAssetDownloadReporter _assetDownloadReporter;

    [Inject]
    public void Construct(IAssetDownloadReporter assetDownloadReporter)
    {
      _assetDownloadReporter = assetDownloadReporter;
    }

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }
    
    public void Hide()
    {
      StartCoroutine(DoFadeIn());
    }
    
    private void DisplayDownloadProgress()
    {
      AssetDownloadBar.gameObject.SetActive(true);
      AssetDownloadBar.SetProgress(_assetDownloadReporter.Progress);
    }

    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.02f);
      }
      
      OnHide?.Invoke();
      gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
    }
  }
}