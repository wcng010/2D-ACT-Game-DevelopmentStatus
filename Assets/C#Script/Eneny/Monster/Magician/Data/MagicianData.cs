using C_Script.BaseClass;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.Data
{
    [CreateAssetMenu(fileName = "Data",menuName = "Data/MagicianData")]
    public class MagicianData : EnemyData
    {
        [field: SerializeField][field: FoldoutGroup("Range")] public float RemoteAttackRange { get; private set; }
        [field:SerializeField][field:FoldoutGroup("Range")] public float MeleeAttackRange{ get; private set; }
        [field:SerializeField][field:FoldoutGroup("CombatMessage")] public float RemoteAttackCoolDown{ get; private set; }

        [field:SerializeField][field:FoldoutGroup("CombatMessage")] public float MeleeAttackCoolDown{ get; private set; }
        [field:SerializeField][field:FoldoutGroup("CombatMessage")] public float OriginPointX { get; set; }

        [field: SerializeField][field: FoldoutGroup("CombatMessage")] public float MeleeAttackAngle { get;private set; }
    }
}