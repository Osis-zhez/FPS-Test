using Code.Infrastructure.Services.Warmup;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "WeaponType", menuName = "Static Data/WeaponType")]
    public class WeaponStaticData : ScriptableObject
    {
        public string WeaponName;
        public int WeaponClass;
        public int WeaponID;
        public int ShopSortId;
        public string Description;
        public int MagazineMax;
        public int ShopCost;
        public WeaponId WeaponId;
        public AssetReferenceT<GameObject> Prefab;
        public AssetReferenceT<RuntimeAnimatorController> AnimatorController;
        public AssetReferenceT<Sprite> WeaponSprite;
    }
}
