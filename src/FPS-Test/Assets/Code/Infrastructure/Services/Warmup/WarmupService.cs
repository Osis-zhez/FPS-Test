using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.LocalDi;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Services.Warmup
{
   public class WarmupService : IWarmupService
   {
      private readonly IAssetProvider _assets;
      private readonly IStaticDataService _staticData;
      private readonly ILocalDiService _localDi;

      public WarmupService(IAssetProvider assets,
         IStaticDataService staticData,
         ILocalDiService localDi)
      {
         _assets = assets;
         _staticData = staticData;
         _localDi = localDi;
      }

      public async UniTask WarmupLevelMode()
      {
         await _assets.Load<GameObject>("Ragdoll_01");
         await _assets.Load<GameObject>("Ragdoll_02");
         await _assets.Load<GameObject>("Ragdoll_03");
         await _assets.Load<GameObject>("Ragdoll_04");
         await _assets.Load<GameObject>("Ragdoll_05");
         await _assets.Load<GameObject>("Ragdoll_06");
         
         await _assets.Load<GameObject>("Gold");
         await _assets.Load<GameObject>("SpareParts");
         await _assets.Load<GameObject>("Health Pack");
         await _assets.Load<GameObject>("Granade");
      }

      public async UniTask WarmupMeta()
      {
         await _assets.Load<GameObject>("Shop");
         await _assets.Load<GameObject>("WeaponCard");
         await _assets.Load<GameObject>("WeaponryBaseWindow");
         await _assets.Load<GameObject>("PauseWindow");
         await _assets.Load<GameObject>(AssetAddress.MetaUIRoot);
         await _assets.Load<Sprite>("RemoveAds Icon");
         await _assets.Load<Sprite>("Gold Icon");

         List<UniTask<Sprite>> uniTasks = new List<UniTask<Sprite>>();
         foreach (KeyValuePair<WeaponId, WeaponStaticData> valuePair in _staticData.GetWeaponAllData())
         {
            if (valuePair.Value.WeaponId == WeaponId.Empty)
               continue;
            uniTasks.Add(_assets.Load<Sprite>(valuePair.Value.WeaponSprite));
         }

         await UniTask.WhenAll(uniTasks);
      }
      
      public async UniTask WarmupTutorial()
      {
         // await _assets.Load<GameObject>(ZombieType.Enemy_01.ToString());
         // await _assets.Load<GameObject>(AssetAddress.EnemyPatrolPoint);
      }
   }
}