using System;
using C_Script.Common.Model.EventCentre;
using UnityEngine;

namespace C_Script.Object
{
    public class DeadLine : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (String.Compare(col.tag, "Player", StringComparison.Ordinal) == 0)
            {
                ScenesEventCentreManager.Instance.Publish(ScenesEventType.ReStart);
                Time.timeScale = 0;
            }
        }
    }
}
