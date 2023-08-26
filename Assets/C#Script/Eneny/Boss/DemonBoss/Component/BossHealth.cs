using C_Script.BaseClass;
using C_Script.Eneny.EnemyCommon.Component;
using C_Script.Eneny.EnemyCommon.Model;
using C_Script.Eneny.Monster.Magician.Component;
using UnityEngine;

namespace C_Script.Eneny.Boss.DemonBoss.Component
{
    [RequireComponent(typeof(EnemyHealth))]
    public class BossHealth : MonoBehaviour
    {
        private void Start()
        {
            ((BossData)GetComponentInParent<EnemyModel>().EnemyData).FirstDeath = false;
        }
    }
}
