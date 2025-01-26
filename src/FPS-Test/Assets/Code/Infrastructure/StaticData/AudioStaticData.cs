using System;
using System.Collections.Generic;
using Code.Infrastructure.Services.Audio;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.StaticData
{
   [CreateAssetMenu(fileName = "AudioConfig", menuName = "Static Data/Audio")]
   public class AudioStaticData : ScriptableObject
   {
      public List<AudioConfig> Music;
      public List<AudioConfig> Common;
      public List<AudioConfig> Weapons;
      public List<AudioConfig> UI;

      public List<List<AudioConfig>> GetAllAudioLists()
      {
         List<List<AudioConfig>> configList = new List<List<AudioConfig>>();
         configList.Add(Music);
         configList.Add(Common);
         configList.Add(Weapons);
         configList.Add(UI);
         
         return configList;
      }
   }

   [Serializable]
   public class AudioConfig
   {
      public AudioId AudioId;
      public AssetReferenceT<AudioClip> AudioReference;
   }
}
