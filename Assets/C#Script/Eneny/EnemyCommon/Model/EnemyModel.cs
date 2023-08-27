using C_Script.BaseClass;
using C_Script.Eneny.Monster.FlyingEye.Controller;
using C_Script.Eneny.Monster.FlyingEye.Data;
using C_Script.Eneny.Monster.Magician.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.Eneny.EnemyCommon.Model
{
    public class EnemyModel : BaseClass.Model
    {
        public EnemyData EnemyData => Data as EnemyData;
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public Transform EnemyTrans { get; private set; }
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public Transform TargetTrans { get; private set; }
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public SpriteRenderer EnemySprite { get; private set;}
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public Animator EnemyAnimator { get; private set; }
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public CapsuleCollider2D EnemyCapCollider2D { get; private set; } 
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public Rigidbody2D EnemyRigidbody2D { get; private set; }

        [field: FoldoutGroup("Custom")] [field: SerializeField] public EnemyCore EnemyCore { get; private set; }
    }
}