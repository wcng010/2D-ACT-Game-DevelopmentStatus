using C_Script.Eneny.Monster.Magician.State.StateBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.BaseClass
{
    public enum EnemyStateType
    {
        SleepStateEnemy,
        WakeStateEnemy,
        IdleStateEnemy,
        PatrolStateEnemy,
        PursuitStateEnemy,
        MeleeAttackStateEnemy,
        MeleeAttack1StateEnemy,
        MeleeAttack2StateEnemy,
        MeleeAttack3StateEnemy,
        RemoteAttackStateEnemy,
        ComboAttackStateEnemy,
        HeavyAttackStateEnemy,
        LightAttackStateEnemy,
        CoolDownStateEnemy,
        HurtStateEnemy,
        DeathStateEnemy,
        WaitStateEnemy,
        DodgeStateEnemy,
        ReadyStateEnemy,
        WinStateEnemy
    }
    public class EnemyData:AttackObjectDataSo
    {
        [field:SerializeField][field:FoldoutGroup("OriginalSetting", Order = -1)] public EnemyStateType OriginState { get; private set; }
        [field:SerializeField][field:FoldoutGroup("Range")] public float PursuitRange{ get; private set; }
        [field:SerializeField][field:FoldoutGroup("Range")] public uint PatrolRange{ get; private set; }
        [field:SerializeField][field:FoldoutGroup("Speed")] public uint PatrolSpeed{ get; private set; }
        [field:SerializeField][field:FoldoutGroup("Speed")] public float PursuitSpeed{ get; private set; }
        
        [field: SerializeField] [field: FoldoutGroup("CombatMessage")] public Vector2 HitEffectOffSet1{ get; set; }
        [field: SerializeField] [field: FoldoutGroup("CombatMessage")] public Vector2 HitEffectOffSet2{ get; set; }
        [field: SerializeField] [field: FoldoutGroup("CombatMessage")] public bool SuperArmor { get; set; }
        
        [field: SerializeField] [field: FoldoutGroup("CombatMessage")] public bool AttackInvalid { get; set; }

        [field: SerializeField] [field: FoldoutGroup("Effect")] public GameObject HitEffect1 { get; private set; }
        [field: SerializeField] [field: FoldoutGroup("Effect")] public GameObject HitEffect2 { get; private set; }
        
        [field: SerializeField] [field: FoldoutGroup("Effect")] public GameObject HitEffect3 { get; private set; }

        [field: SerializeField] [field: FoldoutGroup("CombatMessage")][field:Range(0,1)] public float DizzinessRate { get; private set; }

    }
}