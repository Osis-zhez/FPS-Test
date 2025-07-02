using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Infrastructure.Services.Audio
{
   public class AudioService : MonoBehaviour, IAudioService
   {
      public AudioSource _musicASource;
      public AudioSource _soundASource;
      public AudioSource _stepASource;
      public Dictionary<AudioId, AudioClip> _sfxDict = new Dictionary<AudioId, AudioClip>();
      private IAssetProvider _assets;
      private IStaticDataService _staticData;
      private AudioStaticData _audioData;

      [Inject]
      public void Construct(IStaticDataService staticData,
         IAssetProvider assets)
      {
         _staticData = staticData;
         _assets = assets;
      }

      public async UniTask Initialize()
      {
         _audioData = _staticData.GetAudioConfig();
         
         foreach (List<AudioConfig> audioList in _audioData.GetAllAudioLists())
         foreach (AudioConfig audioConfig in audioList)
            _sfxDict.Add(audioConfig.AudioId, await _assets.LoadWithoutRegistration<AudioClip>(audioConfig.AudioReference));
         
         PlayMusic(AudioId.Level1Music);
      }

      private void Awake() => 
         DontDestroyOnLoad(gameObject);


      public void PlaySfx(AudioId audioId)
      {
         if (audioId == AudioId.None) return;
         
         _soundASource.PlayOneShot(_sfxDict[audioId]);
      }

      public void PlayMusic(AudioId audioId)
      {
         _musicASource.clip = _sfxDict[audioId];
         _musicASource.Play();
      }

      public void PlaySfxAdjusted(AudioId audioId)
      {
         if (audioId == AudioId.None) return;
         
         _soundASource.pitch = Random.Range(0.9f, 1.1f);
         _soundASource.PlayOneShot(_sfxDict[audioId]);
      }

      public void PlayStepSfx(AudioId audioId)
      {
         if (audioId == AudioId.None) return;
         
         _stepASource.pitch = Random.Range(0.9f, 1.1f);
         _stepASource.PlayOneShot(_sfxDict[audioId]);
      }

      public AudioSource GetStepAudioSource() => 
         _stepASource;
   }
}