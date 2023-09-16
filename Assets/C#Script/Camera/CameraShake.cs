using System;
using C_Script.Common.Model.EventCentre;
using Cinemachine;
using UnityEngine;
namespace C_Script.Camera
{
    public class CameraShake : MonoBehaviour
    {
        private CinemachineImpulseSource _source;
        private void OnEnable()
        {
            _source = GetComponent<CinemachineImpulseSource>();
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.PlayerHurt,CameraShakeEvent);
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.CriticalStrike,CameraShakeEvent);
        }

        private void OnDisable()
        {
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.PlayerHurt,CameraShakeEvent);
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.CriticalStrike,CameraShakeEvent);
        }

        private void CameraShakeEvent()=> _source.GenerateImpulse();
    }
}
