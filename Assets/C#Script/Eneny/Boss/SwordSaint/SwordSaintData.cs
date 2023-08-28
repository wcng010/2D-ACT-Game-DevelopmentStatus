using System;
using C_Script.BaseClass;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/SwordSaintData")]
    public class SwordSaintData : BossData
    {
        [field: FoldoutGroup("ReadyState")] [field: SerializeField] public int ReadyTime { get;private set; }
        [field: FoldoutGroup("Range")] [field: SerializeField] public float AttackRange { get; private set; }
        [field: FoldoutGroup("DodgeState")] [field: SerializeField] public float DodgeAngle { get; private set; }
        [field: FoldoutGroup("DodgeState")] [field: SerializeField] public uint DodgeForce { get; private set; }

        [field: FoldoutGroup("AttackState")] [field: SerializeField] public Vector2 SwordScarRelativePos1 { get;private set; }
        [field: FoldoutGroup("AttackState")] [field: SerializeField] public Vector2 SwordScarRelativePos2 { get;private set; }
        [field: FoldoutGroup("AttackState")] [field: SerializeField] public Vector2 SwordScarRelativePos3 { get;private set; }
        [field: FoldoutGroup("AttackState")] [field: SerializeField] public GameObject SwordScar { get; private set; }
        [NonSerialized]public Vector2 Attack1Direction = new Vector2(Mathf.Cos(75),Mathf.Sin(75));
        [NonSerialized]public Vector2 Attack2Direction = new Vector2(Mathf.Sin(-75), Mathf.Cos(-75));
        [NonSerialized]public Vector2 Attack3Direction = new Vector2(1, 0);
    }
}