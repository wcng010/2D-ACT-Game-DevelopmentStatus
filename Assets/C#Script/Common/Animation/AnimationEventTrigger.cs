using System;
using C_Script.Common.Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Common
{
    public class AnimationEventTrigger : MonoBehaviour
    {
        [SerializeField] private Vector2 posOffset;
        public void DestroyThis() => Destroy(gameObject);
        public void Inactive() => gameObject.SetActive(false);
        public void SetParentTrans() => GetComponentInParent<ISetParentMessage>()?.SetParentTrans(posOffset);
        public void SetScaleX_Follow() => GetComponentInParent<ISetParentMessage>()?.SetScaleX_Follow();
        public void SetScaleX_Unfollow()=>GetComponentInParent<ISetParentMessage>()?.SetScaleX_Unfollow();
}
}
