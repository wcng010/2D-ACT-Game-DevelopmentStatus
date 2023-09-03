using C_Script.BaseClass;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Eneny.Boss.DemonBoss.Data
{
    [CreateAssetMenu(fileName = "Data",menuName = "Data/DemonBossData")]
    public class DemonBossData:BossData
    {
        [field: FoldoutGroup("ReadyState")] [field: SerializeField] public int ReadyTime { get;private set; }

        [field: SerializeField] [field: FoldoutGroup("CombatMessage")] public Vector2 OriginPos { get; private set; }

        [field: SerializeField] [field: FoldoutGroup("Prefabs")] public GameObject SkullCircle { get; private set; }
    }
}