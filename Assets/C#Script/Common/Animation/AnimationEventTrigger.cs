using System;
using C_Script.Common.Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Common
{
    public class AnimationEventTrigger : MonoBehaviour
    {
        [SerializeField] private Vector2 posOffset;
        [SerializeField] private string anitorTrigger;
        public void DestroyThis() => Destroy(gameObject);
        public void Inactive() => gameObject.SetActive(false);
        public void SetOffSet() => GetComponentInParent<ISetParentMessage>()?.SetOffSet(posOffset);
        public void SetScaleX_Follow() => GetComponentInParent<ISetParentMessage>()?.SetScaleX_Follow();
        public void SetScaleX_Unfollow()=>GetComponentInParent<ISetParentMessage>()?.SetScaleX_Unfollow();

        public void AnimatorSetTrigger() => GetComponent<Animator>()?.SetTrigger(anitorTrigger);
    }
}
