using System;
using C_Script.Common.Model.EventCentre;
using UnityEngine;


namespace C_Script.SceneControl
{
    public class PassSceneTrigger : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (String.Compare(col.gameObject.tag, "Player", StringComparison.Ordinal) == 0)
            {
                ScenesEventCentreManager.Instance.Publish(ScenesEventType.LevelPass);
            }
        }
    }
}
