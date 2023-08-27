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
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.CameraShake,CameraShakeEvent);
        }
        private void CameraShakeEvent()
        {
            _source.GenerateImpulse();
        }
    }
}
