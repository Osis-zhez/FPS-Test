using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Services.Audio
{
   public interface IAudioService
   {
      UniTask Initialize();
      void PlaySfx(AudioId audioId);
      void PlayMusic(AudioId audioId);
      void PlaySfxAdjusted(AudioId audioId);
      void PlayStepSfx(AudioId audioId);
      AudioSource GetStepAudioSource();
   }
}