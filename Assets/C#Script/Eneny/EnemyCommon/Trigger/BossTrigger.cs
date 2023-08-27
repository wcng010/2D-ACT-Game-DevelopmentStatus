using System;
using C_Script.Common.Model.EventCentre;
using UnityEngine;

namespace C_Script.Eneny.EnemyCommon.Trigger
{
    public class BossTrigger : MonoBehaviour
    {
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (String.Compare(col.gameObject.tag, "Player", StringComparison.Ordinal) == 0)
            {
                CombatEventCentreManager.Instance.Publish(CombatEventType.EnterBossRoom);
                gameObject.SetActive(false);
            }
        }
    }
}
