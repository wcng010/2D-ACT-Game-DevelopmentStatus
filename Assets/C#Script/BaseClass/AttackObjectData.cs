using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.BaseClass
{
    public class AttackObjectDataSo : DataBaseSo
    {
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] public float MaxHealth {get; private set; }
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] public float CurrentHealth {get; set; }
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] public float AttackPower {get; set;}
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] public int Defense { get; set; }
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField][field:Range(0,1)] public float CriticalRate {get; set; }
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField][field: Range(1,10)] public float CriticalDamage {get; set; }
        
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] public ushort Level { get; set; }
        
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] [field: Range(0, 1)] public float GradeIncrease { get;private set; }
        
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] public float CoolDown { get; set; }
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] public float HitForceUp { get; set; }
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] public float HitForceDown { get; set; }
        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] public float HitForceForward { get; set; }
        //[field: FoldoutGroup("CombatMessage")] [field: SerializeField] public bool IsDeath { get; set;}
        //[field: FoldoutGroup("CombatMessage")] [field: SerializeField] public bool IsHurt { get; set; }
        [field: FoldoutGroup("DebugUsage")] [field: SerializeField] public bool IsDebug { get; private set; }
    }
}
