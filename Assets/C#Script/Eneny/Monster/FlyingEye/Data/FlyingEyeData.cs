using C_Script.BaseClass;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.Eneny.Monster.FlyingEye.Data
{
    [CreateAssetMenu(fileName = "Data",menuName = "Data/FlyingEyeData")]
    public class FlyingEyeData:EnemyData
    {
        [field:SerializeField][field:FoldoutGroup("Range")] public float AttackRange { get; private set; }

        [field: SerializeField] [field: FoldoutGroup("PositionCorrection")] public Vector2 LightAttackPosOffSet { get; private set; }
        
        [field: SerializeField] [field: FoldoutGroup("PositionCorrection")] public Vector2 HeavyAttackPosOffSet { get; private set; }
        public float OriginPointX { get; set; }
    }
}