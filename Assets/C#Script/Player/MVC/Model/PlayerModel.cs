using C_Script.Player.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.Player.MVC.Model
{
    public class PlayerModel : C_Script.BaseClass.Model
    {
        public PlayerData PlayerData => Data as PlayerData;
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public Transform PlayerTrans { get; private set; }
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public SpriteRenderer PlayerSprite { get; private set;}
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public Animator PlayerAnimator { get; private set; }
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public CapsuleCollider2D PlayerCapCollider2D { get; private set; } 
        [field:FoldoutGroup("UnityComponent")] [field: SerializeField] public Rigidbody2D PlayerRigidbody2D { get; private set; }
        [field:FoldoutGroup("Custom")] [field: SerializeField] public C_Script.BaseClass.Core PlayerCore { get; private set; }
        [field: FoldoutGroup("Custom")] [field: SerializeField] public SkillData SkillData { get; private set; }
    }
}
