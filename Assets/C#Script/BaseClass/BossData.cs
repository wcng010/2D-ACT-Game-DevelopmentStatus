using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.BaseClass
{
    public class BossData : EnemyData
    {
        [field: FoldoutGroup("DeathState")] [field: SerializeField] public bool FirstDeath { get; set; }

        [field: FoldoutGroup("DeathState")] [field: SerializeField] public bool IsTwoLives { get; set; }

        [field: FoldoutGroup("ReadyState")] [field: SerializeField] public GameObject WarningSign { get; private set; }
    }
}