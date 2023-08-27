using C_Script.BaseClass;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.Eneny.Boss.DemonBoss.Data
{
    [CreateAssetMenu(fileName = "Data",menuName = "Data/DemonBossData")]
    public class DemonBossData:BossData
    {
        [field: FoldoutGroup("ReadyState")] [field: SerializeField] public int ReadyTime { get;private set; }
    }
}