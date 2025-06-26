using System;
using Code.Gameplay.GameIDs;
using Code.Infrastructure.Services.Audio;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "EnemyData", menuName = "Static Data/Enemy")]
  public class EnemyStaticData : ScriptableObject
  {
    // public GameObject Prefab;
    public string EnemyName;
    public AssetReferenceT<GameObject> RagdollPrefab;
    public ZombieType ZombieType;
    
    [ColorFoldoutGroup("Enemy AI", 0f, 1f, 0)]
    public TargetId TargetId;
    [ColorFoldoutGroup("Enemy AI")]
    public float Speed;
    [ColorFoldoutGroup("Enemy AI")]
    public float StoppingDistance;
    [ColorFoldoutGroup("Enemy AI")]
    public float AnimatorSpeed;
    
    [ColorFoldoutGroup("Enemy Health", 0.9f, 0, 0)]
    public float MaxHealth;

    [ColorFoldoutGroup("Enemy Attack", 0f, 0, 0.9f)]
    public float AttackCoolDown;
    [ColorFoldoutGroup("Enemy Attack")]
    public LayerMask AttackLayer;
    [ColorFoldoutGroup("Enemy Attack")]
    public float Cleavage;
    [ColorFoldoutGroup("Enemy Attack")]
    public float AttackDistance;
    [ColorFoldoutGroup("Enemy Attack")]
    public float Damage;
        
    [ColorFoldoutGroup("Enemy Explode", 0f, 1f, 0)]
    public bool IsDiedExplode;
    [ColorFoldoutGroup("Enemy Explode")]
    public bool IsToCloseExplode;
    [ColorFoldoutGroup("Enemy Explode")]
    public bool IsEnemyFire;
    [ColorFoldoutGroup("Enemy Explode")]
    public AudioId ExplodeAudioId;
    [ColorFoldoutGroup("Enemy Explode")]
    public float ExplosionRadius;
    [ColorFoldoutGroup("Enemy Explode")]
    public float YOffsetForExplode = 1;
    [ColorFoldoutGroup("Enemy Explode")]
    public float ExplodeDamage = 100;
    [ColorFoldoutGroup("Enemy Explode")]
    public LayerMask LayerMaskForDamage;
    
    [ColorFoldoutGroup("Enemy Ranged Attack", 0.9f, 0, 0)]
    public bool IsRangedAttack;
    [ColorFoldoutGroup("Enemy Ranged Attack")]
    public float RangeDistance;
    [ColorFoldoutGroup("Enemy Ranged Attack")]
    public float RangeAttackCooldown;
    [ColorFoldoutGroup("Enemy Ranged Attack")]
    public float RangeAttackDamage;


  }


public class ColorFoldoutGroupAttribute : PropertyGroupAttribute
{
    public float R, G, B, A;

    public ColorFoldoutGroupAttribute(string path) : base (path)
    {

    }

    public ColorFoldoutGroupAttribute(string path, float r, float g, float b, float a = 1f) : base(path)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }

    protected override void CombineValuesWith(PropertyGroupAttribute other)
    {
        var otherAttr = (ColorFoldoutGroupAttribute)other;

        this.R = Math.Max(otherAttr.R, this.R);
        this.G = Math.Max(otherAttr.G, this.G);
        this.B = Math.Max(otherAttr.B, this.B);
        this.A = Math.Max(otherAttr.A, this.A);
    }
}
#if UNITY_EDITOR
public class ColorFoldoutGroupAttributeDrawer : OdinGroupDrawer<ColorFoldoutGroupAttribute>
{
    private LocalPersistentContext<bool> isExpanded;

    protected override void Initialize()
    {
        this.isExpanded = this.GetPersistentValue<bool>("ColorFoldoutGroupAttributeDrawer.isExpanded",
            GeneralDrawerConfig.Instance.ExpandFoldoutByDefault);
    }
    

    protected override void DrawPropertyLayout(GUIContent label)
    {
        GUIHelper.PushColor(new Color(this.Attribute.R, this.Attribute.G, this.Attribute.B, this.Attribute.A));
        SirenixEditorGUI.BeginBox();
        SirenixEditorGUI.BeginBoxHeader();
        GUIHelper.PopColor();
    
        this.isExpanded.Value = SirenixEditorGUI.Foldout(this.isExpanded.Value, label);
        SirenixEditorGUI.EndBoxHeader();
    
        if (SirenixEditorGUI.BeginFadeGroup(this, this.isExpanded.Value))
        {
            for (int i = 0; i < this.Property.Children.Count; i++)
            {
                this.Property.Children[i].Draw();
            }
        }
        SirenixEditorGUI.EndFadeGroup();
        SirenixEditorGUI.EndBox();
    }
}
#endif
}