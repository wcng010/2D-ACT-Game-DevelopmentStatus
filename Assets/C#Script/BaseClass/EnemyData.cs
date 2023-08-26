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
        ReadyStateEnemy
    }
    public class EnemyData:AttackObjectDataSo
    {
        [field:SerializeField][field:FoldoutGroup("OriginalSetting", Order = -1)] public EnemyStateType OriginState { get; private set; }
        [field:SerializeField][field:FoldoutGroup("Range")] public float PursuitRange{ get; private set; }
        [field:SerializeField][field:FoldoutGroup("Range")] public uint PatrolRange{ get; private set; }
        [field:SerializeField][field:FoldoutGroup("Speed")] public uint PatrolSpeed{ get; private set; }
        [field:SerializeField][field:FoldoutGroup("Speed")] public float PursuitSpeed{ get; private set; }
        [field: SerializeField] [field: FoldoutGroup("CombatMessage")] public float HitEffectOffSetY { get; private set; }
        [field: SerializeField] [field: FoldoutGroup("Effect")] public GameObject HitEffect1 { get; private set; }
        [field: SerializeField] [field: FoldoutGroup("Effect")] public GameObject HitEffect2 { get; private set; }
        public bool IsTargetDeath { get; set; }
    }
}